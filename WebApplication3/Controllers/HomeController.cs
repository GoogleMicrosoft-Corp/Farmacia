using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly FARMACIAEntities db;

        public HomeController()
        {
            db = new FARMACIAEntities();
            ViewBag.Alerta = string.Empty;
        }
        #region  Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

        #region Módulos
        #region Fornecedores
        public ActionResult CadastroFornecedor(int? pagina)
        {
            var pageSize = 10;
            var pageNumber = (pagina ?? 1);
            var Fornecedor = new List<Fornecedor>();
            var Dados = db.Fornecedor.ToList();
            Fornecedor = Dados.ToList();
            return View(Fornecedor.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CadastrarFornecedor(int? id)
        {
            return id != null ? View(db.Fornecedor.Find(id)) : View();
        }

        [HttpPost]
        public ActionResult CadastrarFornecedor(Fornecedor data)
        {
            db.Fornecedor.Add(data);
            db.SaveChanges();
            return RedirectToAction("CadastroFornecedor");
            ViewBag.Alerta = "Fornecedor Cadastrado com Sucesso!";
        }

        [HttpPost]
        public ActionResult AtualizarFornecedor(Fornecedor data)
        {
            db.Fornecedor.Attach(data);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Alerta = "Fornecedor Editado com Sucesso!";
            return RedirectToAction("CadastroFornecedor");
        }


        public ActionResult ConfirmaRemoverFornecedor(int id)
        {
            var registro = db.Fornecedor.Find(id);
            if (registro != null)
            {
                db.Fornecedor.Remove(registro);
                db.SaveChanges();
            }

            return RedirectToAction("CadastroFornecedor");
        }
        #endregion
        #endregion
    }
}