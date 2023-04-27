using DesafioFluxoCaixa.Models.Pessoas;
using DesafioFluxoCaixa.Models.Transacoes;
using System;
using System.Collections.Generic;

namespace DesafioFluxoCaixa.Models.Contas
{
    public class Account
    {
        public virtual int Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual IList<Withdraw> Saques { get; set; }
        public virtual IList<Deposit> Depositos { get; set; }
        public virtual double Balance { get; set; }
        public virtual double Limit { get; set; }
        public virtual double MinimalBalance { get; set; }
        public virtual string Name
        {
            get
            {
                return this.Person?.Name;
            }
        }

        public Account()
        { }

        public Account(double minimal_balance)
        {
            Balance = 0;
            MinimalBalance = minimal_balance > 0 ? minimal_balance : throw new ArgumentException("O Saldo Mínimo não pode ser igual ou menor que 0");
            Limit = 0;
        }

        public Account(double minimal_balance, double limit)
        {
            Balance = 0;
            MinimalBalance = minimal_balance > 0 ? minimal_balance : throw new ArgumentException("O Saldo Mínimo não pode ser igual ou menor que 0");
            Limit = limit >= 0 ? minimal_balance : throw new ArgumentException("O Saldo Mínimo não pode ser menor que 0");
        }
        public virtual bool IsUnderMinimal()
        {
            return this.Balance < this.MinimalBalance;
        }

    }
}
