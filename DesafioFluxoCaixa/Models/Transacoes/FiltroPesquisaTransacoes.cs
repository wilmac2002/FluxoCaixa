using System;

namespace DesafioFluxoCaixa.Models.Transacoes
{
    public class FiltroPesquisaTransacoes
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int UserId { get; set; }

        public FiltroPesquisaTransacoes()
        {
            this.DataInicial = DateTime.Now;
            this.DataFinal = DateTime.Now;
        }
    }
}
