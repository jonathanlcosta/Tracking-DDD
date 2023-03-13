using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Transportadoras.Request;
using Tracking.DataTransfer.Transportadoras.Response;
using Tracking.Dominio.Generico.Repositorios;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Aplicacao.Transportadoras.Servicos.Interfaces
{
    public interface ITransportadorasAppServico
    {
        IList<TransportadoraResponse> ListarPaginado(int pagina, int tamanho, int? order = 0, string? search = null);
        TransportadoraResponse Recuperar(int codigoTransportadora);
        TransportadoraResponse Inserir(TransportadoraInserirRequest request);
        TransportadoraResponse Editar(int codigoTransportadora, TransportadoraEditarRequest request);
        void Excluir(int codigo);
    }
}