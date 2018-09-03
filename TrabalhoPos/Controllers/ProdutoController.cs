using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPos.Models;

namespace TrabalhoPos.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                List<Produto> lista = model.Read();
                return View(lista);

            } //model.Dispose();
        }

        public JsonResult Lista()
        {
            using (ProdutoModel model = new ProdutoModel())
                return Json(model.Read(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto p)
        {
            if (ModelState.IsValid)
            {
                p.UsuarioID = (int)Session["UsuarioId"];
                p.Vendido = false;

                using (ProdutoModel model = new ProdutoModel())
                {
                    model.Create(p);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Erro = "Preencha a descrição da tarefa.";
                return View();
            }
        }

        public ActionResult Detalhes(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                return View(model.Read(id));
            }
        }

        public ActionResult Update(Produto p)
        {
            if (ModelState.IsValid)
            {
                using (ProdutoModel model = new ProdutoModel())
                {
                    int id = p.ProdutoID;
                    decimal valorReceita = p.Preco + (decimal)Session["Receita"];
                    int usuarioId = (int)Session["UsuarioId"];
                    model.Comprar(id, valorReceita, usuarioId);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(p);
            }
        }
    }
}