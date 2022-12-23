using System.Web;
using System.Web.Mvc;

namespace Bastion_Exam_Retying
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
