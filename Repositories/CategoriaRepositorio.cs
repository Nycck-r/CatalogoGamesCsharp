using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Models;
using CatalogoJogosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoJogosAPI.Repositorios
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly CatalogoContext _contexto;

        public CategoriaRepositorio(CatalogoContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Categoria> ObterTodas()
        {
            return _contexto.Categorias.Include(c => c.Jogos).ToList();
        }

        public Categoria? ObterPorId(int id)
        {
            return _contexto.Categorias.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
        }

        public Categoria? ObterPorNome(string nome)
        {
            
            return _contexto.Categorias.FirstOrDefault(c => c.Nome.ToLower() == nome.ToLower());
        }

        public void Adicionar(Categoria categoria)
        {
            _contexto.Categorias.Add(categoria);
            _contexto.SaveChanges();
        }

        public void Atualizar(Categoria categoria)
        {
            _contexto.Categorias.Update(categoria);
            _contexto.SaveChanges();
        }

        public void Remover(int id)
        {
            var categoria = ObterPorId(id);
            if (categoria != null)
            {
                _contexto.Categorias.Remove(categoria);
                _contexto.SaveChanges();
            }
        }
    }
}