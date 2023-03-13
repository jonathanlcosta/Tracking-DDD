using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.Transportadoras.Response
{
    public class TransportadoraResponse
    {
    public int CodigoTransportadora { get; set; }
    public string? RazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }
    public string? Cnpj { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? Email1 { get; set; }
    public string? Email2 { get; set; }
    public string? NumeroTelefone1 { get; set; }
    public string? NumeroTelefone2 { get; set; }
    public string? Endereco { get; set; }
    public string? Cidade { get; set; }
    public string? Cep { get; set; }
    public string? Uf{ get; set; }
    public string? Site { get; set; }
    }
}