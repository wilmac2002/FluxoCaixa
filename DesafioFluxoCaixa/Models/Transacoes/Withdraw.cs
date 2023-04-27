using DesafioFluxoCaixa.Models.Contas;
using DesafioFluxoCaixa.Models.Excecoes;
using System;

namespace DesafioFluxoCaixa.Models.Transacoes
{
    public class Withdraw : Transaction
    {
        public Withdraw()
        { }

        public Withdraw(Account account, double value, string desc, DateTime date) : base(account, value, desc, date)
        {
            if(value <= 0)
            {
                throw new ArgumentException("O valor do saque não pode menor ou igual a zero");
            }

            else if (value <= account.Balance)
            {
                account.Balance -= value;

                this.Date = date;
                this.Conta = account;
                this.Value = value;
                this.Description = desc;

                if (account.Balance < account.MinimalBalance)
                {
                    Console.WriteLine("Saldo abaixo do mínimo");
                }
            }
            else
            {
                throw new SaldoInsuficienteException("Saldo insuficiente para a operação com o valor solicitado");
            }
        }
    }
}
