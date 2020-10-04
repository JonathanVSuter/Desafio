using System;
using System.Collections.Generic;
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
        public List<Regiao> GetRegioesUF(int id_UF)
        {
            RegiaoDAO contextoRegiao = new RegiaoDAO();
            return contextoRegiao.GetAll().Where(x=>x.UF==id_UF).ToList();
        }
        public UF GetUFDaRegiao(int id_UF)
        {
            return new UFDAO().GetById(id_UF);
        }
        public List<UF> GetUFs()
        {            
            return new UFDAO().GetAll();
        }
        public List<Cidade> GetCidades(string nome)
        {
            return contextoCidade.GetAll().Where(x => x.Nome.Contains(nome)).ToList();
        }
        public List<Regiao> GetRegioes()
        {
            return new RegiaoDAO().GetAll();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CodIBGE,Nome,Latitude,Longitude,UF,Regiao")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                contextoCidade.Add(cidade);
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

            return View();
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
