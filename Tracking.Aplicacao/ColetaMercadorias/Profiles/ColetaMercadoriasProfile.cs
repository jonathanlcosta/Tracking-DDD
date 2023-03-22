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
    public class ColetaMercadoriasProfile : Profile
    {
        public ColetaMercadoriasProfile()
        {
             CreateMap<ColetaMercadoria, ColetaMercadoriaResponse>()
                .ForMember(x => x.IdCliente, m => m.MapFrom(y => y.Cliente!.Id))
                .ForMember(x => x.IdTransportadora, m => m.MapFrom(y => y.Transportadora!.CodigoTransportadora));
            CreateMap<ColetaMercadoriaListarRequest, ColetaMercadoria>();
            CreateMap<ColetaMercadoriaEditarRequest, ColetaMercadoria>();
            CreateMap<ColetaMercadoriaInserirRequest, ColetaMercadoria>();
        }
    }
}