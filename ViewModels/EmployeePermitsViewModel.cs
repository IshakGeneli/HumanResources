using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class EmployeePermitsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string EmployeeFullName { get; set; }

        public List<Permit>? Permits { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
