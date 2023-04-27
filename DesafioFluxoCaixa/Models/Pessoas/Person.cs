using DesafioFluxoCaixa.Models.Contas;
using System;

namespace DesafioFluxoCaixa.Models.Pessoas
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Account Conta { get; set; }
        public virtual string Email { get; set; }
        public virtual double Salario { get; set; }
        public virtual string Password { get; set; }

        public Person()
        { }
        public Person (string name, string email, string password, double salario, Account conta)
        {

            this.Name = name ?? throw new ArgumentNullException("O campo Nome não pode ser nulo");
            this.Password = password ?? throw new ArgumentNullException("O campo Senha não pode ser nulo");
            this.Email = email ?? throw new ArgumentNullException("O campo Email não pode ser nulo");
            this.Salario = salario >= 0 ? salario : throw new ArgumentException("O campo Salario não pode menor que 0");
            this.Conta = conta ?? throw new ArgumentNullException("O campo Conta não pode ser nulo");
        }

        public Person(int id, string name, string email, string password, double salario)
        {
            this.Id = id > 0 ? id : throw new ArgumentException("O campo ID não pode menor que 0");
            this.Name = name ?? throw new ArgumentException("O campo Nome não pode ser nulo");
            this.Password = password ?? throw new ArgumentException("O campo Senha não pode ser nulo");
            this.Email = email ?? throw new ArgumentException("O campo Email não pode ser nulo");
            this.Salario = salario >= 0 ? salario : throw new ArgumentException("O campo Salario não pode menor que 0");
        }

    }
}
