namespace CatalogoJogosAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public List<Jogo> Jogos { get; set; }

        public Categoria()
        {
            Jogos = new List<Jogo>();
        }
    }
}