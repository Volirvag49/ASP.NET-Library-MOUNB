using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOUNB.Models
{
    public class Reader
    {
        // ID 
        public int Id { get; set; }

        // Номер ЧБ
        [Required]
        [Display(Name = "Номер читательского билета")]
        public int? LibraryCardId { get; set; }

        // Дата регистрации
        [Required]
        [Display(Name = "Дата регистрации")]
        //[Range(2016, 2100, ErrorMessage = "Недопустимый год")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Registration { get; set; }

        // Фамилия Имя Отчество
        [Required]
        [Display(Name = "ФИО")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }

        // Дата рождения
        [Required]
        [Display(Name = "Дата рождения")]
        //[Range(1900, 2003, ErrorMessage = "Недопустимый год")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        // Образование
        [Required]
        [Display(Name = "Образование")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Education { get; set; }

        // Профессия
        [Required]
        [Display(Name = "Профессия")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Profession { get; set; }

        // Место работы/учёбы
        [Required]
        [Display(Name = "Место работы/учёбы")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string PlaceOfWorkStudy { get; set; }

        // Адрес
        [Required]
        [Display(Name = "Домашний адрес")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Address { get; set; }

        // Телефон
        [Display(Name = "Контактный телефон")]
        [MaxLength(15, ErrorMessage = "Превышена максимальная длина записи")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<ReaderHistorys> histores { get; set; }

    } // Конец класса
} // Конец пронстранстаа