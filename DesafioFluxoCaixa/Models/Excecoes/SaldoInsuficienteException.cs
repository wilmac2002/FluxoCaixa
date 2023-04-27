using System;

namespace DesafioFluxoCaixa.Models.Excecoes
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException()
        {

        }
        public SaldoInsuficienteException(string mensagem) : base(mensagem)
        {

        }
    }

}
