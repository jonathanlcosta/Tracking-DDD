using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Produtos.Response;

namespace Tracking.DataTransfer.ColetaMercadorias.Response
{
    public class ItemColetaMercadoriaResponse
    {
    public int Id { get; set; }
    public int IdColetaMercadoria { get; set; }
    public ProdutoResponse? Produto { get; set; }
    public string? Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorProduto { get; set; }
    public decimal Dimensoes { get; set; }
    public decimal ValorFrete { get; set; }
    }
}