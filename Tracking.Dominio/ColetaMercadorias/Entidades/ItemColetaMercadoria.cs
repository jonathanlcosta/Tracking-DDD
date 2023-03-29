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
    public virtual int Quantidade { get; protected set; }
    public virtual decimal ValorFrete { get; protected set; }


    public ItemColetaMercadoria()
    {
        
    }

    public ItemColetaMercadoria(Produto produto, int quantidade, ColetaMercadoria coletaMercadoria,
   decimal ValorFrete )
    {
        SetProduto(produto);
        SetQuantidade(quantidade);
        SetColetaMercadoria(coletaMercadoria);
        SetFrete(ValorFrete);
    }

     public virtual void SetProduto(Produto? produto)
        {
            if (produto == null)
                throw new ArgumentNullException("O campo de produto não pode ser nulo.");
            Produto = produto;
        }

    public virtual void SetColetaMercadoria(ColetaMercadoria? coletaMercadoria)
        {
            if (coletaMercadoria == null)
                throw new ArgumentNullException("O campo de coleta mercadoria não pode ser nulo.");
            ColetaMercadoria = coletaMercadoria;
        }


     public virtual void SetQuantidade(int quantidade)
        {
            if(quantidade <= 0)
            {
                throw new Exception("A quantidade não pode ser menor ou igual a zero");
            }
            Quantidade = quantidade;
        }

    public virtual void SetFrete(decimal frete)
    {
        ValorFrete = frete;
    }

    public virtual decimal CalcularFrete(decimal altura, decimal largura, decimal custoPorPeso, decimal seguro)
    {
        var pesoCubado = altura * largura * 300;
        ValorFrete = pesoCubado * custoPorPeso + seguro;
        var valorTotal = ValorFrete * Quantidade;
        return valorTotal;

    }

     public virtual void CalcularFreteTotal(decimal valorFrete, int quantidade)
    {
        ValorFrete = valorFrete * quantidade;
    }

}

}