using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc_test.DbContexts;
using mvc_test.Models;

namespace mvc_test.Controllers
{
    [Authorize(Policy ="Add")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestDbContext context;

        public HomeController(ILogger<HomeController> logger, TestDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var computers = context.Computers;
            var computerModel = new DisplayComputerViewModel
            {
                Title = "All Computers",
                Computers = computers.ToList()
            };
            return View(computerModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ComputerViewModel model)
        {
            var computer = new Computer
            {
                Name = model.Name,
                Price = model.Price
            };

            context.Computers.Add(computer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == id);
            if(computer != null)
            {
                var model = new ComputerViewModel
                {
                    Name = computer.Name,
                    Price = computer.Price
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(ComputerViewModel model)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == model.Id);

            computer.Name = model.Name;
            computer.Price = model.Price;

            context.Computers.Update(computer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == id);
            context.Computers.Remove(computer);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
