using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.ColetaMercadorias.Repositorios;
using Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Servicos.Interfaces;

namespace Tracking.Dominio.ColetaMercadorias.Servicos
{
    public class ItemColetaMercadoriasServico : IItemColetaMercadoriasServico
    {   private readonly IProdutosServico produtosServico;
        public ItemColetaMercadoriasServico(IProdutosServico produtosServico)
        {
            this.produtosServico = produtosServico;
        }
        public decimal CalcularFrete(FreteOpcoesEnum opcao, decimal altura, decimal largura, decimal profundidade, decimal valorProduto, int quantidade)
        {
    decimal pesoCubado = altura * largura * 300;
    decimal custoPorPeso = 0;
    decimal seguro = 0;

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

        public ItemColetaMercadoria Instanciar(Produto produto, FreteOpcoesEnum opcao, int quantidade, ColetaMercadoria coletaMercadoria, decimal valorProduto, string descricao, decimal dimensoes, decimal ValorFrete)
        {
            return new ItemColetaMercadoria(produto, opcao, quantidade, coletaMercadoria, descricao, valorProduto, descricao, dimensoes, ValorFrete );
        }
    }
}