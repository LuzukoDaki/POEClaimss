using System;
using System.Data.SqlClient;

namespace POEClaim.Models
{
    public class all_queries
    {
        private string connection = @"server=(localdb)\MSSQLLocalDB;database=POEClaimDb;";

        public void creates_table()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    string query = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='users' AND xtype='U')
                    CREATE TABLE users (
                        userID INT IDENTITY(1,1) PRIMARY KEY,
                        name VARCHAR(100) NOT NULL,
                        surname VARCHAR(100) NOT NULL,
                        email VARCHAR(100) NOT NULL,
                        password VARCHAR(100) NOT NULL
                    )";

                    using (SqlCommand create = new SqlCommand(query, connect))
                    {
                        create.ExecuteNonQuery();
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error creating table: " + error.Message);
            }
        }

        public void store_user(string name, string surname, string email, string password)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    // Trim inputs to avoid storing accidental spaces
                    name = name.Trim();
                    surname = surname.Trim();
                    email = email.Trim();
                    password = password.Trim();

                    Console.WriteLine($"[Register] Storing user: '{name}', '{surname}', '{email}'");

                    string query = @"INSERT INTO users (name, surname, email, password)
                                     VALUES (@name, @surname, @email, @password)";

                    using (SqlCommand insert = new SqlCommand(query, connect))
                    {
                        insert.Parameters.AddWithValue("@name", name);
                        insert.Parameters.AddWithValue("@surname", surname);
                        insert.Parameters.AddWithValue("@email", email);
                        insert.Parameters.AddWithValue("@password", password);

                        insert.ExecuteNonQuery();

                        Console.WriteLine("[Register] User stored successfully.");
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error storing user: " + error.Message);
            }
        }

        public bool search_user(string name, string surname, string email, string password)
        {
            bool found = false;

            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    // Trim inputs to avoid whitespace mismatch
                    name = name.Trim();
                    surname = surname.Trim();
                    email = email.Trim();
                    password = password.Trim();

                    Console.WriteLine($"[Login Attempt] Searching for user: '{name}', '{surname}', '{email}'");

                    // Debug: print all current users
                    using (SqlCommand dumpCmd = new SqlCommand("SELECT * FROM users", connect))
                    using (SqlDataReader reader = dumpCmd.ExecuteReader())
                    {
                        Console.WriteLine("[DB Users]");
                        while (reader.Read())
                        {
                            string dbName = reader["name"].ToString().Trim();
                            string dbSurname = reader["surname"].ToString().Trim();
                            string dbEmail = reader["email"].ToString().Trim();
                            string dbPassword = reader["password"].ToString().Trim();

                            Console.WriteLine($"User: '{dbName}', '{dbSurname}', '{dbEmail}', '{dbPassword}'");
                        }
                    }

                    // Need to close and reopen connection because reader was used above
                    connect.Close();
                    connect.Open();

                    // Case-insensitive search for name, surname, email; exact password match
                    string query = @"SELECT * FROM users
                                     WHERE LOWER(name) = LOWER(@name)
                                     AND LOWER(surname) = LOWER(@surname)
                                     AND LOWER(email) = LOWER(@email)
                                     AND password = @password";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                found = true;
                                Console.WriteLine($"[Login Success] UserID: {reader["userID"]}");
                            }
                            else
                            {
                                Console.WriteLine("[Login Failed] No matching user found.");
                            }
                        }
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error during search: " + error.Message);
            }

            return found;
        }

        public void store_login(string name, string surname, string email, string password)
        {
            // Implement if you want to track logins; leave empty or remove for now
        }

        public List<Claim> GetAllClaims()
        {
            List<Claim> claims = new List<Claim>();

            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    string query = @"SELECT Id, FacultyName, ModuleName, Sessions, Hours, Rate, TotalAmount, DocumentPath, SubmissionDate 
                             FROM claims"; // make sure table name matches your DB

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Claim claim = new Claim
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FacultyName = reader["FacultyName"].ToString(),
                                ModuleName = reader["ModuleName"].ToString(),
                                Sessions = Convert.ToInt32(reader["Sessions"]),
                                Hours = Convert.ToInt32(reader["Hours"]),
                                Rate = Convert.ToDecimal(reader["Rate"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                DocumentPath = reader["DocumentPath"].ToString(),
                                SubmissionDate = Convert.ToDateTime(reader["SubmissionDate"])
                            };

                            claims.Add(claim);
                        }
                    }

                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving claims: " + ex.Message);
            }

            return claims;
        }

    }
}