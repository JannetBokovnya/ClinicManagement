using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.Services
{
    public class ApplicationUserService : IApplicationUserServise
    {
        private readonly ClinicContext _dbcontext;

        public ApplicationUserService(ClinicContext context)
        {
            _dbcontext = context;
        }

        public List<ApplicationUser> GetAllWithInclude()
        {
            var users = _dbcontext.Users
                .Where(u => u.IsActive == true)
                .ToList();
            return users;
        }

        public ApplicationUser GetUser(string id)
        {
            return _dbcontext.Users.Find(id);
        }

        public List<UserViewModel> GetUsers()
        {
            return (from user in _dbcontext.Users
                    from userRole in user.Roles
                    join role in _dbcontext.Roles
                        on userRole.RoleId equals role.Id
                    select new UserViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Role = role.Name,
                        IsActive = user.IsActive
                    }).ToList();
        }

        public void Update(UserViewModel entity)
        {

            var user = _dbcontext.Users.Find(entity.Id);
            
            //user.UserName = editUser.Email;
            // user.Id = editUser.Id;
            user.Email = entity.Email;
            user.IsActive = entity.IsActive;
            _dbcontext.Entry(user).State = EntityState.Modified;
            _dbcontext.SaveChanges();
        }
    }
}