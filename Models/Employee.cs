using HumanResources.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [ValidateNever]
        public AppUser User { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Departman")]
        public string? Department { get; set; }

        [Display(Name = "İşe Başlama Tarihi")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public int RemainPermitCount { get; set; }

        public List<Permit>? Permits { get; set; }

        public List<Task>? Tasks { get; set; }

    }
}
