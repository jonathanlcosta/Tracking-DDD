using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Request;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Response;
using Tracking.Dominio.Clientes.Entidades;

namespace Tracking.Aplicacao.Clientes.Profiles
{
    public class PessoaJuridicasProfile : Profile
    {
        public PessoaJuridicasProfile()
        {
            ReplaceMemberName("CpfCnpj", "Cnpj");
            CreateMap<Cliente, PessoaJuridicasResponse>();
            CreateMap<PessoaJuridicasListarRequest, Cliente>();
            CreateMap<PessoaJuridicasCadastroRequest, Cliente>();
            CreateMap<PessoaJuridicasEditarRequest, Cliente>();
            CreateMap<PessoaJuridicasResponse, Cliente>();
        }
    }
}