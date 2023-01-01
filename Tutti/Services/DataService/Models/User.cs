namespace DataService.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public List<TimeStamp> TimeStamps { get; set; }
    }
}
