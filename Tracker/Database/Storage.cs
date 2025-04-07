using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Tracker.Database.Models;

namespace Tracker
{
    internal sealed class Storage
    {
        private const string DefaultDatabaseName = "Tracker";
        private const string RecordsTableName = "[dbo].[Records]";
        
        private readonly SqlConnectionStringBuilder connectionStringBuilder;

        public Storage(string connectionString)
        {
            connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            if (string.IsNullOrWhiteSpace(connectionStringBuilder.InitialCatalog))
            {
                connectionStringBuilder.InitialCatalog = DefaultDatabaseName;
            }
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var rows = new List<Record>();
            
            using (SqlConnection connection = GetOpenedConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = 
                         "SELECT [Id], [CreatedAt], [Description], [Total], [Comment] " +
                        $"FROM {RecordsTableName}";

                    using (var reader = await command.ExecuteReaderAsync(token))
                    {
                        while (await reader.ReadAsync(token))
                        {
                            var columnIndex = 0;
                            var record = new Record
                            {
                                Id = reader.GetInt64(columnIndex++),
                                CreatedAt = reader.GetDateTime(columnIndex++).ToLocalTime(),
                                Description = reader.GetString(columnIndex++),
                                Total = reader.GetDecimal(columnIndex++),
                                Comment = GetNullableString(reader, columnIndex++)
                            };

                            rows.Add(record);
                        }
                    }
                }
            }

            return rows;
        }

        public bool TryCreateRecord(Record newRecord, out Exception exception)
        {
            try
            {
                using (SqlConnection connection = GetOpenedConnection())
                {
                    using (var createCommand = connection.CreateCommand())
                    {
                        createCommand.CommandText =
                             $"INSERT INTO {RecordsTableName} ([CreatedAt], [Description], [Total], [Comment]) "
                            + "VALUES (@CreatedAt, @Description, @Total, @Comment)";

                        createCommand.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                        createCommand.Parameters.AddWithValue("@Description", newRecord.Description);
                        createCommand.Parameters.AddWithValue("@Total", newRecord.Total);
                        createCommand.Parameters.AddWithValue("@Comment", newRecord.Comment);

                        createCommand.ExecuteNonQuery();
                    }
                }

                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public bool TryUpdateRecord(Record newRecord, out Exception exception)
        {
            try
            {
                if (newRecord.Id < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(newRecord));
                }

                using (SqlConnection connection = GetOpenedConnection())
                {
                    using (var createCommand = connection.CreateCommand())
                    {
                        createCommand.CommandText =
                             $"UPDATE {RecordsTableName}"
                            + "SET [Description] = @Description, "
                                + "[Total] = @Total, "
                                + "[Comment] = @Comment "
                            + "WHERE [Id] = @Id";

                        createCommand.Parameters.AddWithValue("@Id", newRecord.Id);
                        createCommand.Parameters.AddWithValue("@Description", newRecord.Description);
                        createCommand.Parameters.AddWithValue("@Total", newRecord.Total);
                        createCommand.Parameters.AddWithValue("@Comment", newRecord.Comment);

                        if (createCommand.ExecuteNonQuery() == 0)
                        {
                            throw new ArgumentOutOfRangeException(nameof(newRecord), $"Record with Id = {newRecord.Id} does not exists.");
                        }
                    }
                }

                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public bool TryRemoveRecord(long id, out Exception exception)
        {
            try
            {
                if (id < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }

                using (var connection = GetOpenedConnection())
                {
                    using (var removeCommand = connection.CreateCommand())
                    {
                        removeCommand.CommandText = 
                            $"DELETE FROM {RecordsTableName} " +
                             "WHERE [Id] = @Id";
                        
                        removeCommand.Parameters.AddWithValue("@Id", id);

                        if (removeCommand.ExecuteNonQuery() == 0)
                        {
                            throw new ArgumentOutOfRangeException(nameof(id), $"Record with Id = {id} does not exists.");
                        }
                    }
                }

                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        private SqlConnection GetOpenedConnection()
        {
            var connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            
            try
            {
                connection.Open();
            }
            catch (SqlException ex) when (ex.Class == 11)
            {
                connection = CreateDatabase();
            }

            CreateRecordsTableIfRequired(connection);

            return connection;
        }

        private SqlConnection CreateDatabase()
        {
            var databaseName = new SqlCommandBuilder().QuoteIdentifier(connectionStringBuilder.InitialCatalog);
            var rootConnectionStringBuilder = new SqlConnectionStringBuilder(connectionStringBuilder.ConnectionString)
            {
                InitialCatalog = string.Empty
            };

            var connection = new SqlConnection(rootConnectionStringBuilder.ConnectionString);
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"CREATE DATABASE {databaseName}";

                command.ExecuteNonQuery();
            }

            connection.ChangeDatabase(connectionStringBuilder.InitialCatalog);
            return connection;
        }

        private void CreateRecordsTableIfRequired(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = 
                    $"IF OBJECT_ID(N'{RecordsTableName}', N'U') IS NULL "
                        + $"CREATE TABLE {RecordsTableName} ("
                            + "[Id] BIGINT IDENTITY PRIMARY KEY NOT NULL, "
                            + "[CreatedAt] DATETIME NOT NULL, "
                            + "[Description] NVARCHAR(256) NOT NULL, "
                            + "[Total] DECIMAL(18, 4) NOT NULL, "
                            + "[Comment] NVARCHAR(2048) NULL" +
                        ")";

                command.ExecuteNonQuery();
            }
        }

        private static string GetNullableString(SqlDataReader dataReader, int index)
        {
            var value = dataReader.GetSqlString(index);
            if (value.IsNull)
            {
                return null;
            }

            return value.Value;
        }
    }
}
