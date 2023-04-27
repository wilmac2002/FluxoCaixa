using DesafioFluxoCaixa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NHibernate;
using DesafioFluxoCaixa.CRUD;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DesafioFluxoCaixa.Models.Transacoes;
using System.Text.Json;
using DesafioFluxoCaixa.Models.Pessoas;
using Microsoft.AspNetCore.Http;
using ISession = NHibernate.ISession;
using System.Collections.Generic;

namespace DesafioFluxoCaixa.Controllers
{
    public class ReportsController:Controller
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _contxt;
        private readonly ISession _session;
        private readonly PersonCRUD PersonCRUD;
        private readonly WithdrawCRUD WithdrawCRUD;
        private readonly DepositCRUD DepositCRUD;
        private readonly AccountCRUD AccountCRUD;

        public ReportsController(ISession session, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _contxt = httpContextAccessor;
            _session = session;
            PersonCRUD = new PersonCRUD(_session);
            WithdrawCRUD = new WithdrawCRUD(_session);
            DepositCRUD = new DepositCRUD(_session);
            AccountCRUD = new AccountCRUD(_session);
        }

        public IActionResult SelecionarContaReport()
        {
            return View(AccountCRUD.FindAll());
        }
        public IActionResult MostrarPorId(int id)
        {
            IEnumerable deposits = DepositCRUD.FindAllByAccount(id);
            IEnumerable withdraws = WithdrawCRUD.FindAllByAccount(id);

            return View("Transacoes", new TransacoesReport(new FiltroPesquisaTransacoes(), deposits, withdraws));
        }

        public IActionResult Mostrar7Dias(int id)
        {
            IEnumerable deposits = DepositCRUD.FindAllLast7Days(id);
            IEnumerable withdraws = WithdrawCRUD.FindAllLast7Days(id);

            return View("Transacoes", new TransacoesReport(new FiltroPesquisaTransacoes(), deposits, withdraws));
        }

        public IActionResult Mostrar15Dias(int id)
        {
            IEnumerable deposits = DepositCRUD.FindAllLast15Days(id);
            IEnumerable withdraws = WithdrawCRUD.FindAllLast15Days(id);

            return View("Transacoes", new TransacoesReport(new FiltroPesquisaTransacoes(), deposits, withdraws));
        }

        public IActionResult Mostrar30Dias(int id)
        {
            IEnumerable deposits = DepositCRUD.FindAllLast30Days(id);
            IEnumerable withdraws = WithdrawCRUD.FindAllLast30Days(id);

            return View("Transacoes", new TransacoesReport(new FiltroPesquisaTransacoes(), deposits, withdraws));
        }

        [HttpPost]
        public IActionResult FiltrarPeriodo(TransacoesReport viewModel)
        {
            var User = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            viewModel.FiltroPesquisaTransacoes.UserId = User.Id;

            viewModel.Deposits = DepositCRUD.FindAllByDates(viewModel.FiltroPesquisaTransacoes);
            viewModel.Withdraws = WithdrawCRUD.FindAllByDates(viewModel.FiltroPesquisaTransacoes);

            return View("Transacoes", viewModel);
        }

        public IActionResult Pessoas()
        {
            return View(PersonCRUD.FindAll());
        }

        public IActionResult Contas()
        {
            return View(AccountCRUD.FindAll());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
