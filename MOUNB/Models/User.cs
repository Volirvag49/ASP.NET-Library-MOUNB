using System.ComponentModel.DataAnnotations;

namespace MOUNB.Models
{
    public class User
    {
        // ID 
        public int Id { get; set; }
        // Фамилия Имя Отчество
        [Required]
        [Display(Name = "ФИО")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Логин
        [Required]
        [Display(Name = "Логин")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Login { get; set; }
        // Пароль
        [Required]
        [Display(Name = "Пароль")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Password { get; set; }
        // Должность
        [Display(Name = "Должность")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Position { get; set; }

        // Идентификатор роли
        [Required]
        [Display(Name = "Роль пользователя")]
        public UserRole Role { get; set; }
    } // Конец класса

    public enum UserRole: byte
    {
        Администратор = 1,
        Библиотекарь = 2
    } 
} 