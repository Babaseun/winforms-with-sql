using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace E_CommerceApp.Helpers
{
    /// <summary>
    ///I wanted to implement users
    ///but changed after 
    ///modification to the task
    /// </summary>
    class Helper
    {
        public static string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
        public static bool ComparePassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword.Trim());
        }

        public static Guid GenerateToken(string email)
        {
            Guid ID = Guid.Empty;
            var connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                        $"SELECT * FROM Users WHERE Email ='{email}'", connection))
                    {
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                ID = (Guid)reader["ID"];


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ID;

        }
    }
}
