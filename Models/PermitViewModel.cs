using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class PermitViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string EmployeeFullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "İzin Tipi")]
        public string PermitType { get; set; }
    }
}
