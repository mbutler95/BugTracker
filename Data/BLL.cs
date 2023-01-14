using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Data
{
    public class BLL
    {

        public static List<SelectListItem> GetUserNameIds(List<UserModel> users)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Unassigned", Value = null, Selected = true });
            
            foreach (var user in users)
            {
                list.Add(new SelectListItem { Text = user.Name, Value = user._id });
            }
                
            return list;
        }
    }
}
