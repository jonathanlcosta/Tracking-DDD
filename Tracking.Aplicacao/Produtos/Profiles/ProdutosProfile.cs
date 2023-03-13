using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.DataTransfer.Produtos.Request;
using Tracking.DataTransfer.Produtos.Response;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Aplicacao.Produtos.Profiles
{
    public class ProdutosProfile : Profile
    {
        public ProdutosProfile()
        {
        CreateMap<Produto, ProdutoResponse>();
        CreateMap<Produto, ProdutoListarRequest>();
        CreateMap<ProdutoInserirRequest, Produto>();
        CreateMap<ProdutoEditarRequest, Produto>();
        }
    }
}