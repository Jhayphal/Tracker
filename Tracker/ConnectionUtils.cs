using System;
using System.Data.SqlClient;

namespace Tracker
{
    internal static class ConnectionUtils
    {
        public static bool TryConnect(string connectionString, out Exception exception)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
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
    }
}
