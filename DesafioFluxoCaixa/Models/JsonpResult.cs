using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.Models
{
    public class JsonpResult:ActionResult
    {
        #region Propriedades
        public object Data { get; set; }
        public string Msg { get; set; }

        public int QuantidadeRegistros { get; set; }
        #endregion

        #region Construtores
        public JsonpResult()
        {
        }

        public JsonpResult(object data)
        {
            this.Data = data;
        }
        public JsonpResult(object data, string msg)
        {
            this.Data = data;
            this.Msg = msg;
        }
        #endregion
    }
}
