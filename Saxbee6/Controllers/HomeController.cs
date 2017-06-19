using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaxBee6.Data;

namespace SaxBee6.Controllers
{

    public class HomeController : Controller
    {
        private IApplicationRepository _repository;

        public HomeController(IApplicationRepository repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            return View();
        }
      
   
    }
}
