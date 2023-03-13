using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracking.Aplicacao.Produtos.Servicos.Interfaces;
using Tracking.DataTransfer.Produtos.Request;
using Tracking.DataTransfer.Produtos.Response;

namespace Tracking.API.Controllers.Produtos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosAppServico produtosAppServico;

        public ProdutosController(IProdutosAppServico produtosAppServico)
        {
            this.produtosAppServico = produtosAppServico;
        }

        [HttpGet("{codigo}")]
        public ActionResult<ProdutoResponse> Recuperar(int codigo)
        {
            var response = produtosAppServico.Recuperar(codigo);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

       [HttpGet]
        public ActionResult<IList<ProdutoResponse>> ListarPaginado(int pagina, int tamanho, int? order, string? search )
        {
            var response = produtosAppServico.ListarPaginado(pagina, tamanho, order, search);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<ProdutoResponse> Inserir([FromBody] ProdutoInserirRequest produto)
        {
            var retorno = produtosAppServico.Inserir(produto);
            return Ok(retorno);
        }

        [HttpPut("{codigo}")]
        public ActionResult Editar(int codigo, [FromBody] ProdutoEditarRequest produto)
        {

            produtosAppServico.Editar(codigo, produto);
            return Ok();
        }

        [HttpDelete]
        [Route("{codigo}")]
        public ActionResult Excluir(int codigo)
        {
            produtosAppServico.Excluir(codigo);
            return Ok();
        }
    }
}