using System.ComponentModel.DataAnnotations;

namespace HumanResources.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string? FullName { get; set; }

        [Display(Name = "Yaş")]
        public int Age { get; set; }

        [Display(Name = "Departman")]
        public string? Department { get; set; }

        [Display(Name = "İşe Başlama Tarihi")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Yıllık İzin Adedi")]
        public int AnnualPermitCount { get; set; }

        [Display(Name = "Kalan İzin Adedi")]
        public int RemainPermitCount { get; set; }
    }
}
