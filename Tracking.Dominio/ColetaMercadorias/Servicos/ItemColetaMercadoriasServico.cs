using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Servicos.Interfaces;

namespace Tracking.Dominio.ColetaMercadorias.Servicos
{
    public class ItemColetaMercadoriasServico : IItemColetaMercadoriasServico
    {   
        public ItemColetaMercadoria Instanciar(Produto produto, int quantidade, ColetaMercadoria coletaMercadoria, decimal valorFrete)
        {
            return new ItemColetaMercadoria(produto, quantidade, coletaMercadoria, valorFrete );
        }

         public decimal CalcularFrete(Produto produto, Cliente cliente, int quantidade)
    {

        var pesoCubado = produto.Altura * produto.Largura * 300;
        var valorFrete = pesoCubado * cliente.CustoPorPeso + cliente.Seguro;
        var valorTotal = valorFrete * quantidade;
        return valorTotal;



    }

    }
}