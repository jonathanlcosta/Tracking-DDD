using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.DataTransfer.ColetaMercadorias.Request;
using Tracking.DataTransfer.ColetaMercadorias.Response;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Aplicacao.ColetaMercadorias.Profiles
{
    public class ItensColetaMercadoriasProfile : Profile
    {
        public ItensColetaMercadoriasProfile()
        {
            CreateMap<ItemColetaMercadoria, ItemColetaMercadoriaResponse>()
             .ForMember(x => x.IdProduto, m => m.MapFrom(y => y.Produto!.CodigoProduto));
            CreateMap<ItemColetaMercadoriaInserirRequest, ItemColetaMercadoria>();
        }
    }
}