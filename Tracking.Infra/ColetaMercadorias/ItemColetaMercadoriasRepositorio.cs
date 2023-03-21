using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.ColetaMercadorias
{
    public class ItemColetaMercadoriasRepositorio : GenericoRepositorio<ItemColetaMercadoria>, IItemColetaMercadoriasRepositorio
    {
      public ItemColetaMercadoriasRepositorio(ISession session) : base(session)
      {
        
      }  
    }
}