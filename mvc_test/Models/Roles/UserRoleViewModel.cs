using Microsoft.AspNetCore.Identity;
using mvc_test.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_test.Models.Roles
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public List<AppUser> NotAssignedUsers { get; set; }
        public List<AppUser> AssignedUsers { get; set; }

        public UserRoleViewModel()
        {
            NotAssignedUsers = new List<AppUser>();
            AssignedUsers = new List<AppUser>();
        }
    }
}
