using System.ComponentModel.DataAnnotations;

namespace HumanResources.ViewModels
{
    public class CreateTaskViewModel
    {

        [Display(Name = "Görev Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        public List<int> MemberIds { get; set; }

    }
}
