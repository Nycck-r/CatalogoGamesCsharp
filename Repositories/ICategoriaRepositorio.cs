using CatalogoJogosAPI.Models;
using CatalogoJogosAPI.Models;

namespace CatalogoJogosAPI.Repositorios
{
    public interface ICategoriaRepositorio
    {
        IEnumerable<Categoria> ObterTodas();
        Categoria? ObterPorId(int id);
        Categoria? ObterPorNome(string nome); // Criamos isso para checar se o nome já existe
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Remover(int id);
    }
}