using mvc_test.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_test.Models.Users
{
    public class DisplayUsersViewModel
    {
        public List<AppUser> Users { get; set; }
    }
}
