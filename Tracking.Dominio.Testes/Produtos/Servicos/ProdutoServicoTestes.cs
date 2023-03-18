using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Repositorios;
using Tracking.Dominio.Produtos.Servicos;
using Tracking.Dominio.Produtos.Servicos.Interfaces;
using Xunit;

namespace Tracking.Dominio.Testes.Produtos.Servicos
{
    public class ProdutosServicoTestes
    {
        private readonly IProdutosServico sut;
        private readonly Produto produtoValido;
        private readonly IProdutosRepositorio produtosRepositorio;

        public ProdutosServicoTestes()
        {
            produtoValido = Builder<Produto>.CreateNew().Build();
            produtosRepositorio = Substitute.For<IProdutosRepositorio>();
            sut = new ProdutosServico(produtosRepositorio);
        }

        public class ValidarMetodo : ProdutosServicoTestes
        {
            [Fact]
            public void Dado_ProdutoNaoEncontrado_Espero_Excecao()
            {
                produtosRepositorio.Recuperar(Arg.Any<int>()).Returns(x => null);
                sut.Invoking(x => x.ValidarProduto(2)).Should().Throw<Exception>();

            }

            [Fact]
            public void Dado_ProdutoEncontrado_Espero_ProdutoValido()
            {
                produtosRepositorio.Recuperar(Arg.Any<int>()).Returns(produtoValido);
                sut.ValidarProduto(2).Should().BeSameAs(produtoValido);
            }
        }
        public class InstanciarMetodo : ProdutosServicoTestes
        {
            [Fact]
            public void Dado_ParametrosParaCriarProduto_Espero_ProdutoInstanciado()
            {
                string descricao = "parabrisa";
                decimal preco = 20;
                decimal peso = 2;
                decimal altura = 2;
                decimal largura = 1;
                decimal comprimento = 1;
                var produto = sut.InstanciarProduto(descricao, preco, peso, altura, largura, comprimento );

                produto.Should().NotBeNull();
                produto.Descricao.Should().Be(descricao);
                produto.Preco.Should().Be(preco);
                produto.Peso.Should().Be(peso);
                produto.Altura.Should().Be(altura);
                produto.Largura.Should().Be(largura);
                produto.Comprimento.Should().Be(comprimento);
            }
        }
        public class InserirMetodo : ProdutosServicoTestes
        {
            [Fact]
            public void Dado_ProdutoValido_Espero_ProdutoInserido()
            {
                produtosRepositorio.Inserir(Arg.Any<Produto>()).Returns(produtoValido);

                var produto = sut.InserirProduto(produtoValido);

                produtosRepositorio.Received(1).Inserir(produtoValido);
                produto.Should().BeOfType<Produto>();
                produto.Should().Be(produtoValido);
            }
        }

        public class AtualizarMetodo : ProdutosServicoTestes
        {
            [Fact]
            public void Quando_MetodoForChamado_Espero_ProdutoAtualizado()
            {
                produtosRepositorio.Recuperar(1).Returns(produtoValido);

                sut.EditarProduto(1, "parabrisa", 123, 1, 1, 2, 3);

                produtoValido.CodigoProduto.Should().Be(1);
                produtoValido.Descricao.Should().Be("parabrisa");
                produtoValido.Preco.Should().Be(123);
                produtoValido.Peso.Should().Be(1);
                produtoValido.Altura.Should().Be(1);
                produtoValido.Largura.Should().Be(2);
                produtoValido.Comprimento.Should().Be(3);
                produtosRepositorio.Received().Editar(produtoValido);
            }
        }
        public class ExcluirMetodo : ProdutosServicoTestes
        {
            [Fact]
            public void Quando_MetodoForChamado_Espero_ProdutoDeletado()
            {
                produtosRepositorio.Recuperar(1).Returns(produtoValido);

                sut.ExcluirProduto(1);

                produtosRepositorio.Received().Excluir(produtoValido);
            }
        }
    }
}