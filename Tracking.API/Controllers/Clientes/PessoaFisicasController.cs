using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracking.Aplicacao.Clientes.Servicos.Interfaces;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Request;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.API.Controllers.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaFisicasController : ControllerBase
    {
        private readonly IPessoaFisicasAppServico pessoaFisicasAppServico;

        public PessoaFisicasController(IPessoaFisicasAppServico pessoaFisicasAppServico)
        {
            this.pessoaFisicasAppServico = pessoaFisicasAppServico;
        }

        [HttpGet("{codigo}")]
        public ActionResult<PessoaFisicasResponse> Recuperar(int codigo)
        {
            var response = pessoaFisicasAppServico.Recuperar(codigo);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<PessoaFisicasResponse>> Listar(int pagina, int quantidade, [FromQuery] PessoaFisicasListarRequest pessoaFisicasListarRequest)
        {    var response = pessoaFisicasAppServico.Listar(pagina, quantidade, pessoaFisicasListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<PessoaFisicasResponse> Inserir([FromBody] PessoaFisicasCadastroRequest pessoaFisicas)
        {
            var retorno = pessoaFisicasAppServico.Inserir(pessoaFisicas);
            return Ok(retorno);
        }

        [HttpPut("{codigo}")]
        public ActionResult Editar(int codigo, [FromBody] PessoaFisicasEditarRequest pessoaFisicas)
        {

            pessoaFisicasAppServico.Editar(codigo, pessoaFisicas);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigo}")]
        public ActionResult Excluir(int codigo)
        {
            pessoaFisicasAppServico.Excluir(codigo);
            return Ok();
        }
    }
}