using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Dominio.ColetaMercadorias.Entidades
{
    public class ItemColetaMercadoria
    {
    public virtual int Id { get; set; }
    public virtual ColetaMercadoria? ColetaMercadoria { get; set; }
    public virtual Produto? Produto { get; set; }
    public virtual FreteOpcoesEnum TipoFrete  { get; set; }
    public virtual  int Quantidade { get; set; }

    public ItemColetaMercadoria()
    {
        
    }

    public ItemColetaMercadoria( Produto produto, FreteOpcoesEnum opcao, int quantidade)
    {
    CalcularCustoFrete(opcao, produto.Altura, produto.Largura, produto.Altura, produto.Preco, quantidade);
    }

    public static decimal CalcularCustoFrete(FreteOpcoesEnum opcao, decimal altura, decimal largura, decimal profundidade, decimal valorProduto, int quantidade)
{
    decimal pesoCubado = altura * largura * 300;
    decimal custoPorPeso;
    decimal seguro;

  switch (opcao)
    {
        case FreteOpcoesEnum.ES_Capital:
            custoPorPeso = 1.80m;
            seguro = valorProduto * 0.005m;
            break;
        case FreteOpcoesEnum.ES_Interior:
            custoPorPeso = 2.10m;
            seguro = valorProduto * 0.005m;
            break;
        case FreteOpcoesEnum.RJ_Capital:
            custoPorPeso = 2.30m;
            seguro = valorProduto * 0.008m;
            break;
        case FreteOpcoesEnum.RJ_Interior:
            custoPorPeso = 2.50m;
            seguro = valorProduto * 0.005m;
            break;
        case FreteOpcoesEnum.BA_Capital:
            custoPorPeso = 2.50m;
            seguro = valorProduto * 0.005m;
            break;
        case FreteOpcoesEnum.BA_Interior:
            custoPorPeso = 2.60m;
            seguro = valorProduto * 0.005m;
            break;
        default:
            throw new ArgumentException("Opção de frete inválida.");
}
     var frete = pesoCubado * custoPorPeso + seguro;
    var valorTotal = frete * quantidade;
    return valorTotal;
    }
}

}