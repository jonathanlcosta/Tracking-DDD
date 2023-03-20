using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces
{
    public interface IItemColetaMercadoriasServico
    {
        ItemColetaMercadoria Instanciar(Produto produto, FreteOpcoesEnum opcao, int quantidade, ColetaMercadoria coletaMercadoria, decimal valorProduto, string descricao, decimal dimensoes, decimal ValorFrete);
    decimal CalcularFrete(FreteOpcoesEnum opcao, decimal altura, decimal largura, decimal profundidade, decimal valorProduto, int quantidade);
    }
}