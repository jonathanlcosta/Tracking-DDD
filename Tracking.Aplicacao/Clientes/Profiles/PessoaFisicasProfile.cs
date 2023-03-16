using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Request;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Response;
using Tracking.Dominio.Clientes.Entidades;

namespace Tracking.Aplicacao.Clientes.Profiles
{
    public class PessoaFisicasProfile : Profile
    {
        public PessoaFisicasProfile()
        {
             ReplaceMemberName("CPFCNPJ", "CPF");
            CreateMap<Cliente, PessoaFisicasResponse>();
            CreateMap<PessoaFisicasListarRequest, Cliente>();
            CreateMap<PessoaFisicasCadastroRequest, Cliente>();
            CreateMap<PessoaFisicasEditarRequest, Cliente>();
            CreateMap<PessoaFisicasResponse, Cliente>();
        }
    }
}