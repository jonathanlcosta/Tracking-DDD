using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
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
        public ItemColetaMercadoria Instanciar(Produto produto, FreteOpcoesEnum opcao, int quantidade, ColetaMercadoria coletaMercadoria, decimal valorProduto, string descricao, decimal dimensoes, decimal ValorFrete)
        {
            return new ItemColetaMercadoria(produto, opcao, quantidade, coletaMercadoria, descricao, valorProduto, descricao, dimensoes, ValorFrete );
        }
    }
}