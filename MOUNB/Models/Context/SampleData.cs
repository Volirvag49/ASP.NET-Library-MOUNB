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
                new User{Name="Пользователь по умолчанию", Login="111111", Password="111111", Position="Администратор", Role = UserRole.Администратор },
                new User{Name="аа", Login="202020", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="бб", Login="191919", Password="111111", Position="Тест", Role = UserRole.Администратор },
                new User{Name="вв", Login="181818", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="гг", Login="171717", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="дд", Login="161616", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="ее", Login="151515", Password="111111", Position="Тест", Role = UserRole.Администратор },
                new User{Name="ёё", Login="141414", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="жж", Login="131313", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="зз", Login="121212", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="ии", Login="111111", Password="111111", Position="Тест", Role = UserRole.Администратор },
                new User{Name="кк", Login="101010", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="лл", Login="999", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="мм", Login="888", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="нн", Login="777", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="оо", Login="666", Password="111111", Position="Тест", Role = UserRole.Администратор },
                new User{Name="пп", Login="555", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="рр", Login="444", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="сс", Login="333", Password="111111", Position="Тест", Role = UserRole.Администратор },
                new User{Name="тт", Login="222", Password="111111", Position="Тест", Role = UserRole.Пользователь },
                new User{Name="уу", Login="111", Password="111111", Position="Тест", Role = UserRole.Пользователь },
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