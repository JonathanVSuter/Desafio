using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistroCidades.Data;
using RegistroCidades.Models;

namespace RegistroCidades.Controllers
{
    public class CidadeController : Controller
    {
        CidadeDAO contextoCidade = new CidadeDAO();
        public CidadeController() { }
        
        public IActionResult Index()
        {
            return View(GetAll());
        }
        public List<Cidade> GetAll()
        {
            return contextoCidade.GetAll();
        }         
        public IActionResult Criar()
        {
            return View();
        }        
        public IActionResult Editar(int? id)
        {
            Cidade cidade;
            if (id == null)
            {
                return NotFound();
            }
            cidade = contextoCidade.GetById((int)id);
            return View(cidade);
        }
        public List<UF> GetUFs()
        {            
            return new UFDAO().GetAll();
        } 
        public List<Cidade> GetCidades(string nome)
        {
            return contextoCidade.GetAll().Where(x => x.Nome == nome).ToList();
        }
        public List<Regiao> GetRegioes()
        {
            return new RegiaoDAO().GetAll();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar([Bind("CodIBGE,Nome,Latitude,Longitude,UF,Regiao")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {                
                contextoCidade.Add(cidade);
                return RedirectToAction(nameof(Index));
            }
            return View(Index());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, [Bind("CodIBGE,Nome,Latitude,Longitude,UF,Regiao")] Cidade cidade)
        {
            if (id != cidade.CodIBGE)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contextoCidade.Update(id, cidade);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (!CidadeExists(cidade.CodIBGE))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cidade);
        }
        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cidade item = contextoCidade.GetAll().FirstOrDefault(m=>m.CodIBGE == id);
            contextoCidade.Delete(item);
            return View(Index());
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            Cidade item = contextoCidade.GetById(id);
            contextoCidade.Delete(item);
                return RedirectToAction(nameof(Index));
        }
        private bool CidadeExists(int id)
        {
            return contextoCidade.GetAll().Any(e => e.CodIBGE == id);
        }
    }
}
