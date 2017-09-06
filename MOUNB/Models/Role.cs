using System.ComponentModel.DataAnnotations;

namespace MOUNB.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Роль пользователя")]
        public string Name { get; set; }
    } // Конец класса
} // Конец пронстранста