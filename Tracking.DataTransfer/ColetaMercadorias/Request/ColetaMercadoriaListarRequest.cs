using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.ColetaMercadorias.Request
{
    public class ColetaMercadoriaListarRequest
    {
    public int? Id { get; set; }
    public string? NotaFiscal { get; set; }
    public string? PedidoCompra { get; set; }
    public int? IdCliente { get; set; }
    public int? IdTransportadora { get; set; }
    public string? NomeFantasia { get; set; }
    }
}