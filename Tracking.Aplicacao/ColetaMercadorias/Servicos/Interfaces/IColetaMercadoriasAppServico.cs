using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.ColetaMercadorias.Request;
using Tracking.DataTransfer.ColetaMercadorias.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.ColetaMercadorias.Servicos.Interfaces
{
    public interface IColetaMercadoriasAppServico
    {
        PaginacaoConsulta<ColetaMercadoriaResponse> Listar(int? pagina, int quantidade, ColetaMercadoriaListarRequest coletaMercadoriaListarRequest);
        ColetaMercadoriaResponse Recuperar(int codigoColeta);
        ColetaMercadoriaResponse Inserir(ColetaMercadoriaInserirRequest coletaMercadoriaInserirRequest);
        ColetaMercadoriaResponse Editar(int codigoColeta, ColetaMercadoriaEditarRequest coletaMercadoriaEditarRequest);
        void Excluir(int codigo);
    }
}