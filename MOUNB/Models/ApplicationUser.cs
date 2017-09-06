using Microsoft.AspNet.Identity.EntityFramework;

namespace MOUNB.Models
{
    public class ApplicationUser: IdentityUser
    {

        public int Year { get; set; }

        public ApplicationUser()
        {

        }

    } // Конец класса
} // Конец пронстранства