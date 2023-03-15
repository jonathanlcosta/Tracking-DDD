using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.Clientes.PessoaJuridicas.Response
{
    public class PessoaJuridicasResponse
    {
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cnpj { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
   public string? Cidade { get; set; }
   public string? Cep { get; set; }
   public string? Uf { get; set; }
   public string? IE { get; set; }
    public string? RazaoSocial { get; set; }
    }
}