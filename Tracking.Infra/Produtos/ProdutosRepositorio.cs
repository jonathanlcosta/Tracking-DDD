using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.Produtos
{
    public class ProdutosRepositorio : GenericoRepositorio<Produto>, IProdutosRepositorio
    {
        public ProdutosRepositorio(ISession session) : base(session)
        {
            
        }
    }
}