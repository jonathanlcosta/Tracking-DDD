using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Request;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Clientes.Servicos.Interfaces
{
    public interface IPessoaFisicasAppServico
    {
        PaginacaoConsulta<PessoaFisicasResponse> Listar(int? pagina, int quantidade, PessoaFisicasListarRequest pessoaFisicasListarRequest);
        PessoaFisicasResponse Recuperar(int codigo);
        PessoaFisicasResponse Inserir(PessoaFisicasCadastroRequest pessoaFisicasCadastroRequest);
        PessoaFisicasResponse Editar(int codigo, PessoaFisicasEditarRequest pessoaFisicasEditarRequest);
        void Excluir(int codigo);
    }
}