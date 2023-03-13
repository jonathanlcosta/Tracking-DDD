using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Generico.Repositorios;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Dominio.Produtos.Repositorios
{
    public interface IProdutosRepositorio : IGenericoRepositorio<Produto>
    {
        
    }
}