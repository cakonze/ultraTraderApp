using System;
using System.Collections.Generic;
using System.Text;

namespace ultraTrader.Models
{
    class Recomendacao
    {
        public string Ticker { get; set; }
        public decimal ValorEntrada { get; set; }
        public string TipoOrdem{ get; set; }
        public bool IsPro { get; set; }
    }
}
