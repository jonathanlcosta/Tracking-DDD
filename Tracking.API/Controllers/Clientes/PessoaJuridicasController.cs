using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracking.Aplicacao.Clientes.Servicos.Interfaces;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Request;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.API.Controllers.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaJuridicasController : ControllerBase
    {
        private readonly IPessoaJuridicasAppServico pessoaJuridicasAppServico;

        public PessoaJuridicasController(IPessoaJuridicasAppServico pessoaJuridicasAppServico)
        {
            this.pessoaJuridicasAppServico = pessoaJuridicasAppServico;
        }

        [HttpGet("{codigo}")]
        public ActionResult<PessoaJuridicasResponse> Recuperar(int codigo)
        {
            var response = pessoaJuridicasAppServico.Recuperar(codigo);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<PessoaJuridicasResponse>> Listar(int pagina, int quantidade, [FromQuery] PessoaJuridicasListarRequest pessoaJuridicasListarRequest)
        {    var response = pessoaJuridicasAppServico.Listar(pagina, quantidade, pessoaJuridicasListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<PessoaJuridicasResponse> Inserir([FromBody] PessoaJuridicasCadastroRequest pessoaJuridicas)
        {
            var retorno = pessoaJuridicasAppServico.Inserir(pessoaJuridicas);
            return Ok(retorno);
        }

        [HttpPut("{codigo}")]
        public ActionResult Editar(int codigo, [FromBody] PessoaJuridicasEditarRequest pessoaJuridicas)
        {

            pessoaJuridicasAppServico.Editar(codigo, pessoaJuridicas);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigo}")]
        public ActionResult Excluir(int codigo)
        {
            pessoaJuridicasAppServico.Excluir(codigo);
            return Ok();
        }
    }
}