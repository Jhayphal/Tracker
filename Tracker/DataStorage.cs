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

        private SqlCommand loadCommand;

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

                await Task.Delay(TimeSpan.FromSeconds(5), token);

                var reader = await GetLoadCommand(connection).ExecuteReaderAsync(token);

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

            return rows;
        }

        private SqlCommand GetLoadCommand(SqlConnection connection)
        {
            if (loadCommand is null)
            {
                loadCommand = connection.CreateCommand();
                loadCommand.CommandText = "SELECT [Id]" +
                    ", [CreatedAt]" +
                    ", [Description]" +
                    ", [Total]" +
                    ", [Comment] " +
                    "FROM [dbo].[Records]";
                loadCommand.CommandType = CommandType.Text;
                loadCommand.Prepare();
            }

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
