using HumanResources.Enums;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.ViewModels
{
    public class EditTaskViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Görev Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Durum")]
        public TaskLabel Label { get; set; }

        public List<int> MemberIds { get; set; }
    }
}
