using HumanResources.Enums;
using HumanResources.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        public string OwnerId { get; set; }

        [ValidateNever]
        [Display(Name = "Rapor Sahibi")]
        public AppUser Owner { get; set; }

        [Display(Name = "Proje İsmi")]
        public ProjectName ProjectName { get; set; }

        [Display(Name = "Proje Tipi")]
        public ProjectType ProjectType { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Yüzde")]
        public byte Percentage { get; set; }

        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; } = DateTime.Today;

        [Display(Name = "Rapor Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; } = DateTime.Today;
    }
}
