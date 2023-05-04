using DesafioFluxoCaixa.Models;
using Microsoft.AspNetCore.Mvc;
using DesafioFluxoCaixa.CRUD;
using System;
using DesafioFluxoCaixa.Models.Pessoas;
using System.Threading.Tasks;
using DesafioFluxoCaixa.Models.Contas;
using DesafioFluxoCaixa.Models.Transacoes;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ISession = NHibernate.ISession;
using System.Reflection;
using DesafioFluxoCaixa.Models.Excecoes;

namespace DesafioFluxoCaixa.Controllers
{
    public class CRUDController : Controller
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _contxt;
        private readonly ISession _session;
        private readonly PersonRep PersonCRUD;
        private readonly WithdrawRep WithdrawCRUD;
        private readonly DepositRep DepositCRUD;
        private readonly AccountRep AccountCRUD;
        private ServicoEmail servicoEmail;

        public CRUDController(ISession session, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, ServicoEmail _servicoEmail)
        {
            _session = session;
            _contxt = httpContextAccessor;
            PersonCRUD = new PersonRep(_session);
            AccountCRUD = new AccountRep(_session);
            DepositCRUD = new DepositRep(_session);
            WithdrawCRUD = new WithdrawRep(_session);
            servicoEmail = _servicoEmail;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public async Task<JsonResult> AlterarConta(int id)
        {
            Account account = await AccountCRUD.FindByID(id);
            return Json(account);
        }

        public async Task<JsonResult> DeletarDados(int id)
        {
            await PersonCRUD.Remove(id);

            return Json(true);
        }

        public IActionResult SelecionarConta()
        {
            return View(AccountCRUD.FindAll());
        }

        public IActionResult SelecionarContaDeposito()
        {
            return View(AccountCRUD.FindAll());
        }

        public IActionResult SelecionarContaSaque()
        {
            return View(AccountCRUD.FindAll());
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarConta(Account conta)
        {
            Account Conta = new(conta.MinimalBalance, conta.Limit);

            var account = await AccountCRUD.FindByID(conta.Id);

            account.Limit = Conta.Limit;
            account.MinimalBalance = Conta.MinimalBalance;

            await AccountCRUD.Update(account);

            return Json(account);
        }

        [HttpPost]
        public async Task<JsonResult> Sacar(DadosTransacao DadosSaque)
        {
            Person person = JsonSerializer.Deserialize<Person>(HttpContext.Session.GetString("User"));
            var conta = await AccountCRUD.FindByID(Convert.ToInt32(DadosSaque.ContaId));
            person.Conta = conta;

            Withdraw saque = new(conta, Convert.ToDouble(DadosSaque.Valor.Replace('.',',')), DadosSaque.Descricao, DateTime.Now);

            await WithdrawCRUD.Add(saque);
            await AccountCRUD.Update(conta);

            string msg = null;
            if (conta.IsUnderMinimal())
            {
                msg = "Cuidado! Sua conta está abaixo do mínimo cadastrado.";
                servicoEmail.EnviarEmail(new[] { person.Email },
                    "Saldo abaixo do mínimo!",
                     person.Name,
                    $"Cuidado. Sua conta em Desafio Fluxo de Caixa está com o saldo em R${conta.MinimalBalance - conta.Balance} abaixo do mínimo cadastrado.");
            }

            JsonpResult Retorno = new(person, msg);
            return Json(Retorno);
        }

        [HttpPost]
        public async Task<JsonResult> Depositar(DadosTransacao deposit)
        {
            Person person = JsonSerializer.Deserialize<Person>(HttpContext.Session.GetString("User"));

            var conta = await AccountCRUD.FindByID(Convert.ToInt32(deposit.ContaId));
            Deposit deposit_atualizado = new(conta, Convert.ToDouble(deposit.Valor.Replace('.',',')), deposit.Descricao, DateTime.Now);
            await DepositCRUD.Add(deposit_atualizado);
            await AccountCRUD.Update(conta);

            return Json(person);
        }

        [HttpGet]
        public async Task<JsonResult> AlterarPessoa(int id)
        {
            Person person = await PersonCRUD.FindByID(id);

            return Json(person);
        }

        // POST: Persons/Edit
        [HttpPost]
        public async Task<JsonResult> AtualizarPessoa(Person person)
        {
            Person pessoa = new(person.Id, person.Name, person.Email, person.Password, person.Salario);
            await PersonCRUD.Update(pessoa);

            return Json(person);
        }

        public IActionResult SelecionarPessoa()
        {
            return View(PersonCRUD.FindAll());
        }

        // POST: Persons/Create
        [HttpPost]
        public async Task<JsonResult> SendCadastro(Cadastro data)
        {
            Person person = new Person
                (
                    data.Nome, data.Email,
                    data.Senha, Convert.ToDouble(data.Salario),
                    new Account(Convert.ToDouble(data.SaldoMinimo))
                );

            person.Conta.Person = person;
            await PersonCRUD.Add(person);
            await AccountCRUD.Add(person.Conta);

            person.Conta = null;
            string jsonString = JsonSerializer.Serialize(person);
            _contxt.HttpContext.Session.SetString("User", jsonString);

            return Json(1);
        }
    }
}
