using HumanResources.Enums;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Görev Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Durum")]
        public TaskLabel Label { get; set; }

        [Display(Name = "Üyeler")]
        public List<Employee>? Members { get; set; }


    }
}
