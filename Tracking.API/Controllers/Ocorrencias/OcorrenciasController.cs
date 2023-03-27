using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracking.Aplicacao.Ocorrencias.Servicos.Interfaces;
using Tracking.DataTransfer.Ocorrencias.Request;
using Tracking.DataTransfer.Ocorrencias.Response;
using Tracking.Dominio.Paginacao;

namespace Tracking.API.Controllers.Ocorrencias
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcorrenciasController : ControllerBase
    {
        private readonly IOcorrenciasAppServico ocorrenciasAppServico;
        public OcorrenciasController(IOcorrenciasAppServico ocorrenciasAppServico)
        {
            this.ocorrenciasAppServico = ocorrenciasAppServico;
        }

        [HttpGet("{codigo}")]
        public ActionResult<OcorrenciaResponse> Recuperar(int codigo)
        {
            var response = ocorrenciasAppServico.Recuperar(codigo);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<PaginacaoConsulta<OcorrenciaResponse>> Listar(int pagina, int quantidade, [FromQuery] OcorrenciaListarRequest ocorrenciaListarRequest)
        {    var response = ocorrenciasAppServico.Listar(pagina, quantidade, ocorrenciaListarRequest);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<OcorrenciaResponse> Inserir([FromBody] OcorrenciaInserirRequest ocorrencia)
        {
            var retorno = ocorrenciasAppServico.Inserir(ocorrencia);
            return Ok(retorno);
        }

        [HttpPut("{codigo}")]
        public ActionResult Editar(int codigo, [FromBody] OcorrenciaEditarRequest ocorrencia)
        {

            ocorrenciasAppServico.Editar(codigo, ocorrencia);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigo}")]
        public ActionResult Excluir(int codigo)
        {
            ocorrenciasAppServico.Excluir(codigo);
            return Ok();
        }
    }
}