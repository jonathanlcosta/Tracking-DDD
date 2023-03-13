using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Paginacao.Profiles
{
    public class PaginacaoConsultasProfile : Profile
    {
        public PaginacaoConsultasProfile()
        {
            CreateMap(typeof(PaginacaoConsulta<>), typeof(PaginacaoConsulta<>));
        }
    }
}