using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_test.Other
{
    public static class TestClaims
    {
        public static List<string> Claims { get; set; } = new List<string>
        {
            "add",
            "delete",
            "update"
        };
    }
}
