namespace DataService.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public int Level { get; set; }
        public string Nationality { get; set; }
        public List<TimeStamp> TimeStamps { get; set; }
    }
}
