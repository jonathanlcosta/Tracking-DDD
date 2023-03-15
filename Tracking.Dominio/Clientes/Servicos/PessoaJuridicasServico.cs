using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Enumeradores;
using Tracking.Dominio.Clientes.Repositorios;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Dominio.Clientes.Servicos
{
    public class PessoaJuridicasServico : IPessoaJuridicasServico
    {
        private readonly IClientesRepositorio clientesRepositorio;

        public PessoaJuridicasServico(IClientesRepositorio clientesRepositorio)
        {
            this.clientesRepositorio = clientesRepositorio;
        }
        public Cliente Atualizar(int codigo, string? nome, string? email, string endereco, string cidade, string telefone, string cep, string uf, string cnpj, string ie, string razaoSocial, IList<ColetaMercadoria> coletaMercadorias)
        {
             Cliente cliente = Validar(codigo);
            if (!String.IsNullOrEmpty(nome)) cliente.SetNome(nome);
            if (!String.IsNullOrEmpty(email)) cliente.SetEmail(email);
            if (!String.IsNullOrEmpty(cnpj)) cliente.SetCNPJ(cnpj);
            if (!String.IsNullOrEmpty(ie)) cliente.SetIE(ie);
             if (!String.IsNullOrEmpty(endereco)) cliente.SetEndereco(endereco);
            if (!String.IsNullOrEmpty(razaoSocial)) cliente.SetRazaoSocial(razaoSocial);
            if (!String.IsNullOrEmpty(telefone)) cliente.SetTelefone(telefone);
            if (!String.IsNullOrEmpty(cidade)) cliente.SetCidade(cidade);
            if (!String.IsNullOrEmpty(cep)) cliente.SetCep(cep);
            if (!String.IsNullOrEmpty(uf)) cliente.SetUf(uf);
            return cliente;
        }

        public Cliente Inserir(Cliente cliente)
        {
            return clientesRepositorio.Inserir(cliente);
        }

        public Cliente Instanciar(string? nome, string? email, string endereco, string cidade, string telefone, string cep, string uf, string cnpj, string ie, string razaoSocial, IList<ColetaMercadoria> coletaMercadorias)
        {
            return new Cliente(nome, email, endereco, cidade, telefone, cep, uf, cnpj, ie, razaoSocial, coletaMercadorias);
        }

        public Cliente Validar(int codigo)
        {
           if (codigo == 0)
                throw new ArgumentException("Insira um codigo de cliente válido");

            var cliente = clientesRepositorio.Recuperar(codigo);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (cliente.TipoPessoa != TipoPessoa.Juriridica)
                throw new Exception("Esse cliente não é uma pessoa juridica.");

            return cliente;
        }
    }
}