using DesafioFluxoCaixa.Models.Contas;
using System;

namespace DesafioFluxoCaixa.Models.Transacoes
{
    public class Deposit : Transaction
    {
       public Deposit()
       { }

        public Deposit(Account account, double value, string desc,DateTime date) : base(account,value,desc,date)
        {
            if(value <= 0)
            {
                throw new ArgumentException("O valor do depósito não pode ser menor ou igual a zero");
            }

            account.Balance += value;

            this.Date = date;
            this.Conta = account;
            this.Value = value;
            this.Description = desc;
        }
    }
}
