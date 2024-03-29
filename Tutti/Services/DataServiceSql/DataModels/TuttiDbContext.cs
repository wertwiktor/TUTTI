﻿using DataService.Models;
using System.Data.Entity;


namespace Services.DataServiceSql.DataModels
{
    public class TuttiDbContext : DbContext
    {

        //ToDo: ConnectionString should be configurable via config files.
        //public TuttiDbContext() : base("name=TuttiConnectionString")
        public TuttiDbContext() : base("Data Source=.\\TUTTIDB;Initial Catalog=TuttiDb;Integrated Security=True")        
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TimeStamp> TimeStamps { get; set; }

    }
}
