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
    public class OcorrenciaColetaMercadoriasProfile : Profile
    {
        public OcorrenciaColetaMercadoriasProfile()
        {
            CreateMap<OcorrenciaColetaMercadoria, OcorrenciaColetaMercadoriaResponse>()
            .ForMember(x => x.IdColetaMercadoria, m => m.MapFrom(y => y.ColetaMercadoria!.Id));
        }
    }
}