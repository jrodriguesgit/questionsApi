using System.ComponentModel.DataAnnotations;

namespace Configuration
{
    public class SqlOptions
    {
        public const string Key = "Sql";

        [Required]
        public string ConnectionString { get; set; }
    }
}
