using System.Collections.Generic;
using System.Data.Entity;

namespace MOUNB.Models
{
    public class SampleData : DropCreateDatabaseAlways<MounbDbContext>
    {
        protected override void Seed(MounbDbContext context)
        {


            // Добавление пользователей
            List<User> users = new List<User>
            {
                new User{Name="Пользователь по умолчанию", Login="111111", Password="111111", Position="Администратор", Role = UserRole.Администратор }
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    } // Конец класса
} // Конец пронстранства