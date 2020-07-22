using mvc_test.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc_test.Models
{
    public class DisplayComputerViewModel
    {
        public string Title { get; set; }
        public List<Computer> Computers{ get; set; }

       
    }
}
