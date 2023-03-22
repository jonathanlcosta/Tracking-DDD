using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Ocorrencias.Request;
using Tracking.DataTransfer.Ocorrencias.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Ocorrencias.Servicos.Interfaces
{
    public interface IOcorrenciasAppServico
    {
        PaginacaoConsulta<OcorrenciaResponse> Listar(int? pagina, int quantidade, OcorrenciaListarRequest ocorrenciaListarRequest);
        OcorrenciaResponse Recuperar(int codigo);
        OcorrenciaResponse Inserir(OcorrenciaInserirRequest request);
        OcorrenciaResponse Editar(int codigo, OcorrenciaEditarRequest request);
        void Excluir(int codigo);
    }
}