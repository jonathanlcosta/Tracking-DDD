using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces
{
    public interface IItemColetaMercadoriasServico
    {
    ItemColetaMercadoria Instanciar(Produto produto, int quantidade, ColetaMercadoria coletaMercadoria, decimal valorFrete);
    
     decimal CalcularFrete(Produto produto, Cliente cliente, int quantidade);
    }
}