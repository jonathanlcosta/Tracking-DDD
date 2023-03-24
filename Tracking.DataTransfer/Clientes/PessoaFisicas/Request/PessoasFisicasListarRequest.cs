using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Enumeradores;

namespace Tracking.DataTransfer.Clientes.PessoaFisicas.Request
{
    public class PessoaFisicasListarRequest
    {
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
   public string? Cidade { get; set; }
   public string? Cep { get; set; }
   public UfEnum? Uf { get; set; }
   public RegiaoEnum? Regiao { get; set; }
    }
}