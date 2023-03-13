using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Produtos.Repositorios;

namespace Tracking.Dominio.Generico.Repositorios
{
    public interface IGenericoRepositorio<T> where T : class
    {
        T Recuperar(int id);

        T Inserir(T entidade);

        T Editar(T entidade);

        void Excluir(T entidade);

        IList<T> Listar(IQueryable<T> query);

        IQueryable<T> Query();
    }
}