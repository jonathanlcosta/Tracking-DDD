using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.Produtos.Response
{
    public class ProdutoResponse
    {
    public int CodigoProduto { get; set; }
    public string? Descricao { get; set; }
    public string? Situacao { get; set; }
    public double Preco { get; set; }
    public double Peso { get; set; }
    public int Altura { get; set; }
    public int Largura { get; set; }
    public int Comprimento { get; set; }
    }
}