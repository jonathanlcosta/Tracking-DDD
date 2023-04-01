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

        public class InstanciarMetodo : TransportadorasServicoTestes
        {
            [Fact]
            public void Dado_ParametrosParaCriarTransportadora_Espero_TransportadoraInstanciado()
            {
                string razaoSocial = "Empresa aleatoria";
                string nomeFantasia = "Empresa";
                string cnpj = "12345678911234" ;
                string inscricaoEstadual = "123456789";
                IList<Email> emails = new List<Email>();
                Email email = new Email("email@email.com", transportadoraValido);
                emails.Add(email);
                IList<Telefone> telefones = new List<Telefone>();
                Telefone telefone = new Telefone("2733432123", transportadoraValido);
                telefones.Add(telefone);
                string endereco = "Rua aleatoria";
                string cidade = "Bahia";
                string cep = "12345123";
                string uf = "BA";
                string site = "www.aleatorio.com";
                var transportadora = sut.Instanciar(razaoSocial, nomeFantasia,  cnpj, inscricaoEstadual, emails, 
                telefones, endereco, cidade, cep, uf, site);

                transportadora.Should().NotBeNull();
                transportadora.NomeFantasia.Should().Be(nomeFantasia);
                transportadora.RazaoSocial.Should().Be(razaoSocial);
                transportadora.Cnpj.Should().Be(cnpj);
                transportadora.InscricaoEstadual.Should().Be(inscricaoEstadual);
                transportadora.Endereco.Should().Be(endereco);
                transportadora.Cidade.Should().Be(cidade);
                transportadora.Cep.Should().Be(cep);
                transportadora.Uf.Should().Be(uf);
                transportadora.Site.Should().Be(site);
                transportadora.Emails.Should().Contain(emails);
                transportadora.Telefones.Should().Contain(telefones);
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