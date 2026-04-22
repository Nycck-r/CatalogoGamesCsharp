namespace CatalogoJogosAPI.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal PrecoOriginal { get; set; }
        public int AnoLancamento { get; set; }
        public ClassificacaoIndicativa Classificacao { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public decimal CalcularPrecoComDesconto()
        {
            if (DateTime.Now.Year - AnoLancamento >= 5)
            {
                return PrecoOriginal * 0.8m;
            }
            return PrecoOriginal;
        }
    }
}