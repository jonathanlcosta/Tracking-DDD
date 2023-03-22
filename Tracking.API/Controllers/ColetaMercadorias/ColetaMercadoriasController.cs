using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracking.Aplicacao.ColetaMercadorias.Servicos.Interfaces;
using Tracking.DataTransfer.ColetaMercadorias.Request;
using Tracking.DataTransfer.ColetaMercadorias.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.API.Controllers.ColetaMercadorias
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColetaMercadoriasController : ControllerBase
    {
        private readonly IColetaMercadoriasAppServico coletasAppServico;

        public ColetaMercadoriasController(IColetaMercadoriasAppServico coletasAppServico)
        {
            this.coletasAppServico = coletasAppServico;
        }

        [HttpGet("{codigo}")]
        public ActionResult<ColetaMercadoriaResponse> Recuperar(int codigo)
        {
            var response = coletasAppServico.Recuperar(codigo);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<ColetaMercadoriaResponse>> Listar(int pagina, int quantidade, [FromQuery] ColetaMercadoriaListarRequest coletaListarRequest)
        {    var response = coletasAppServico.Listar(pagina, quantidade, coletaListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<ColetaMercadoriaResponse> Inserir([FromBody] ColetaMercadoriaInserirRequest coleta)
        {
            var retorno = coletasAppServico.Inserir(coleta);
            return Ok(retorno);
        }

        [HttpPut("{codigo}")]
        public ActionResult Editar(int codigo, [FromBody] ColetaMercadoriaEditarRequest coleta)
        {

            coletasAppServico.Editar(codigo, coleta);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigo}")]
        public ActionResult Excluir(int codigo)
        {
            coletasAppServico.Excluir(codigo);
            return Ok();
        }
    }
}