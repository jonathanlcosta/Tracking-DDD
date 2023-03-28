using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Ocorrencias.Request;

namespace Tracking.DataTransfer.ColetaMercadorias.Request
{
    public class ColetaMercadoriaInserirRequest
    {
    public string? NotaFiscal { get; set; }
    public string? PedidoCompra { get; set; }
    public int IdCliente { get; set; }
    public int IdTransportadora { get; set; }
    public string? NomeFantasia { get; set; }
    public IEnumerable<ItemColetaMercadoriaInserirRequest>? ItensProdutos { get; set; }
    }
}