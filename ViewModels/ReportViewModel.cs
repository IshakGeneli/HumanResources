using HumanResources.Enums;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.ViewModels
{
    public class ReportViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Rapor Sahibi")]
        public string OwnerFullName { get; set; }

        [Display(Name = "Proje İsmi")]
        public ProjectName ProjectName { get; set; }

        [Display(Name = "Proje Tipi")]
        public ProjectType ProjectType { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Yüzde")]
        public byte Percentage { get; set; }

        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        public bool IsLateEntry { get; set; }
    }
}
