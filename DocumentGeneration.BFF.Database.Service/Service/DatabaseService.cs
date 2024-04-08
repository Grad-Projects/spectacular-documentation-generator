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
    }
}
