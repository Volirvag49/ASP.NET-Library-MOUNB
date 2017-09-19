using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MOUNB.Models
{
    // Читательский абонемент и его правила
    public class LibrarySubscriptions
    {
        // Идентификатор абонемента
        public int Id { get; set; }

        // Название абонемента
        [Required]
        [MaxLength(10, ErrorMessage = "Превышена максимальная длина записи")]
        [Display(Name = "Название абонемента")]
        public string Name { get; set; }

        // Сколько книг может хранить дома читатель
        [Required]
        [Display(Name = "Кол-во книг")]
        public int BooksCount { get; set; }

        // Сколько раз читатель может перенести дату возврата
        // каждой  взятой книги
        [Required]
        [Display(Name = "Кол-во продлений")]
        public int ExtensionsCount { get; set; }
    }
}
