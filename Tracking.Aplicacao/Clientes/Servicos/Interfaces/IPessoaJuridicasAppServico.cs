using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Request;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Clientes.Servicos.Interfaces
{
    public interface IPessoaJuridicasAppServico
    {
        PaginacaoConsulta<PessoaJuridicasResponse> Listar(int? pagina, int quantidade, PessoaJuridicasListarRequest pessoaJuridicasListarRequest);
        PessoaJuridicasResponse Recuperar(int codigo);
        PessoaJuridicasResponse Inserir(PessoaJuridicasCadastroRequest pessoaJuridicasCadastroRequest);
        PessoaJuridicasResponse Editar(int codigo, PessoaJuridicasEditarRequest pessoaJuridicasEditarRequest);
        void Excluir(int codigo);
    }
}