using Azure.Identity;
using file_importer_for_user_data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var file_path = @"D:\dot_net_projects\file_importer_for_user_data\users.csv";
            List<Users> user_list = new List<Users>();

            // Reading the CSV file and populating the user list
            using (StreamReader reader = new StreamReader(file_path))
            {
                string? headerLine = reader.ReadLine();
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    try
                    {
                        int userID = int.Parse(fields[0]);
                        string? username = fields[1];
                        string? email = fields[2];
                        string? passwordHashed = fields[3];
                        string? status = fields[4];
                        string? addressLine1 = fields[5];
                        string? addressLine2 = fields[6];
                        string? city = fields[7];
                        string? state = fields[8];
                        string? postalCode = fields[9];
                        int countryID = int.Parse(fields[10]);
                        DateTime createdAT = DateTime.Parse(fields[11]);

                        Users user = new Users(
                            userID,
                            username,
                            email,
                            passwordHashed,
                            status,
                            addressLine1,
                            addressLine2,
                            city,
                            state,
                            postalCode,
                            countryID,
                            createdAT
                        );

                        user_list.Add(user);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing CSV line: {ex.Message}. Line: {line}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error: {ex.Message}. Line: {line}");
                    }
                }
            }

            // Inserting data into the database
            try
            {
                using (SqlConnection conn = new SqlConnection(configuration.GetSection("ConnectionStrings").Value))
                {
                    conn.Open();

                    foreach (var user in user_list)
                    {
                        try
                        {
                            var query = "INSERT INTO Users (UserID, Username, Email, PasswordHash, Status, AddressLine1, AddressLine2, City, State, PostalCode, CountryID, CreatedAT) " +
                                        "VALUES (@userID, @username, @email, @passwordHashed, @status, @addressLine1, @addressLine2, @city, @state, @postalCode, @countryID, @createdAT)";

                            using (SqlCommand command = new SqlCommand(query, conn))
                            {
                                command.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int) { Value = user.UserID });
                                command.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar) { Value = user.Username });
                                command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar) { Value = user.Email });
                                command.Parameters.Add(new SqlParameter("@passwordHashed", SqlDbType.VarChar) { Value = user.PasswordHashed });
                                command.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar) { Value = user.Status });
                                command.Parameters.Add(new SqlParameter("@addressLine1", SqlDbType.VarChar) { Value = user.AddressLine1 });
                                command.Parameters.Add(new SqlParameter("@addressLine2", SqlDbType.VarChar) { Value = user.AddressLine2 });
                                command.Parameters.Add(new SqlParameter("@city", SqlDbType.VarChar) { Value = user.City });
                                command.Parameters.Add(new SqlParameter("@state", SqlDbType.VarChar) { Value = user.State });
                                command.Parameters.Add(new SqlParameter("@postalCode", SqlDbType.VarChar) { Value = user.PostalCode });
                                command.Parameters.Add(new SqlParameter("@countryID", SqlDbType.Int) { Value = user.CountryID });
                                command.Parameters.Add(new SqlParameter("@createdAT", SqlDbType.DateTime) { Value = user.CreatedAT });

                                command.CommandType = CommandType.Text;

                                if (command.ExecuteNonQuery() > 0)
                                {
                                    Console.WriteLine($"User {user.UserID} added successfully.");
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627) // SQL error code for unique constraint violation (duplicate key)
                            {
                                Console.WriteLine($"Duplicate UserID: {user.UserID}. Skipping this record.");
                            }
                            else
                            {
                                Console.WriteLine($"SQL Error: {ex.Message}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error inserting UserID: {user.UserID}. Error: {ex.Message}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
