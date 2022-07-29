﻿using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string? FullName { get; set; }

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

    }
}
