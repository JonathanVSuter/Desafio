using Microsoft.AspNetCore.Mvc;
using RegistroCidades.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Controllers
{
    public class UFController:Controller
    {
        private UFDAO UFDAO = new UFDAO();
        
        public IActionResult Index()
        {
            return View(UFDAO.GetAll());
        }
    }
}
