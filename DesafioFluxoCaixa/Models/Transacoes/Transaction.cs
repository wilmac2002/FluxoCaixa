using DesafioFluxoCaixa.Models.Contas;
using System;

namespace DesafioFluxoCaixa.Models.Transacoes
{
    public abstract class Transaction
    {
        public virtual int Id { get; set; }
        public virtual Account Conta { get; set; }
        public virtual double Value { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Description { get; set; }

        public Transaction()
        { }
        public Transaction (Account conta, double value, string desc, DateTime date)
        {
            this.Date = date;
            this.Conta = conta;
            this.Value = value;
            this.Description = desc;
        }
    }
}
