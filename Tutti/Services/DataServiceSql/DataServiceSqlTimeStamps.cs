using DataService.Models;

namespace Services.DataServiceSql
{
    public partial class DataServiceSql
    {
        public void AddTimeStamp(TimeStamp timeStamp)
        {
            if (timeStamp == null)
            {
                throw new ArgumentNullException("timeStamp");
            }

            using (var context = GetDbContext())
            {
                context.TimeStamps.Add(timeStamp);
                context.SaveChanges();
            }
        }

        public void DeleteTimeStamp(long id)
        {
            var timeStamp = new TimeStamp() { Id = id };
            using (var context = GetDbContext())
            {
                context.TimeStamps.Attach(timeStamp);
                context.TimeStamps.Remove(timeStamp);
                context.SaveChanges();
            }
        }

        public List<TimeStamp> GetTimeStamps(long userId, DateTime minDateTime, DateTime maxDateTime)
        {
            var timeStamps = new List<TimeStamp>();
            using (var context = GetDbContext())
            {
                timeStamps.AddRange(context.TimeStamps.Where(timeStamp => 
                                                            timeStamp.UserId == userId 
                                                            && timeStamp.DateTime >= minDateTime 
                                                            && timeStamp.DateTime <= maxDateTime));
            }
            return timeStamps;
        }
    }
}
