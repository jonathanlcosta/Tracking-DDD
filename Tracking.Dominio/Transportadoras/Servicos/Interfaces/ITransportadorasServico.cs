using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Transportadoras.Servicos.Interfaces
{
    public interface ITransportadorasServico
    {
        Transportadora Validar(int codigoTransportadora);
        Transportadora Inserir(Transportadora transportadora);
        void Excluir(int codigoTransportadora);
        Transportadora Instanciar(string razaoSocial, string nomeFantasia, 
    string cnpj, string inscricaoEstadual, IList<Email> emails, IList<Telefone> telefones, string endereco,
    string cidade, string cep, string uf, string site);
        Transportadora Editar(int codigoTransportadora, string razaoSocial, string nomeFantasia, 
    string cnpj, string inscricaoEstadual, IList<Email> emails, IList<Telefone> telefones, string endereco,
    string cidade, string cep, string uf, string site);
    }
}