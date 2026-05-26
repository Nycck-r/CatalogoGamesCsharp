using CatalogoJogosAPI.Models;
using CatalogoJogosAPI.Models;
using CatalogoJogosAPI.Repositories;
using CatalogoJogosAPI.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogosAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoRepositorio _repositorio;

        public JogosController(IJogoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var jogos = _repositorio.ObterTodos().Select(j => new {
                j.Id,
                j.Titulo,
                j.PrecoOriginal,
                PrecoComDesconto = j.CalcularPrecoComDesconto(),
                j.AnoLancamento,
                j.Classificacao,
                Categoria = j.Categoria?.Nome
            });
            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var jogo = _repositorio.ObterPorId(id);
            if (jogo == null) return NotFound("Jogo não encontrado.");
            return Ok(jogo);
        }

        [HttpPost]
        public IActionResult Criar(Jogo jogo)
        {
            if (string.IsNullOrWhiteSpace(jogo.Titulo)) return BadRequest("O título é obrigatório.");
            if (jogo.PrecoOriginal <= 0) return BadRequest("O preço deve ser maior que zero.");
            if (jogo.AnoLancamento > DateTime.Now.Year) return BadRequest("Ano de lançamento inválido.");

            _repositorio.Adicionar(jogo);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogo.Id }, jogo);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Jogo jogoAtualizado)
        {
            if (id != jogoAtualizado.Id) return BadRequest("IDs não conferem.");

            var jogoExistente = _repositorio.ObterPorId(id);
            if (jogoExistente == null) return NotFound("Jogo não encontrado.");

            
            jogoExistente.Titulo = jogoAtualizado.Titulo;
            jogoExistente.PrecoOriginal = jogoAtualizado.PrecoOriginal;
            jogoExistente.AnoLancamento = jogoAtualizado.AnoLancamento;
            jogoExistente.Classificacao = jogoAtualizado.Classificacao;
            jogoExistente.CategoriaId = jogoAtualizado.CategoriaId;

            _repositorio.Atualizar(jogoExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            if (_repositorio.ObterPorId(id) == null) return NotFound("Jogo não encontrado.");

            _repositorio.Remover(id);
            return NoContent();
        }
    }
}