using ClinicManagement.Web.Models;
using ClinicManagement.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Services
{
    public interface IApplicationUserServise
    {
        List<UserViewModel> GetUsers();
        ApplicationUser GetUser(string id);
        void Update(UserViewModel entity);
        List<ApplicationUser> GetAllWithInclude();


    }
}
