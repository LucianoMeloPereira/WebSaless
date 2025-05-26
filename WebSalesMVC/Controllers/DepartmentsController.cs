using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebSalesMVC.Models;

namespace WebSalesMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Deparment> list = new List<Deparment>();
            list.Add(new Deparment { Id = 1, Name = "Markeking" });
            list.Add(new Deparment { Id = 2, Name = "Desenvolvimento" });
            list.Add(new Deparment { Id = 2, Name = "Recursos Humanos" });
            list.Add(new Deparment { Id = 2, Name = "Telemarketing" });



            return View(list);
        }
    }
}
