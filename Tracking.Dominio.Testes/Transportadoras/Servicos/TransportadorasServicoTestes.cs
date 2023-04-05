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
            public void Dado_TransportadoraEncontrado_Espero_TransportadoraValido()
            {
                transportadoraRepositorio.Recuperar(Arg.Any<int>()).Returns(transportadoraValido);
                sut.Validar(2).Should().BeSameAs(transportadoraValido);
            }
        }

        public class InstanciarMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Dado_ParametrosParaCriarTransportadora_Espero_TransportadoraInstanciado()
            {
            var email = new Email("email@email.com", transportadoraValido);
            var telefone = new Telefone("2733432123", transportadoraValido);
            var emails = new List<Email> { email };
            var telefones = new List<Telefone> { telefone };

            // Act
            var transportadora = sut.Instanciar("Empresa aleatoria", "Empresa", "12345678911234", "123456789", emails,
                telefones, "Rua aleatoria", "Bahia", "12345123", "BA", "www.aleatorio.com");

            // Assert
            Assert.NotNull(transportadora);
            Assert.Equal("Empresa aleatoria", transportadora.RazaoSocial);
            Assert.Equal("Empresa", transportadora.NomeFantasia);
            Assert.Equal("12345678911234", transportadora.Cnpj);
            Assert.Equal("123456789", transportadora.InscricaoEstadual);
            Assert.Equal("Rua aleatoria", transportadora.Endereco);
            Assert.Equal("Bahia", transportadora.Cidade);
            Assert.Equal("12345123", transportadora.Cep);
            Assert.Equal("BA", transportadora.Uf);
            Assert.Equal("www.aleatorio.com", transportadora.Site);
            Assert.Contains(email, transportadora.Emails);
            Assert.Contains(telefone, transportadora.Telefones);
            }
        }
        public class InserirMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Dado_TransportadoraValido_Espero_TransportadoraInserida()
            {
                transportadoraRepositorio.Inserir(Arg.Any<Transportadora>()).Returns(transportadoraValido);

                var transportadora = sut.Inserir(transportadoraValido);

                transportadoraRepositorio.Received(1).Inserir(transportadoraValido);
                transportadora.Should().BeOfType<Transportadora>();
                transportadora.Should().Be(transportadoraValido);
            }
        }
        public class ExcluirMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Quando_MetodoForChamado_Espero_TransportadoraDeletada()
            {
                transportadoraRepositorio.Recuperar(1).Returns(transportadoraValido);

                sut.Excluir(1);

                transportadoraRepositorio.Received().Excluir(transportadoraValido);
            }
        }

         public class AtualizarMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Quando_MetodoForChamado_Espero_TransportadoraAtualizado()
            {
                transportadoraRepositorio.Recuperar(1).Returns(transportadoraValido);
                IList<Email> emails = new List<Email>();
                IList<Telefone> telefones = new List<Telefone>();
                sut.Editar(1, "Empresa", "Aleatorio", "12345678911234", "123456789", emails, telefones,
                 "Rua aleatoria", "Salvador", "12342134", "BA", "www.aleatorio.com");

                transportadoraValido.CodigoTransportadora.Should().Be(1);
                transportadoraValido.RazaoSocial.Should().Be("Empresa");
                transportadoraValido.NomeFantasia.Should().Be("Aleatorio");
                transportadoraValido.Cnpj.Should().Be("12345678911234");
                transportadoraValido.InscricaoEstadual.Should().Be("123456789");
                transportadoraValido.Endereco.Should().Be("Rua aleatoria");
                transportadoraValido.Cidade.Should().Be("Salvador");
                transportadoraValido.Cep.Should().Be("12342134");
                transportadoraValido.Uf.Should().Be("BA");
                transportadoraValido.Site.Should().Be("www.aleatorio.com");
                transportadoraRepositorio.Received().Editar(transportadoraValido);
            }
        }
    }
}