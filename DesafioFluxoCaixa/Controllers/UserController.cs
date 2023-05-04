using DesafioFluxoCaixa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NHibernate;
using DesafioFluxoCaixa.CRUD;
using DesafioFluxoCaixa.Models.Pessoas;
using System.Text.Json;
using ISession = NHibernate.ISession;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using DesafioFluxoCaixa.Models.Contas;
using DesafioFluxoCaixa.Models.Transacoes;
using System;

namespace DesafioFluxoCaixa.Controllers
{
    public class UserController:Controller
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _contxt;
        private readonly ISession _session;
        private readonly PersonRep PersonCRUD;
        private readonly WithdrawRep WithdrawCRUD;
        private readonly DepositRep DepositCRUD;
        private readonly AccountRep AccountCRUD;

        private readonly PersonRep personCRUD;
        public UserController(ISession session, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _session = session;
            _contxt = httpContextAccessor;
            PersonCRUD = new PersonRep(_session);
            AccountCRUD = new AccountRep(_session);
            DepositCRUD = new DepositRep(_session);
            WithdrawCRUD = new WithdrawRep(_session);
            personCRUD = new PersonRep(_session);
        }

        public async Task<ActionResult> MandarContaDeposito()
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));

            Account conta = await AccountCRUD.FindByID(User.Id);
            Person person = await PersonCRUD.FindByID(User.Id);
            Deposit deposito = new Deposit();
            deposito.Conta = conta;
            deposito.Date = DateTime.Now;

            ViewBag.Saldo = conta.Balance;
            ViewBag.NomeCliente = person.Name;

            return View("Deposito", deposito);
        }
        public async Task<ActionResult> MandarContaSaque()
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));

            Account conta = await AccountCRUD.FindByID(User.Id);
            Person person = await PersonCRUD.FindByID(User.Id);
            Withdraw saque = new();
            saque.Conta = conta;
            saque.Date = DateTime.Now;

            ViewBag.NomeCliente = person.Name;
            ViewBag.Saldo = conta.Balance;

            return View("Saque", saque);
        }
        public async Task<ActionResult> AlterarConta()
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            var conta = await AccountCRUD.FindByID(User.Id);

            return View(conta);
        }

        public async Task<ActionResult> AlterarPessoa()
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            var person = await personCRUD.FindByID(User.Id);

            return View(person);
        }

        public IActionResult TransacoesPorId()
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));

            return RedirectToAction("MostrarPorId", "Reports", User, User.Id.ToString());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
