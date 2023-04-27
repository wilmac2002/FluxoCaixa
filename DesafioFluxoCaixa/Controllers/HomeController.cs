using DesafioFluxoCaixa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DesafioFluxoCaixa.CRUD;
using System;
using Microsoft.AspNetCore.Http;
using ISession = NHibernate.ISession;
using DesafioFluxoCaixa.Models.Pessoas;
using System.Text.Json;
using System.Collections.Generic;

namespace DesafioFluxoCaixa.Controllers
{
    public class HomeController : Controller
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _contxt;
        private readonly ISession _session;

        private readonly PersonCRUD personCRUD;
        public HomeController(ISession session, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _session = session;
            _contxt = httpContextAccessor;

            personCRUD = new PersonCRUD(_session);
        }

        public IActionResult Index()
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            
            ViewBag.Name = User.Name;
            ViewBag.Id = User.Id;

            return View();
        }

        [HttpPost]
        public JsonResult VefLogin (DadosLogin data)
        {
            var person = personCRUD.FindByEmail(data.Email);

            ViewBag.ErroEmail = null;
            ViewBag.ErroSenha = null;

            if (person == null)
            {
                ViewBag.ErroEmail = "Email inválido";
                return Json(1);
            }

            else if (person.Password != data.Password)
            {
                ViewBag.ErroSenha = "Senha inválida";
                return Json(2);
            }

            else
            {
                person.Conta = null;
                string jsonString = JsonSerializer.Serialize(person);
                _contxt.HttpContext.Session.SetString("User", jsonString);

                return Json(3);
            }
        }

        public IActionResult Login ()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _contxt.HttpContext.Session.Remove("User");
            return View("Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
