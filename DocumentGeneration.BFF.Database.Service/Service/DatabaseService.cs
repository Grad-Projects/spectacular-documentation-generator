using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DocumentGeneration.BFF.Database.Service.Models;
using DocumentGeneration.BFF.Core.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace DocumentGeneration.BFF.Database.Service.Service
{

    public class DatabaseService
    {
        private readonly DatabaseOptions _options;

        public DatabaseService(IOptions<DatabaseOptions> options)
        {

            _options = options.Value;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_options.ConnectionString);
        }


        public async Task<string> getStyleFromDB(string styleName)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT \"styleContent\" FROM styles WHERE \"styleName\" = @StyleName", connection))
                    {
                        command.Parameters.AddWithValue("styleName", styleName);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader["styleContent"].ToString();
                            }
                            else
                            {
                                Console.WriteLine("Style not found.");
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error querying database: {ex.Message}");
                    return null;
                }
            }

        }

        public async Task postDocumentToDB(string htmlFile, string styleName, string userName, string documentName, string documentContent)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using(var command = new NpgsqlCommand("name of proc here", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("document_name", documentName);
                        command.Parameters.AddWithValue("document_content", documentContent);
                        command.Parameters.AddWithValue("style_name", styleName);
                        command.Parameters.AddWithValue("githubUserName", userName);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error querying database: {ex.Message}");
                    
                }
            }
        }

        //public async Task CheckOrAddUser(string userName)
        //{
        //    using (var connection = GetConnection())
        //    {
        //        try
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new NpgsqlCommand())
        //            {
        //                command.Connection = connection;
        //                command.CommandText = "SELECT COUNT(*) FROM users WHERE \"githubUserName\" = @UserName";
        //                command.Parameters.AddWithValue("UserName", userName);

                        
        //                var result = await command.ExecuteScalarAsync();
        //                int count = Convert.ToInt32(result);

        //                if (count == 0)
        //                {
                            
        //                    command.CommandText = "INSERT INTO users (\"githubUserName\") VALUES (@UserName)";
        //                    await command.ExecuteNonQueryAsync();
        //                    Console.WriteLine($"User {userName} added successfully!");
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"User {userName} already exists!");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error querying database: {ex.Message}");
        //        }
        //    }
        //}

        public async Task CheckOrAddUser(string userName) {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new NpgsqlCommand("proc name", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("githubUserName", userName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error querying database: {ex.Message}");
                }
            }
        }
    }
}
