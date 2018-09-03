using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoPos.Models;

namespace TrabalhoPos.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario u)
        {
            if (ModelState.IsValid)
            {
            

                using (UsuarioModel model = new UsuarioModel())
                {
                    model.Create(u);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Erro = "Preencha a descrição da tarefa.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            if (ModelState.IsValid)
            {


                using (UsuarioModel model = new UsuarioModel())
                {

                    var login = model.Login(u.Email, u.Senha);
                    if(login != null)
                    {
                        Session["UsuarioId"] = login.UsuarioID;
                        Session["Nome"] = login.Nome;
                        Session["Receita"] = login.Receita;
                    }
                    return RedirectToAction("Produto/Index");
                }
            }
            else
            {
                ViewBag.Erro = "Preencha a descrição da tarefa.";
                return View();
            }
        }
    }
}