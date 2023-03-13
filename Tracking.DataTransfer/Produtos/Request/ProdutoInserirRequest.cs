using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.Produtos.Request
{
    public class ProdutoInserirRequest
    {
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double Largura { get; set; }
    public double Comprimento { get; set; }
    }
}