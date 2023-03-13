using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Produtos.Enumeradores;

namespace Tracking.Dominio.Produtos.Entidades
{
    public class Produto
    {
    public virtual int CodigoProduto { get; protected set; }
    public virtual string? Descricao { get; protected set; }
    public virtual SituacaoProdutoEnum Situacao { get; protected set; }
    public virtual decimal Preco { get; protected set; }
    public virtual double Peso { get; protected set; }
    public virtual double Altura { get; protected set; }
    public virtual double Largura { get; protected set; }
    public virtual double Comprimento { get; protected set; }
    public Produto(string descricao, decimal preco, double peso, double altura, double largura, double comprimento)
    {
        SetDescricao(descricao);
        SetPreco(preco);
        SetPeso(peso);
        SetAltura(altura);
        SetLargura(largura);
        SetComprimento(comprimento);
        
    }

    public Produto()
    { }

    public virtual void SetDescricao(string? descricao)
        {
            if (String.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException("A descrição não pode ser nula ou vazia");
            }
           Descricao = descricao;
        }

    public virtual void SetSituacaoProduto(SituacaoProdutoEnum? situacaoProduto)
        {
            if (!situacaoProduto.HasValue)
            {
                throw new ArgumentNullException("A situação do produto não pode ser nulo.");
            }
            Situacao = situacaoProduto.Value;
        }

        public virtual void SetPreco(decimal preco)
        {
            if(preco <= 0)
            {
                throw new Exception("O preço não pode ser menor ou igual a zero");
            }
            Preco = preco;
        }

        public virtual void SetPeso(double peso)
        {
            if(peso <= 0)
            {
                throw new Exception("O peso não pode ser menor ou igual a zero");
            }
            Peso = peso;
        }

        public virtual void SetAltura(double altura)
        {
            if(altura <= 0)
            {
                throw new Exception("A altura não pode ser menor ou igual a zero");
            }
           Altura = altura;
        }

        public virtual void SetLargura(double largura)
        {
            if(largura <= 0)
            {
                throw new Exception("A largura não pode ser menor ou igual a zero");
            }
           Largura = largura;
        }

         public virtual void SetComprimento(double comprimento)
        {
            if(comprimento <= 0)
            {
                throw new Exception("O comprimento não pode ser menor ou igual a zero");
            }
           Comprimento = comprimento;
        }


    }
}