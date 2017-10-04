using System;
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
                new User{Name="Пользователь по умолчанию 1", Login="111111", Password="111111", Position="Администратор", Role = UserRole.Администратор },
                new User{Name="Пользователь по умолчанию 2", Login="222222", Password="222222", Position="Библиотекарь", Role = UserRole.Библиотекарь },
                new User{Name="Попова Татьяна Ильинична", Login="PopovaTatyana93", Password="vBzEy9XhH9ZN", Position="Тест", Role = UserRole.Администратор },
                new User{Name="Федотов Арсений Олегович", Login="FedotovArseniy255", Password="bfMNjaJaxToR", Position="Тест", Role = UserRole.Библиотекарь },
                new User{Name="Киселёва Ольга Александровна", Login="KiselevaOlga69", Password="JChTQoBWERBo", Position="Тест", Role = UserRole.Библиотекарь },
                new User{Name="Давыдов Варфоломей Викторович", Login="DavyidovVarfolomey46", Password="nzXCZ2ALdbiO", Position="Тест", Role = UserRole.Библиотекарь },
                new User{Name="Леонтьева Васса Ивановна", Login="LeontevaVassa93", Password="HdA3yJRfHYIc", Position="Тест", Role = UserRole.Библиотекарь },
                new User{Name="Дидиченко Арсений Александрович", Login="DidichenkoArseniy119", Password="dvJHlvclxXyh", Position="Тест", Role = UserRole.Библиотекарь },

                new User{Name="Андреева Елизавета Георгиевна ", Login="AndreevaElizaveta231", Password="f8PGfdV4gN9G", Position="Тест", Role = UserRole.Библиотекарь }

            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            // Добавление читателей
            List<Reader> readers = new List<Reader>
            {
                new Reader{ LibraryCardId= 647347, Name = "Гусева Розина Владиславовна", Education = "Колледж искусств", Profession = "Дирижер", PlaceOfWorkStudy = "Колледж искусств", Address = "г. Междуречье, ул. Басовская, дом 41, квартира 42", PhoneNumber = "651-41-37",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                },
                 new Reader{ LibraryCardId= 535436, Name = "Давыдов Семён Геннадьевич", Education = "СВГУ", Profession = "Спортивный тренер", PlaceOfWorkStudy = "С/К Энергия", Address = "г. Междуречье, ул. Басовская, дом 41, квартира 42", PhoneNumber = "8(910)640-61-59",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                },
                new Reader{ LibraryCardId= 452754, Name = "Троицкий Корнил Ярославович", Education = "СВГУ", Profession = "Тележурналис", PlaceOfWorkStudy = "ГТРК", Address = "ул. Авиаторов, дом 37, квартира 233", PhoneNumber = "8(923)601-59-93",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                },
                 new Reader{ LibraryCardId= 343453, Name = "Щербакова Августа Аркадьевна", Education = "Профессиональный лицей №7", Profession = "Продавец", PlaceOfWorkStudy = "т.к. Идея", Address = "ул. Бакинская, дом 63, квартира 285", PhoneNumber = "8(945)455-78-36",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                },
                new Reader{ LibraryCardId= 243451, Name = "Чапко Серафима Богдановна", Education = "Профессиональный лицей №7", Profession = "Продавец", PlaceOfWorkStudy = "Северное сияние", Address = "ул. Бартеневская, дом 92, квартира 218", PhoneNumber = "8(933)451-32-52",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                },
                new Reader{ LibraryCardId= 163234, Name = "Денисов Фотий Вячеславович", Education = "Среднее-специальное", Profession = "Ветеринар", PlaceOfWorkStudy = "Вет-лечебница", Address = " ул. Базовская, дом 30, квартира 154", PhoneNumber = "8(915)161-31-35",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                },


                 new Reader{ LibraryCardId= 93241, Name = "Недашковский Тимофей Давидович", Education = "среднее", Profession = "Студент", PlaceOfWorkStudy = "Мед-колледж", Address = "ул. Баженова, дом 19, квартира 266", PhoneNumber = "8(918)251-30-18",
                Registration = GenRandomRegData(),
                DOB = GenRandomDOB()
                }

            };

            Random rndData = new Random();
            foreach (Reader reader in readers)
            {
                reader.DOB = GenRandomDOB(rndData);
                reader.Registration = GenRandomRegData(rndData);
                context.Readers.Add(reader);
            }

            // Добавление правила
            List<LibrarySubscriptions> subs = new List<LibrarySubscriptions> {
                new LibrarySubscriptions{Name="АБС", BooksCount=4, ExtensionsCount= 3},
                new LibrarySubscriptions{Name="АБО", BooksCount=3, ExtensionsCount= 1}
            };

            foreach (LibrarySubscriptions item in subs)
            {

                context.LibrarySubscriptions.Add(item);
            }

            context.SaveChanges();
            base.Seed(context);

        } // Конец метода

        // Генерация дат
        private  DateTime GenRandomRegData(Random random = null)
        {
            DateTime from = DateTime.Now.AddYears(-30);
            DateTime to = DateTime.Now;


            if (from >= to)
            {
                throw new Exception("Параметр \"from\" должен быть меньше параметра \"to\"!");
            }
            if (random == null)
            {
                random = new Random();
            }
            TimeSpan range = to - from;
            var randts = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            return from + randts;
        }

        private static DateTime GenRandomDOB(Random random = null)
        {

            DateTime from = DateTime.Now.AddYears(-90);
            DateTime to = DateTime.Now.AddYears(-18);

            if (from >= to)
            {
                throw new Exception("Параметр \"from\" должен быть меньше параметра \"to\"!");
            }
            if (random == null)
            {
                random = new Random();
            }
            TimeSpan range = to - from;
            var randts = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            return from + randts;
        } // Конец метода
    } // Конец класса
} // Конец пронстранства