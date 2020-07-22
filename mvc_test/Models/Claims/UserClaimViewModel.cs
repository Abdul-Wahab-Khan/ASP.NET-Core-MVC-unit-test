using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mvc_test.Models.Claims
{
    public class UserClaimViewModel
    {
        public string UserId { get; set; }
        public string Claim { get; set; }
        public IList<Claim> AssignedClaims { get; set; }
        public IList<string> Claims { get; set; }

        public UserClaimViewModel()
        {
            AssignedClaims = new List<Claim>();
            Claims = new List<string>();
        }
    }
}
