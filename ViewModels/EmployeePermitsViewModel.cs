using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class EmployeePermitsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string? EmployeeFullName { get; set; }
        public int RemainPermitCount { get; set; }

        public List<Permit>? Permits { get; set; }
    }
}
