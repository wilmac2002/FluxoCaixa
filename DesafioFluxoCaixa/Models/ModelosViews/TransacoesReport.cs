using DesafioFluxoCaixa.Models.Transacoes;
using System;
using System.Collections;

namespace DesafioFluxoCaixa.Models
{
    public class TransacoesReport
    {
        public FiltroPesquisaTransacoes FiltroPesquisaTransacoes { get; set; }
        public IEnumerable Deposits { get; set; }
        public IEnumerable Withdraws { get; set; }

        public TransacoesReport (FiltroPesquisaTransacoes filtro, IEnumerable depositos, IEnumerable saques)
        {
            this.FiltroPesquisaTransacoes = filtro;
            this.Deposits = depositos;
            this.Withdraws = saques;
        }
    }
}
