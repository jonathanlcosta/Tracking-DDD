using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Repositorios;
using Tracking.Dominio.Produtos.Servicos.Interfaces;

namespace Tracking.Dominio.Produtos.Servicos
{
    public class ProdutosServico : IProdutosServico
    {
        private readonly IProdutosRepositorio produtosRepositorio;
        public ProdutosServico(IProdutosRepositorio produtosRepositorio)
        {
            this.produtosRepositorio = produtosRepositorio;
        }
        public Produto Editar(int codigoProduto, string descricao, decimal preco, decimal peso, decimal altura, decimal largura, decimal comprimento)
        {
            var produto = Validar(codigoProduto);
            if(!string.IsNullOrEmpty(descricao) && produto.Descricao != descricao) produto.SetDescricao(descricao);
            if (preco != 0) produto.SetPreco(preco);
            if (peso != 0) produto.SetPeso(peso);
            if (altura != 0) produto.SetAltura(altura);
            if (largura != 0) produto.SetLargura(largura);
            if (comprimento != 0) produto.SetComprimento(comprimento);
            produto = produtosRepositorio.Editar(produto);
            return produto;
        }

        public void Excluir(int id)
        {
            Produto produto = Validar(id);
            produtosRepositorio.Excluir(produto);
        }

        public Produto Inserir(Produto produto)
        { 
            var produtoResponse = produtosRepositorio.Inserir(produto);
            return produtoResponse;
        }

        public Produto Instanciar(string descricao, decimal preco, decimal peso, decimal altura, decimal largura, decimal comprimento)
        {
           var produtoResponse = new Produto(descricao, preco, peso, altura, largura, comprimento);
            return produtoResponse;
        }   

        public Produto Validar(int codigoProduto)
        {
            var produtoResponse = this.produtosRepositorio.Recuperar(codigoProduto);
            if(produtoResponse is null)
            {
                 throw new Exception("Produto não encontrado");
            }
            return produtoResponse;
        }
    }
}