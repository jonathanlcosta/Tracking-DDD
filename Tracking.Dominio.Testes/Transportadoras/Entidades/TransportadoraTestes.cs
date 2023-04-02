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

        public class Construtor : TransportadoraTestes
            {
                [Fact]
                public void Quando_Parametros_ForemValidos_Espero_ObjetoIntegro()
                {
                var email = new Email("aleatorio@email.com", sut);
                var telefone = new Telefone("27887432234", sut);
                var emails = new List<Email> { email };
                var telefones = new List<Telefone> { telefone };
                var transportadora = new Transportadora(
                    "Empresa Teste", 
                    "Empresa", 
                    "12345678911234",
                    "123456789", 
                    emails, 
                    telefones, 
                    "Rua aleatoria", 
                    "Salvador", 
                    "123345672", 
                    "BA", 
                    "www.google.com.br");

                    Assert.Equal("Empresa Teste", transportadora.RazaoSocial);
                    Assert.Equal("Empresa", transportadora.NomeFantasia);
                    Assert.Equal("12345678911234", transportadora.Cnpj);
                    Assert.Equal("123456789", transportadora.InscricaoEstadual);
                    Assert.Equal(emails, transportadora.Emails);
                    Assert.Equal(telefones, transportadora.Telefones);
                    Assert.Equal("Rua aleatoria", transportadora.Endereco);
                    Assert.Equal("Salvador", transportadora.Cidade);
                    Assert.Equal("123345672", transportadora.Cep);
                    Assert.Equal("BA", transportadora.Uf);
                    Assert.Equal("www.google.com.br", transportadora.Site);
                    
                }

            }


        public class SetRazaoSocialMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_RazaoSocialNuloOuEspacoEmBranco_Espero_Excecao(string razaoSocial)
            {
                sut.Invoking(x => x.SetRazaoSocial(razaoSocial)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_NomeFantasiaMaisDeCemCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetCnpj(new string('*', 101))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_razaoSocialValido_Espero_PropriedadesPreenchidas()
            {
                string razaoSocial = "Romano";
                sut.SetRazaoSocial(razaoSocial);
                sut.RazaoSocial.Should().NotBeNullOrWhiteSpace(razaoSocial);
                sut.RazaoSocial.Should().Be(razaoSocial);
            }
    }

     public class SetNomeFantasiaMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("              ")]
            public void Dado_NomeFantasiaNuloOuEspacoEmBranco_Espero_Excecao(string nomeFantasia)
            {
                sut.Invoking(x => x.SetNomeFantasia(nomeFantasia)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_NomeFantasiaMaisDeCemCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetCnpj(new string('*', 101))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_NomeFantasiaValido_Espero_PropriedadesPreenchidas()
            {
                string nomeFantasia = "Empresa";
                sut.SetNomeFantasia(nomeFantasia);
                sut.NomeFantasia.Should().NotBeNullOrWhiteSpace(nomeFantasia);
                sut.NomeFantasia.Should().Be(nomeFantasia);
            }
    }

    public class SetCnpjMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_CnpjNuloOuEspacoEmBranco_Espero_Excecao(string cnpj)
            {
                sut.Invoking(x => x.SetCnpj(cnpj)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_CnpjComMaisDeQuatorzeCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetCnpj(new string('*', 15))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_CnpjComMenosDeQuatorzeCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetCnpj(new string('*', 13))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_CnpjValido_Espero_PropriedadesPreenchidas()
            {
                string cnpj = "123-45678911234";
                sut.SetCnpj(cnpj);
                sut.Cnpj.Should().NotBeNullOrWhiteSpace(cnpj);
                sut.Cnpj.Should().Be(cnpj);
            }
    }

    public class SetInscricaoEstadualMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_InscricaoEstadualNuloOuEspacoEmBranco_Espero_Excecao(string ie)
            {
                sut.Invoking(x => x.SetInscricaoEstadual(ie)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_InscricaoEstadualComMaisDeNoveCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetInscricaoEstadual(new string('*', 10))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_InscricaoEstadualComMenosDeNoveCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetInscricaoEstadual(new string('*', 8))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_InscriçãoEstadualValido_Espero_PropriedadesPreenchidas()
            {
                string ie = "123456789";
                sut.SetInscricaoEstadual(ie);
                sut.InscricaoEstadual.Should().NotBeNullOrWhiteSpace(ie);
                sut.InscricaoEstadual.Should().Be(ie);
            }
    }

     public class SetEnderecoMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_EnderecoNuloOuEspacoEmBranco_Espero_Excecao(string endereco)
            {
                sut.Invoking(x => x.SetEndereco(endereco)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_EnderecoComMaisDeCentoECinquentaCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetEndereco(new string('*', 151))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_EnderecoValido_Espero_PropriedadesPreenchidas()
            {
                string endereco = "Rua Henrique";
                sut.SetEndereco(endereco);
                sut.Endereco.Should().NotBeNullOrWhiteSpace(endereco);
                sut.Endereco.Should().Be(endereco);
            }
    }

    public class SetEmailsMetodo : TransportadoraTestes
    {
        [Fact]
        public void Dado_ListaEmailNulo_Espero_Excecao()
        {
        IList<Email> emails = null;
         sut.Invoking(x => x.SetEmail(emails)).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Dado_ListaEmailValido_Espero_PropriedadesPreenchidas()
        {
           IList<Email> emails = new List<Email>();
           sut.SetEmail(emails);

        }
    }

    public class SetTelefones: TransportadoraTestes
    {
        [Fact]
        public void Dado_ListaTelefoneNulo_Espero_Excecao()
        {
            IList<Telefone> telefones = null;
            sut.Invoking(x => x.SetTelefones(telefones)).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Dado_ListaTelefoneValido_Espero_PropriedadesPreenchidas()
        {
            IList<Telefone> telefones = new List<Telefone>();
            sut.SetTelefones(telefones);

        }
    }
    public class SetCEPMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_CepNuloOuEspacoEmBranco_Espero_Excecao(string cep)
            {
                sut.Invoking(x => x.SetCep(cep)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_CepMaisDeNoveCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetCep(new string('*', 10))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_CepValido_Espero_PropriedadesPreenchidas()
            {
                string cep = "29072130";
                sut.SetCep(cep);
                sut.Cep.Should().NotBeNullOrWhiteSpace(cep);
                sut.Cep.Should().Be(cep);
            }
    }

     public class SetCidadeMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_CidadeNuloOuEspacoEmBranco_Espero_Excecao(string cidade)
            {
                sut.Invoking(x => x.SetCidade(cidade)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_CidadeValido_Espero_PropriedadesPreenchidas()
            {
                string cidade = "Vitória";
                sut.SetCidade(cidade);
                sut.Cidade.Should().NotBeNullOrWhiteSpace(cidade);
                sut.Cidade.Should().Be(cidade);
            }
    }

    public class SetUfMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_UfNuloOuEspacoEmBranco_Espero_Excecao(string uf)
            {
                sut.Invoking(x => x.SetUf(uf)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_UfMaisDeDoisCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetUf(new string('*', 3))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_UfMenosDeDoisCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetUf(new string('*', 1))).Should().Throw<Exception>();
            }


            [Fact]
            public void Dado_UfValido_Espero_PropriedadesPreenchidas()
            {
                string uf = "ES";
                sut.SetUf(uf);
                sut.Uf.Should().NotBeNullOrWhiteSpace(uf);
                sut.Uf.Should().Be(uf);
            }
    }

    public class SetSiteMetodo: TransportadoraTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_SiteNuloOuEspacoEmBrancoEAcimaDe100Caracteres_Espero_Excecao(string site)
            {
                sut.Invoking(x => x.SetSite(site)).Should().Throw<ArgumentException>();
            }

            [Fact]
            public void Dado_SiteMaisDeCentoECinquentaCaracteres_Espero_Exception()
            {
                sut.Invoking(x => x.SetSite(new string('*', 151))).Should().Throw<Exception>();
            }

            [Fact]
            public void Dado_SiteValido_Espero_PropriedadesPreenchidas()
            {
                string site = "www.autoglass.com.br";
                sut.SetSite(site);
                sut.Site.Should().NotBeNullOrWhiteSpace(site);
                sut.Site.Should().Be(site);
            }
    }


    }
}