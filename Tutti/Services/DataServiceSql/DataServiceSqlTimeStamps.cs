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
        public void EditTimeStamp(TimeStamp timeStamp)
        {
            if (timeStamp == null)
            {
                throw new ArgumentNullException("timeStamp");
            }

            using (var context = GetDbContext())
            {
                context.Entry(timeStamp).State = System.Data.Entity.EntityState.Modified;
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
                                                            && timeStamp.EntryDate >= minDateTime
                                                            && timeStamp.EntryDate <= maxDateTime));
            }
            return timeStamps;
        }

        public TimeStamp GetLastTimeStampByUserId(long userId)
        {
            using (var context = GetDbContext())
            {
                return context.TimeStamps.Where(p => p.User.Id == userId).OrderByDescending(p => p.EntryDate).FirstOrDefault();
            }
        }
    }
}
