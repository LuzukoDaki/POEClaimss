using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace POEClaim.Models
{
    public class sql_query
    {
        // connection string 
        private string connection = @"Server=(localdb)\database;database=claim_nigga;";

        // method to create user creating tables
        public void create_table()
        {

            // try and catch for error handling - they are marks for this
            try
            {

                // connecting first in order to opent the port, using the the using function
                using (SqlConnection connect = new SqlConnection(connection))
                {

                    // Openeing the connection
                    connect.Open();

                    // tempt variable to hold the query
                    string query = @"Create table users (userID int identity(1,1) not null primary key,
                                                         name varchar(50) not null, 
                                                         age int not null)";


                    // Use the sql command class to run the query
                    using (SqlCommand create_table = new SqlCommand(query, connect))
                    {

                        // run the query
                        create_table.ExecuteNonQuery();

                        // showing the success message
                        Console.WriteLine("Eita daa!! Table is created");

                        // Then closing the connection
                        connect.Close();


                    }



                }

            }
            catch (Exception error)
            {
                // show error message
                Console.WriteLine(error.Message);

            }


        }

        public void store_user(string name, int age)
        {

            // try and catch for error handling - they are marks for this
            try
            {

                // connecting first in order to opent the port, using the the using function
                using (SqlConnection connect = new SqlConnection(connection))
                {

                    // Openeing the connection
                    connect.Open();

                    // tempt variable to hold the query
                    string query = @"insert into users values('" + name + "', " + age + ");";


                    // Use the sql command class to run the query
                    using (SqlCommand create_table = new SqlCommand(query, connect))
                    {

                        // run the query
                        create_table.ExecuteNonQuery();

                        // showing the success message
                        Console.WriteLine("Data inserted successfully");

                        // Then closing the connection
                        connect.Close();


                    }



                }

            }
            catch (Exception error)
            {
                // show error message
                Console.WriteLine(error.Message);

            }


        }

        public bool login_user(string name, int age)
        {
            bool found = false;



            // try and catch for error handling - they are marks for this
            try
            {

                // connecting first in order to opent the port, using the the using function
                using (SqlConnection connect = new SqlConnection(connection))
                {

                    // Openeing the connection
                    connect.Open();

                    // tempt variable to hold the query
                    string query = @"Select * from users 
                                   where name = '" + name + "' AND age = '" + age + "' ";


                    // Use the sql command class to run the query
                    using (SqlCommand create_table = new SqlCommand(query, connect))
                    {

                        // run the query
                        create_table.ExecuteNonQuery();

                        // showing the success message
                        Console.WriteLine("User Found");

                        using (SqlDataReader finds = create_table.ExecuteReader())
                        {

                            while (finds.Read())
                            {
                                Console.WriteLine(finds["userID"]);
                                Console.WriteLine(finds["name"]);
                                Console.WriteLine(finds["age"]);
                                found = true;

                            }





                        }



                    }
                    // Then closing the connection
                    connect.Close();
                }
            }
            catch (Exception error)
            {
                // show error message
                Console.WriteLine(error.Message);

            }

            return found;

        }






    
    public void create_claim_table()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    string query = @"CREATE TABLE claims (
                                claimID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                LecturerName VARCHAR(50) NOT NULL,
                                ClaimDescription VARCHAR(255) NOT NULL,
                                Amount DECIMAL(10,2) NOT NULL,
                                SubmissionDate DATETIME NOT NULL
                             )";

                    using (SqlCommand createTable = new SqlCommand(query, connect))
                    {
                        createTable.ExecuteNonQuery();
                        Console.WriteLine("Claims table created successfully");
                        connect.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}
