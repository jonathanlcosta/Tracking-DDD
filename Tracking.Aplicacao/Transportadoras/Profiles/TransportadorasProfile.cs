using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.DataTransfer.Transportadoras.Request;
using Tracking.DataTransfer.Transportadoras.Response;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Aplicacao.Transportadoras.Profiles
{
    public class TransportadorasProfile : Profile
    {
        public TransportadorasProfile()
        {
        CreateMap<Transportadora, TransportadoraResponse>()
        .ForMember(dest => dest.NumeroTelefone1, m => m.MapFrom(src => src.Telefones.First().NumeroTelefone))
                .ForMember(dest => dest.NumeroTelefone2, m => m.MapFrom(src => src.Telefones.LastOrDefault()!.NumeroTelefone))
                .ForMember(dest => dest.Email1, m => m.MapFrom(src => src.Emails.First().EnderecoEmail))
                .ForMember(dest => dest.Email2, m => m.MapFrom(src => src.Emails.LastOrDefault()!.EnderecoEmail));
        CreateMap<Transportadora, TransportadoraListarRequest>();
        CreateMap<TransportadoraInserirRequest, Transportadora>();
        CreateMap<TransportadoraEditarRequest, Transportadora>();
        }
    }
}