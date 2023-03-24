using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Enumeradores;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Dominio.Clientes.Servicos.Interfaces
{
    public interface IPessoaFisicasServico
    {
        Cliente Atualizar(int codigo, string? nome, string? email, string endereco, string cidade, string telefone, string cep, UfEnum uf, string cpf, RegiaoEnum regiao);
        Cliente Inserir(Cliente cliente);
        Cliente Instanciar(string? nome, string? email, string endereco, string cidade, string telefone, string cep, UfEnum uf, string cpf, RegiaoEnum regiao);
        Cliente Validar(int codigo);
    }
}