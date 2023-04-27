using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.Models
{
    public class Cadastro
    {
        public virtual string Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Salario { get; set; }
        public virtual string Saldo { get; set; }
        public virtual string Limite { get; set; }
        public virtual string SaldoMinimo { get; set; }
        public virtual string Senha { get; set; }

    }
}
