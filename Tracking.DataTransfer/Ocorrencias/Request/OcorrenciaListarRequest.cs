using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.Ocorrencias.Request
{
    public class OcorrenciaListarRequest
    {
        public int Id { get; set; }
        public string? NotaFiscal { get; set; }
        public int? IdCliente { get; set; }
        public int? IdTransportadora { get; set; }
        public string? Observacao { get; set; }
        public DateTime Data { get; set; }
    }
}