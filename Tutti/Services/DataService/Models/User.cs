using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace DataService.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } = (DateTime)SqlDateTime.MinValue;

        public string Email { get; set; } = string.Empty;

        public string Identifier { get; set; } = string.Empty;

        public UserLevel Level { get; set; } = UserLevel.User;

        public string Nationality { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public List<TimeStamp> TimeStamps { get; set; }

        [NotMapped]
        public string Initials { get => string.Concat(Name.FirstOrDefault(), Surname.FirstOrDefault()); }

        [NotMapped]
        public string FullName { get => string.Join(string.Empty, Name, Surname); }

        //[NotMapped]
        //public string RecentEntry { get => TimeStamps.LastOrDefault().ToString(); }
    }
}
