using System.ComponentModel.DataAnnotations;

namespace HumanResources.Enums
{
    public enum ProjectName
    {
        [Display(Name = "Dalisto")]
        Dalisto = 1,

        [Display(Name = "İnsan Kaynakları")]
        InsanKaynaklari = 2,

        [Display(Name = "ContentManage")]
        ContentManage = 3,
    }

    public enum ProjectType
    {
        [Display(Name = "Web Dizayn")]
        WebDesign = 1,

        [Display(Name = "Web Uygulaması")]
        WebApp = 2,

        [Display(Name = "Mobil Uygulama")]
        MobileApp = 3,
    }
}
