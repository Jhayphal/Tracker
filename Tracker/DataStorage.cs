using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Tracker
{
    internal sealed class DataStorage
    {
        private readonly string connectionString;

        public DataStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var rows = new List<Record>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(token);

                using (var reader = await GetLoadCommand(connection).ExecuteReaderAsync(token))
                {
                    while (await reader.ReadAsync(token))
                    {
                        var columnIndex = 0;
                        var record = new Record
                        {
                            Id = reader.GetInt64(columnIndex++),
                            CreatedAt = reader.GetDateTime(columnIndex++),
                            Description = reader.GetString(columnIndex++),
                            Total = reader.GetDecimal(columnIndex++),
                            Comment = SafeGetSqlString(reader, columnIndex++)
                        };

                        rows.Add(record);
                    }
                }
            }

            return rows;
        }

        public bool TryCreateRecord(Record newRecord, out Exception exception)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var createCommand = connection.CreateCommand())
                    {
                        createCommand.CommandType = CommandType.Text;
                        createCommand.CommandText = 
                              "INSERT INTO [dbo].[Records] ([CreatedAt], [Description], [Total], [Comment]) "
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

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var createCommand = connection.CreateCommand())
                    {
                        createCommand.CommandType = CommandType.Text;
                        createCommand.CommandText =
                              "UPDATE [dbo].[Records]"
                            + "SET [Description] = @Description"
                            + ", [Total] = @Total"
                            + ", [Comment] = @Comment "
                            + "WHERE [Id] = @Id";

                        createCommand.Parameters.AddWithValue("@Id", newRecord.Id);
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

        private SqlCommand GetLoadCommand(SqlConnection connection)
        {
            var loadCommand = connection.CreateCommand();
            loadCommand.CommandText = "SELECT [Id]"
                + ", [CreatedAt]"
                + ", [Description]"
                + ", [Total]"
                + ", [Comment] "
                + "FROM [dbo].[Records]";
            loadCommand.CommandType = CommandType.Text;

            return loadCommand;
        }

        private static string SafeGetSqlString(SqlDataReader dataReader, int index)
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
