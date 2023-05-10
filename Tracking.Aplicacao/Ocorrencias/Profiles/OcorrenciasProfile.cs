using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.DataTransfer.Ocorrencias.Request;
using Tracking.DataTransfer.Ocorrencias.Response;
using Tracking.Dominio.Ocorrencias.Entidades;

namespace Tracking.Aplicacao.Ocorrencias.Profiles
{
    public class OcorrenciasProfile : Profile
    {
        public OcorrenciasProfile()
        {
             CreateMap<Ocorrencia, OcorrenciaResponse>()
                .ForMember(x => x.IdCliente, m => m.MapFrom(y => y.Cliente!.Id))
                .ForMember(x => x.IdTransportadora, m => m.MapFrom(y => y.Transportadora!.CodigoTransportadora));
        }
    }
}