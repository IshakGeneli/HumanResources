using System.ComponentModel.DataAnnotations;

namespace HumanResources.Enums
{

    public enum Months
    {

        [Display(Name = "Ocak")]
        January = 0,

        [Display(Name = "Şubat")]
        February = 1,

        [Display(Name = "Mart")]
        March = 2,

        [Display(Name = "Nisan")]
        April = 3,

        [Display(Name = "Mayıs")]
        May = 4,

        [Display(Name = "Haziran")]
        June = 5,

        [Display(Name = "Temmuz")]
        July = 6,

        [Display(Name = "Ağustos")]
        August = 7,

        [Display(Name = "Eylül")]
        September = 8,

        [Display(Name = "Ekim")]
        October = 9,

        [Display(Name = "Kasım")]
        November = 10,

        [Display(Name = "Aralık")]
        December = 11,

    }
}
