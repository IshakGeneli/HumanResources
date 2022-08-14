using System.ComponentModel.DataAnnotations;

namespace HumanResources.Enums
{
    public enum TaskLabel
    {
        [Display(Name = "Yapılacak")]
        ToDo = 1,

        [Display(Name = "Yapılıyor")]
        InProgress = 2,

        [Display(Name = "Tamamlandı")]
        Done = 3,
    }
}
