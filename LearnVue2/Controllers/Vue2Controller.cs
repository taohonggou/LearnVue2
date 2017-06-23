using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnVue2.Controllers
{
    public class Vue2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}