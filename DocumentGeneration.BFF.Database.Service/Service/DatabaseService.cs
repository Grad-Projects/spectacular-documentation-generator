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
using NpgsqlTypes;
using Microsoft.Extensions.Configuration;

namespace DocumentGeneration.BFF.Database.Service.Service
{

    public class DatabaseService
    {
        private readonly DatabaseOptions _options;

        public DatabaseService(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
            //IConfigurationRoot config = new ConfigurationBuilder()
            //.AddUserSecrets<DatabaseService>()
            //.Build();
            //_options = config["Database:ConnectionString"];
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

        public async Task postDocumentToDB(string htmlFile, string styleName, string userName, string documentName)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using(var command = new NpgsqlCommand("\"add_doc\"", connection))
                    {
                        command.Parameters.AddWithValue("doc_name", documentName);
                        command.Parameters.AddWithValue("doc_content", htmlFile);
                        command.Parameters.AddWithValue("style_name", styleName);
                        command.Parameters.AddWithValue("user_name", userName);

                        command.CommandType = CommandType.StoredProcedure;
                        await command.ExecuteNonQueryAsync();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error querying database: {ex.Message}");
                    
                }
            }
        }

        public async Task addUserToDb(string userName) {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new NpgsqlCommand("\"add_user\"", connection))
                    {
                        command.Parameters.AddWithValue("user_name", userName);
                        command.CommandType = CommandType.StoredProcedure;

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error querying database: {ex.Message}");
                }
            }
        }

        public async Task<bool> checkStyle(string styleName)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new NpgsqlCommand("SELECT check_style_exists(@style_name)", connection))
                    {
                        command.Parameters.AddWithValue("@style_name", styleName);

                        var result = await command.ExecuteScalarAsync();

                        return (bool)result;
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Error querying database: {ex.Message}");
                    return false;
                }
            }
        }

        //checks to see if the user exist in db and if not we add them 
        public async Task checkUserInDB(string userName)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new NpgsqlCommand("SELECT check_user_exists(@githubUserName)", connection))
                    {
                        command.Parameters.AddWithValue("@githubUserName", userName);

                        var result = await command.ExecuteScalarAsync();

                        if (!(bool)result)
                        {
                            await addUserToDb(userName);
                        }
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
