using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.Ocorrencias.Entidades;

namespace Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces
{
    public interface IColetaMercadoriasServico
    {
        ColetaMercadoria Validar(int codigo);
        ColetaMercadoria Instanciar(string notaFiscal, string pedidoCompra, int codCliente, int codTransportadora,
     string nomeFantasia);
        ColetaMercadoria Atualizar(int codigo, string notaFiscal, string pedidoCompra, int? codCliente, int? codTransportadora,
     string nomeFantasia);
        ColetaMercadoria Inserir(ColetaMercadoria coletaMercadoria);
        void AdicionarItem(ColetaMercadoria coletaMercadoria, ItemColetaMercadoria? itemColetaMercadoria);
        void AdicionarItem(ColetaMercadoria coletaMercadoria, IList<ItemColetaMercadoria> itensColetaMercadoria);
        void Excluir(ColetaMercadoria coletaMercadoria);
    }
}