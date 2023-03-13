using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracking.Aplicacao.Transportadoras.Servicos.Interfaces;
using Tracking.DataTransfer.Transportadoras.Request;
using Tracking.DataTransfer.Transportadoras.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.API.Controllers.Transportadoras
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportadorasController : ControllerBase
    {
        private readonly ITransportadorasAppServico transportadorasAppServico;

        public TransportadorasController(ITransportadorasAppServico transportadorasAppServico)
        {
            this.transportadorasAppServico = transportadorasAppServico;
        }

        [HttpGet("{codigo}")]
        public ActionResult<TransportadoraResponse> Recuperar(int codigo)
        {
            var response = transportadorasAppServico.Recuperar(codigo);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<TransportadoraResponse>> Listar(int pagina, int quantidade, [FromQuery] TransportadoraListarRequest transportadoraListarRequest)
        {    var response = transportadorasAppServico.Listar(pagina, quantidade, transportadoraListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<TransportadoraResponse> Inserir([FromBody] TransportadoraInserirRequest transportadora)
        {
            var retorno = transportadorasAppServico.Inserir(transportadora);
            return Ok(retorno);
        }

        [HttpPut("{codigo}")]
        public ActionResult Editar(int codigo, [FromBody] TransportadoraEditarRequest transportadora)
        {

            transportadorasAppServico.Editar(codigo, transportadora);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigo}")]
        public ActionResult Excluir(int codigo)
        {
            transportadorasAppServico.Excluir(codigo);
            return Ok();
        }
    }
}