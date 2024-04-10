using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace DocumentGeneration.BFF.Database.Service.Models
{
    public class DatabaseOptions : IOptions<DatabaseOptions>
    {
        public static readonly string Section = "DatebaseOptions";
        public DatabaseOptions Value => this;

        [Required]
        public string? ConnectionString { get; init; }
    }
}
