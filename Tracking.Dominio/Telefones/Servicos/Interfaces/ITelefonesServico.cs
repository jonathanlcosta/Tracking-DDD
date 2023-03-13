using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Telefones.Servicos.Interfaces
{
    public interface ITelefonesServico
    {
        Telefone Validar(int codigo);
        Telefone Instanciar(string tipoTelefone, string numeroTelefone, Transportadora transportadora);
        Telefone Editar(int codigo, string tipoTelefone, string numeroTelefone, Transportadora transportadora);
        Telefone Inserir(Telefone telefone);
    }
}