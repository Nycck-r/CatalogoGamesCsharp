using CatalogoJogosAPI.Modelos;

namespace CatalogoJogosAPI.Repositorios
{
    public interface IJogoRepositorio
    {
        IEnumerable<Jogo> ObterTodos();
        Jogo? ObterPorId(int id);
        void Adicionar(Jogo jogo);
        void Atualizar(Jogo jogo);
        void Remover(int id);
    }
}