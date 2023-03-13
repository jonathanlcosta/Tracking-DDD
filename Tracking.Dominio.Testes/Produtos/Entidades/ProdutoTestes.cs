using FizzWare.NBuilder;
using FluentAssertions;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Enumeradores;
using Xunit;

namespace Tracking.Dominio.Testes.Produtos.Entidades
{
    public class ProdutoTestes
    {
        private readonly Produto sut; 
        public ProdutoTestes()
        {
            sut = Builder<Produto>.CreateNew().Build();
        }

        public class SetDescricaoMetodo: ProdutoTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("                ")]
            public void Dado_DescricaoNuloOuEspacoEmBranco_Espero_Excecao(string descricao)
            {
                sut.Invoking(x => x.SetDescricao(descricao)).Should().Throw<ArgumentException>();
            }
            [Fact]
            public void Dado_DescricaoValido_Espero_PropriedadesPreenchidas()
            {
                string descricao = "Camisa";
                sut.SetDescricao(descricao);
                sut.Descricao.Should().NotBeNullOrWhiteSpace(descricao);
                sut.Descricao.Should().Be(descricao);
            }
    }
        public class SetPrecoMetodo: ProdutoTestes
        {
            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Dado_PrecoMenorOuIgualAZero_Espero_Excecao(decimal preco)
            {
                sut.Invoking(x => x.SetPreco(preco)).Should().Throw<Exception>();
            }
            [Fact]
            public void Dado_PrecoValido_Espero_PropriedadesPreenchidas()
            {
                decimal preco = 12m;
                sut.SetPreco(preco);
                sut.Preco.Should().Be(preco);
                sut.Preco.Should().BeGreaterThan(0);
            }
        }

        public class SetAlturaMetodo: ProdutoTestes
        {
            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Dado_AlturaMenorOuIgualAZero_Espero_Excecao(double altura)
            {
                sut.Invoking(x => x.SetAltura(altura)).Should().Throw<Exception>();
            }
            [Fact]
            public void Dado_AlturaValido_Espero_PropriedadesPreenchidas()
            {
                double altura = 2;
                sut.SetAltura(altura);
                sut.Altura.Should().Be(altura);
                sut.Altura.Should().BeGreaterThan(0);
            }
        }

        public class SetPesoMetodo: ProdutoTestes
        {
            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Dado_PesoMenorOuIgualAZero_Espero_Excecao(double peso)
            {
                sut.Invoking(x => x.SetPeso(peso)).Should().Throw<Exception>();
            }
            [Fact]
            public void Dado_PesoValido_Espero_PropriedadesPreenchidas()
            {
                double peso = 2.5;
                sut.SetPeso(peso);
                sut.Peso.Should().Be(peso);
                sut.Peso.Should().BeGreaterThan(0);
            }
        }
        

        public class SetLarguraMetodo: ProdutoTestes
        {
            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Dado_LarguraMenorOuIgualAZero_Espero_Excecao(double largura)
            {
                sut.Invoking(x => x.SetLargura(largura)).Should().Throw<Exception>();
            }
            [Fact]
            public void Dado_LarguraValido_Espero_PropriedadesPreenchidas()
            {
                double largura = 2;
                sut.SetLargura(largura);
                sut.Largura.Should().Be(largura);
                sut.Largura.Should().BeGreaterThan(0);
            }
        }

        public class SetComprimentoMetodo: ProdutoTestes
        {
            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Dado_ComprimentoMenorOuIgualAZero_Espero_Excecao(double comprimento)
            {
                sut.Invoking(x => x.SetComprimento(comprimento)).Should().Throw<Exception>();
            }
            [Fact]
            public void Dado_ComprimentoValido_Espero_PropriedadesPreenchidas()
            {
                double comprimento = 2;
                sut.SetComprimento(comprimento);
                sut.Comprimento.Should().Be(comprimento);
                sut.Comprimento.Should().BeGreaterThan(0);
            }
        }

         public class SetSituacaoProdutoTest : ProdutoTestes
{
    [Fact]
    public void Dado_SituacaoNula_Espera_ArgumentNullException()
    {
        SituacaoProdutoEnum? situacaoProduto = null;
        sut.Invoking(x => x.SetSituacaoProduto(situacaoProduto))
           .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Dado_SituacaoValida_Espera_Situacao()
    {
        SituacaoProdutoEnum situacaoProduto = SituacaoProdutoEnum.Ativo;
        sut.SetSituacaoProduto(situacaoProduto);
        sut.Situacao.Should().Be(situacaoProduto);
    }
}
    public class Construtor
            {
                [Fact]
                public void Quando_Parametros_ForemValidos_Espero_ObjetoIntegro()
                {
                    var produto = new Produto("parabrisa", 1, 2, 1, 2, 3);
                    produto.Descricao.Should().Be("parabrisa");
                    produto.Preco.Should().Be(1);
                    produto.Peso.Should().Be(2);
                    produto.Altura.Should().Be(1);
                    produto.Largura.Should().Be(2);
                    produto.Comprimento.Should().Be(3);
                    
                }

            }

}}