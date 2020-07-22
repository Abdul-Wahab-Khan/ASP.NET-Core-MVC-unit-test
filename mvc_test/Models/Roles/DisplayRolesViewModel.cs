using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_test.Models.Roles
{
    public class DisplayRolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }
}
