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


        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    } // Конец класса
} // Конец пронстранства