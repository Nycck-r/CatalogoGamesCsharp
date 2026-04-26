using CatalogoJogosAPI.Models;
using CatalogoJogosAPI.Models;
using CatalogoJogosAPI.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepositorio _repositorio;

        public CategoriasController(ICategoriaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(_repositorio.ObterTodas());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var categoria = _repositorio.ObterPorId(id);
            if (categoria == null) return NotFound("Categoria não encontrada.");
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Criar(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome) || categoria.Nome.Length < 3)
                return BadRequest("O nome da categoria é obrigatório e deve ter no mínimo 3 caracteres.");

            if (_repositorio.ObterPorNome(categoria.Nome) != null)
                return BadRequest("Já existe uma categoria cadastrada com este nome.");

            _repositorio.Adicionar(categoria);
            return CreatedAtAction(nameof(ObterPorId), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Categoria categoriaAtualizada)
        {
            if (id != categoriaAtualizada.Id) return BadRequest("IDs não conferem.");

            var categoriaExistente = _repositorio.ObterPorId(id);
            if (categoriaExistente == null) return NotFound("Categoria não encontrada.");

            if (string.IsNullOrWhiteSpace(categoriaAtualizada.Nome) || categoriaAtualizada.Nome.Length < 3)
                return BadRequest("O nome da categoria é obrigatório e deve ter no mínimo 3 caracteres.");

            
            categoriaExistente.Nome = categoriaAtualizada.Nome;

            _repositorio.Atualizar(categoriaExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var categoria = _repositorio.ObterPorId(id);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            if (categoria.Jogos.Any())
                return BadRequest("Não é possível excluir esta categoria pois existem jogos vinculados a ela.");

            _repositorio.Remover(id);
            return NoContent();
        }
    }
}