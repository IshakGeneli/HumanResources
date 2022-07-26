using System.ComponentModel.DataAnnotations;

namespace HumanResources.Enums
{
    public enum PermitType
    {
        [Display(Name = "Mazeretli")]
        Excused = 1,

        [Display(Name = "Mazeretsiz")]
        Unexcused = 2,

        [Display(Name = "İzinli")]
        OnLeave = 3
    }

}
