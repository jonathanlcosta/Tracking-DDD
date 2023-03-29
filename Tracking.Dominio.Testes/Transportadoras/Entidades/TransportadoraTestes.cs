using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Tracking.Dominio.Transportadoras.Entidades;
using Xunit;

namespace Tracking.Dominio.Testes.Transportadoras.Entidades
{
    public class TransportadoraTestes
    {
        private readonly Transportadora sut; 
        public TransportadoraTestes()
        {
            sut = Builder<Transportadora>.CreateNew().Build();
        }

    }
}