using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Repositorios;
using Tracking.Dominio.Transportadoras.Servicos;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;
using Xunit;

namespace Tracking.Dominio.Testes.Transportadoras.Servicos
{
    public class TransportadorasServicoTestes
    {
        private readonly ITransportadorasServico sut;
        private readonly Transportadora transportadoraValido;
        private readonly ITransportadorasRepositorio transportadoraRepositorio;

        public TransportadorasServicoTestes()
        {
            transportadoraValido = Builder<Transportadora>.CreateNew().Build();
            transportadoraRepositorio = Substitute.For<ITransportadorasRepositorio>();
            sut = new TransportadorasServico(transportadoraRepositorio);
        }

        public class ValidarMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Dado_TransportadoraNaoEncontrado_Espero_Excecao()
            {
                transportadoraRepositorio.Recuperar(Arg.Any<int>()).Returns(x => null);
                sut.Invoking(x => x.Validar(2)).Should().Throw<Exception>();

            }

            [Fact]
            public void Dado_TransportadoraEncontrado_Espero_ProdutoValido()
            {
                transportadoraRepositorio.Recuperar(Arg.Any<int>()).Returns(transportadoraValido);
                sut.Validar(2).Should().BeSameAs(transportadoraValido);
            }
        }
        public class InserirMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Dado_ProdutoValido_Espero_ProdutoInserido()
            {
                transportadoraRepositorio.Inserir(Arg.Any<Transportadora>()).Returns(transportadoraValido);

                var produto = sut.Inserir(transportadoraValido);

                transportadoraRepositorio.Received(1).Inserir(transportadoraValido);
                produto.Should().BeOfType<Transportadora>();
                produto.Should().Be(transportadoraValido);
            }
        }
        public class ExcluirMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Quando_MetodoForChamado_Espero_ProdutoDeletado()
            {
                transportadoraRepositorio.Recuperar(1).Returns(transportadoraValido);

                sut.Excluir(1);

                transportadoraRepositorio.Received().Excluir(transportadoraValido);
            }
        }
    }
}