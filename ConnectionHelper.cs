using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace licenta_test
{
    public class ConnectionHelper
    {
        private static string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool IsUserAdmin(string username)
        {
            bool isAdmin = false;
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT Rol FROM Users WHERE Utilizator = @username";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result.ToString() == "admin")
                    {
                        isAdmin = true;
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (if necessary)
                    Console.WriteLine(ex.ToString());
                }
            }
            return isAdmin;
        }
    }
}