using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace DataService.Models
{
    public class User
    {
        public long Id { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = true)]
        public string Surname { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } = (DateTime)SqlDateTime.MinValue;
        
        [Required(AllowEmptyStrings = true)]
        public string Email { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = true)]
        public string Identifier { get; set; } = string.Empty;
        
        public UserLevel Level { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        public string Nationality { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = true)]
        public string PhoneNumber { get; set; } = string.Empty;

        public List<TimeStamp> TimeStamps { get; set; }
    }
}
