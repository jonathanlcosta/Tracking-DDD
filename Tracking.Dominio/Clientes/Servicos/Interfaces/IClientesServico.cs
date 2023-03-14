using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Enumeradores;

namespace Tracking.Dominio.Clientes.Servicos.Interfaces
{
    public interface IClientesServico
    {
         Cliente Validar(int codigo);
        TipoPessoa GetTipoCliente(Cliente cliente);
    }
}