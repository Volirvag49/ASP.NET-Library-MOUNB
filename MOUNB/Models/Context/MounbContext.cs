using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MOUNB.Models
{
    public class MounbDbContext : DbContext
    {

        public MounbDbContext()
        {
            Database.SetInitializer<MounbDbContext>(new SampleData());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<LibrarySubscriptions> LibrarySubscriptions { get; set; }
        public DbSet<ReaderHistorys> ReaderHistorys { get; set; }
    } // Конец класса
} // Конец пронстранства