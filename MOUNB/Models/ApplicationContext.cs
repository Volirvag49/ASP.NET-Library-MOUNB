using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MOUNB.Models
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(): base("MounbDb") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    } // Конец класса
} // Конец пронстранства