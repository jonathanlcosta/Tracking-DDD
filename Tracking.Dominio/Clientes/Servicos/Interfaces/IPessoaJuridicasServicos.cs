using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Dominio.Clientes.Servicos.Interfaces
{
    public interface IPessoaJuridicasServico
    {
        Cliente Atualizar(int codigo, string? nome, string? email, string endereco, string cidade, string telefone, string cep, string uf, string cnpj, string ie, string razaoSocial, IList<ColetaMercadoria> coletaMercadorias);
        Cliente Inserir(Cliente cliente);
        Cliente Instanciar(string? nome, string? email, string endereco, string cidade, string telefone, string cep, string uf, string cnpj, string ie, string razaoSocial, IList<ColetaMercadoria> coletaMercadorias);
        Cliente Validar(int codigo);
    }
}