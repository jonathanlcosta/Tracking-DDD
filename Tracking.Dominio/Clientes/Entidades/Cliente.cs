using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Enumeradores;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Dominio.Clientes.Entidades
{
    public class Cliente
    {
    public virtual int Id { get; protected set; }
    public virtual string? Nome { get; protected set; }
    public virtual TipoPessoa TipoPessoa { get; protected set; }
    public virtual string? CpfCnpj { get; protected set; }
    public virtual string? Email { get; protected set; }
    public virtual string? Telefone { get; protected set; }
    public virtual string? Endereco { get; protected set; }
   public virtual string? Cidade { get; protected set; }
   public virtual string? Cep { get; protected set; }
   public virtual UfEnum? Uf { get; protected set; }
   public virtual RegiaoEnum? Regiao { get; protected set; }
   public virtual string? IE { get; protected set; }
    public virtual string? RazaoSocial { get; protected set; }
     public virtual decimal CustoPorPeso { get; protected set; }
    public virtual decimal Seguro { get; protected set; }

    public virtual IList<ColetaMercadoria>? ColetasMercadoria { get; protected set; }

   protected Cliente()
        { }

        private Cliente(string? nome, string? email, string endereco, string cidade, string cep, UfEnum uf, string telefone, RegiaoEnum regiao)
        {
            SetNome(nome);
            SetEmail(email);
            SetTelefone(telefone);
            SetCep(cep);
            SetCidade(cidade);
            SetEndereco(endereco);
            SetUf(uf);
            SetCustoPorPeso(uf, regiao);
            SetRegiao(regiao);
            SetSeguro(uf, regiao);


        }

        // Construtor Pessoa Fisica
        public Cliente(string? nome, string? email, string endereco, string cidade, string telefone, string cep, UfEnum uf, string cpf, RegiaoEnum regiao) : this(nome, email, endereco, cidade, cep, uf, telefone, regiao)
        {
            SetTipoCliente(TipoPessoa.Fisica);
            SetCPF(cpf);
        }

        // Construtor Pessoa Jurídica
        public Cliente(string? nome, string? email, string endereco, string cidade, string telefone, string cep, UfEnum uf, string cnpj, string ie, string razaoSocial, RegiaoEnum regiao) : this(nome, email, endereco, cidade, cep, uf, telefone, regiao)
        {
            SetTipoCliente(TipoPessoa.Juriridica);
            SetCNPJ(cnpj);
            SetIE(ie);
            SetRazaoSocial(razaoSocial);

        }

        public virtual void SetNome(string? nome)
        {
            if (String.IsNullOrEmpty(nome))
                throw new ArgumentNullException("O nome não pode ser vazio");
            if (nome.Length > 100)
                throw new ArgumentOutOfRangeException("O nome nao pode ter mais que 100 caracteres");
            Nome = nome;
        }

        public virtual void SetEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9]+@\S+\.com(\.\w+)?$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
            {
                throw new Exception("Email com formato invalido");
            }
            Email = email;
        }

        public virtual void SetTipoCliente(TipoPessoa tipoPessoa)
        {
            if (!Enum.IsDefined(tipoPessoa))
            {
                throw new Exception("Tipo não existe");
            }
            TipoPessoa = tipoPessoa;
        }

        public virtual void SetCPF(string? cpf)
        {
            if (String.IsNullOrEmpty(cpf))
                throw new ArgumentNullException("O CPF não pode ser vazio.");
            if (cpf.Length != 11)
                throw new ArgumentOutOfRangeException("O CPF deve conter 11 caracteres.");

            CpfCnpj = cpf.Replace(".", "").Replace("/", "").Replace("-", "");
        }

        public virtual void SetCNPJ(string? cnpj)
        {
            if (String.IsNullOrEmpty(cnpj))
                throw new ArgumentNullException("O CNPJ não pode ser vazio.");
            if (cnpj.Length != 14)
                throw new ArgumentOutOfRangeException("O CNPJ deve conter 14 caracteres.");

            CpfCnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
        }

        public virtual void SetIE(string? ie)
        {
            if (String.IsNullOrEmpty(ie))
                throw new ArgumentNullException("A Inscrição Estadual deve ser preenchida.");
            if (ie.Length != 9)
                throw new ArgumentException("A Inscrição Estadual deve ter 9 digitos.");
            IE = ie.Replace(".", "").Replace("/", "").Replace("-", "");
        }
        public virtual void SetRazaoSocial(string? razaoSocial)
        {
            if (String.IsNullOrEmpty(razaoSocial))
                throw new ArgumentNullException("O campo razao social deve ser preenchido.");
            if (razaoSocial.Length > 100)
                throw new ArgumentException("O campo razao social deve ter no máximo 100 caracteres.");
            RazaoSocial = razaoSocial;
        }
        public virtual void SetTelefone(string telefone)
        {

            if (telefone.Length > 14)
                throw new ArgumentOutOfRangeException("O numero de Telefone deve possuir no máximo 14 caracteres");

            Telefone = telefone;
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
            string cepFormatado = Regex.Replace(cep, @"\W", "");
            if (string.IsNullOrWhiteSpace(cepFormatado))
            {
                throw new Exception("Cep nulo ou com apenas espaços em branco");
            }
            if (cepFormatado.Length > 8 || cepFormatado.Length < 8)
            {
                throw new Exception("Tamanho do cep inválido");
            }
            string regexPattern = @"\D+";
            bool regexTrial = Regex.IsMatch(cepFormatado, regexPattern);
            if (regexTrial)
            {
                throw new Exception("O cep só pode conter caracteres númericos");
            }
            this.Cep = cepFormatado;
        }

    public virtual void SetCidade(string cidade)
    {
        if (String.IsNullOrEmpty(cidade))
                throw new ArgumentException($"O campo {nameof(cidade)} não pode ser vazio.");
                Cidade = cidade;
    }
    public virtual void SetUf(UfEnum? uf)
        {
            Uf = uf;
        }

    public virtual void SetRegiao(RegiaoEnum? regiao)
        {
            Regiao = regiao;
        }
    public virtual void SetCustoPorPeso(UfEnum uf, RegiaoEnum regiao)
{
    switch (uf)
    {
        case UfEnum.BA:
            CustoPorPeso = regiao == RegiaoEnum.Capital ? 2.50m : 2.60m;
            break;

        case UfEnum.ES:
            CustoPorPeso = regiao == RegiaoEnum.Capital ? 1.80m : 2.10m;
            break;

        case UfEnum.RJ:
            CustoPorPeso = regiao == RegiaoEnum.Capital ? 2.30m : 2.50m;
            break;

        default:
            throw new ArgumentException("Combinação UF/Região inválida");
    }

    Uf = uf;
    Regiao = regiao;
}

    public virtual void SetSeguro(UfEnum uf, RegiaoEnum regiao)
        {
            if(uf == UfEnum.RJ && regiao == RegiaoEnum.Capital)
            {
                Seguro = 0.008m;
            }

            else {
                Seguro = 0.005m;
            }
        }

   
    }
}