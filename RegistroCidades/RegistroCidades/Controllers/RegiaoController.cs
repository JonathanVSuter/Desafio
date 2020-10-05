using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroCidades.Data;
using RegistroCidades.Models;

namespace RegistroCidades.Controllers
{
    public class RegiaoController : Controller
    {
        private RegiaoDAO RegiaoDAO = new RegiaoDAO();
        public IActionResult Index()
        {
            return View();
        }
        public List<Regiao> GetAll()
        {
            return RegiaoDAO.GetAll();
        }
    }
}
