using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOUNB.Models
{
    public class ReaderViewModel
    {
        // Номер ЧБ
        [Required]
        [Display(Name = "№ Ч.Б.")]
        public int? LibraryCardId { get; set; }

        // Дата рождения
        [Required]
        [Display(Name = "Д. рождения")]
        //[Range(1900, 2003, ErrorMessage = "Недопустимый год")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}