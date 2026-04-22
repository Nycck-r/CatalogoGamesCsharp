using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoJogosAPI.Repositories
{
    public class JogoRepositorio : IJogoRepositorio
    {
        private readonly CatalogoContext _contexto;

        public JogoRepositorio(CatalogoContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Jogo> ObterTodos()
        {
            return _contexto.Jogos.Include(j => j.Categoria).ToList();
        }

        public Jogo? ObterPorId(int id)
        {
            return _contexto.Jogos.Include(j => j.Categoria).FirstOrDefault(j => j.Id == id);
        }

        public void Adicionar(Jogo jogo)
        {
            _contexto.Jogos.Add(jogo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Jogo jogo)
        {
            _contexto.Jogos.Update(jogo);
            _contexto.SaveChanges();
        }

        public void Remover(int id)
        {
            var jogo = ObterPorId(id);
            if (jogo != null)
            {
                _contexto.Jogos.Remove(jogo);
                _contexto.SaveChanges();
            }
        }
    }
}