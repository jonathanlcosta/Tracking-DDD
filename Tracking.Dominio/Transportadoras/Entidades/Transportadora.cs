using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Telefones.Entidades;

namespace Tracking.Dominio.Transportadoras.Entidades
{
    public class Transportadora
    {
    public virtual int CodigoTransportadora { get; protected set; }
    public virtual string? RazaoSocial { get; protected set; }
    public virtual string? NomeFantasia { get; protected set; }
    public virtual string? Cnpj { get; protected set; }
    public virtual string? InscricaoEstadual { get; protected set; }
    public virtual IList<Email>? Emails { get; protected set; }
    public virtual IList<Telefone>? Telefones { get; protected set; }
    public virtual string? Endereco { get; protected set; }
    public virtual string? Cidade { get; protected set; }
    public virtual string? Cep { get; protected set; }
    public virtual string? Uf{ get; protected set; }
    public virtual string? Site { get; protected set; }

    public Transportadora(string razaoSocial, string nomeFantasia, 
    string cnpj, string inscricaoEstadual, IList<Email> Emails, IList<Telefone> telefones, IList<Email> emails, string endereco,
    string cidade, string cep, string uf, string site)
    {
        SetRazaoSocial(razaoSocial);
        SetNomeFantasia(nomeFantasia);
        SetCnpj(cnpj);
        SetInscricaoEstadual(inscricaoEstadual);
        SetEmail(emails);
        SetTelefones(telefones);
        SetEndereco(endereco);
        SetCidade(cidade);
        SetCep(cep);
        SetUf(uf);
        SetSite(site);
    }

    public Transportadora()
    {
        
    }

    public virtual void SetRazaoSocial(string razaoSocial)
    {
        if (String.IsNullOrEmpty(razaoSocial))
                throw new ArgumentNullException("O campo razao social deve ser preenchido.");
            if (razaoSocial.Length > 100)
                throw new ArgumentException("O campo razao social deve ter no máximo 100 caracteres.");
            RazaoSocial = razaoSocial;
    }

     public virtual void SetNomeFantasia(string nomeFantasia)
    {
        if (String.IsNullOrEmpty(nomeFantasia))
                throw new ArgumentNullException("O campo nome fantasia deve ser preenchido.");
            if (nomeFantasia.Length > 100)
                throw new ArgumentException("O campo nome fantasia deve ter no máximo 100 caracteres.");
            NomeFantasia = nomeFantasia;
    }

    public virtual void SetCnpj(string cnpj)
    {
         if (String.IsNullOrEmpty(cnpj))
                throw new ArgumentNullException("O CNPJ não pode ser vazio.");
            if (cnpj.Length != 14)
                throw new ArgumentOutOfRangeException("O CNPJ deve conter 14 caracteres.");
            Cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
    }

    public virtual void SetInscricaoEstadual(string inscricaoEstadual)
    {
        if (String.IsNullOrEmpty(inscricaoEstadual))
                throw new ArgumentNullException("A Inscrição Estadual deve ser preenchida.");
            if (inscricaoEstadual.Length != 9)
                throw new ArgumentException("A Inscrição Estadual deve ter 9 digitos.");
            InscricaoEstadual = inscricaoEstadual;
    }

    public virtual void SetEmail(IList<Email> emails)
        {
            if (emails == null)
                throw new ArgumentNullException("o email não pode ser vazio");
            
           Emails = emails;
        }

     public virtual void SetTelefones(IList<Telefone> telefones)
        {
            if (telefones[0] == null && telefones[1] == null)
                throw new ArgumentNullException("Um dos 2 campos precisa ter um telefone");
            Telefones = telefones;
        }

    public virtual void SetEndereco(string? endereco)
        {
            if (String.IsNullOrEmpty(endereco))
                throw new ArgumentException($"O campo {nameof(endereco)} não pode ser vazio.");
            if (endereco.Length > 150)
                throw new ArgumentOutOfRangeException($"O campo {nameof(endereco)} deve ter no máximo 150 caracteres.");
            Endereco = endereco;
        }

    public virtual void SetCep(string? cep)
        {
            if (String.IsNullOrEmpty(cep))
                throw new ArgumentException($"O campo {nameof(cep)} não pode ser vazio.");
            if (cep.Length > 9)
                throw new ArgumentOutOfRangeException($"O campo {nameof(cep)} deve ter 9 caracteres.");
            Cep = cep;
        }

    public virtual void SetCidade(string cidade)
    {
        if (String.IsNullOrEmpty(cidade))
                throw new ArgumentException($"O campo {nameof(cidade)} não pode ser vazio.");
                Cidade = cidade;
    }
    public virtual void SetUf(string uf)
        {
            if (String.IsNullOrEmpty(uf))
                throw new ArgumentNullException("O estado não pode ser vazio");
            if (uf.Length != 2)
                throw new ArgumentOutOfRangeException("O estado deve possuir dois caracteres");

            Uf = uf;
        }

    public virtual void SetSite(string site)
    {
        if (String.IsNullOrEmpty(site))
                throw new ArgumentNullException("O campo site deve ser preenchido.");
            if (site.Length > 100)
                throw new ArgumentException("O campo site deve ter no máximo 100 caracteres.");
            Site = site;
    }
    }
}