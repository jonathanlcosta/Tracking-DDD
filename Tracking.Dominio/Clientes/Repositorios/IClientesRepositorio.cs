using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Generico.Repositorios;

namespace Tracking.Dominio.Clientes.Repositorios
{
    public interface IClientesRepositorio : IGenericoRepositorio<Cliente>
    {
        
    }
}