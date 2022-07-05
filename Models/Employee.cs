using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Display(Name = "Yaş")]
        public int Age { get; set; }

        [Display(Name = "Departman")]
        public string Department { get; set; }

        [Display(Name = "İşe Başlama Tarihi")]
        [DataType(DataType.Date)]
        [BindNever]
        public DateTime HireDate { get; set; }

        [Display(Name = "Yıllık İzin Adedi")]
        public int AnnualPermitCount { get; set; }
    }
}
