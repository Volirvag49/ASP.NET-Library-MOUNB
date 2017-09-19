using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MOUNB.Models
{
    public class ReaderHistorys
    {

        // ID 
        public int Id { get; set; }

        // Читатель
        [Required]
        [Display(Name = "Читатель")]
        public int? ReaderId { get; set; }
        public Reader Reader { get; set; }

        // Идентификатор экземпляра книги
        [Required]
        [Range(1, UInt32.MaxValue, ErrorMessage = "Введите номер экземпляра")]
        [Display(Name = "Экземпляр")]
        public int? BookExampleId { get; set; }

        // Дата выдачи
        [Required]
        [Display(Name = "Дата выдачи")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Opened { get; set; }

        // Дата возврата
        [Required]
        [Display(Name = "Дата возврата")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Closed { get; set; }

        // Статус книги (возвращена или нет)
        [Required]
        [Display(Name = "Возвращена")]
        public bool Returned { get; set; }

        //Количество продлений сроков возврата книг
        [Required]
        [Display(Name = "Кол-во продлений")]
        public int ExtensionsCount { get; set; }

        // Абонемент, на котором читатель взял книгу.
        // От этого зависят правила выдачи книг и переноса дат возврата
        [Required]
        [Display(Name = "Правила выдачи")]
        public int? SubscriptionId { get; set; }
        public LibrarySubscriptions LibrarySubscriptions { get; set; }
    } // Конец класса
} // Конец пронстранста