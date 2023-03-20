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
    public virtual int Id { get; protected set; }
    public virtual ColetaMercadoria? ColetaMercadoria { get; protected set; }
    public virtual Produto? Produto { get; protected set; }
    public virtual FreteOpcoesEnum TipoFrete  { get; protected set; }
    public virtual string? Descricao { get; protected set; }
    public virtual int Quantidade { get; protected set; }
    public virtual decimal ValorProduto { get; protected set; }
    public virtual decimal Dimensoes { get; protected set; }
    public virtual decimal ValorFrete { get; protected set; }


    public ItemColetaMercadoria()
    {
        
    }

    public ItemColetaMercadoria(Produto produto, FreteOpcoesEnum opcao, int quantidade, ColetaMercadoria coletaMercadoria, string Descricao,
    decimal valorProduto, string descricao, decimal dimensoes, decimal ValorFrete )
    {
        SetProduto(produto);
        SetQuantidade(quantidade);
        SetColetaMercadoria(coletaMercadoria);
        SetDescricao(descricao);
        SetValorProduto(valorProduto);
    }

     public virtual void SetProduto(Produto? produto)
        {
            if (produto == null)
                throw new ArgumentNullException("O campo de produto não pode ser nulo.");
            Produto = produto;
        }
    public virtual void SetValorProduto(decimal valorProduto)
        {
            if(valorProduto <= 0)
            {
                throw new Exception("O valor do produto não pode ser menor ou igual a zero");
            }
            ValorProduto = valorProduto;
        }

    public virtual void SetColetaMercadoria(ColetaMercadoria? coletaMercadoria)
        {
            if (coletaMercadoria == null)
                throw new ArgumentNullException("O campo de coleta mercadoria não pode ser nulo.");
            ColetaMercadoria = coletaMercadoria;
        }

    public virtual void SetDescricao(string? descricao)
        {
            if (String.IsNullOrEmpty(descricao))
                throw new ArgumentNullException("O a descrição não pode ser vazia");
            if (descricao.Length > 100)
                throw new ArgumentOutOfRangeException("A descrição nao pode ter mais que 100 caracteres");
            Descricao = descricao;
        }


     public virtual void SetQuantidade(int quantidade)
        {
            if(quantidade <= 0)
            {
                throw new Exception("A quantidade não pode ser menor ou igual a zero");
            }
            Quantidade = quantidade;
        }

    public decimal CalcularCustoFrete(FreteOpcoesEnum opcao, decimal altura, decimal largura, decimal profundidade, decimal valorProduto, int quantidade)
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
    ValorFrete = frete * quantidade;
    return ValorFrete;
    }
}

}