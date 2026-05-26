using CatalogoJogosAPI.Data;
using CatalogoJogosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

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
            
            var categoriaExiste = _contexto.Categorias.Any(c => c.Id == jogo.CategoriaId);
            if (!categoriaExiste)
            {
                var primeira = _contexto.Categorias.FirstOrDefault();
                if (primeira != null)
                    jogo.CategoriaId = primeira.Id;
                else
                    throw new Exception("Você precisa criar pelo menos uma Categoria antes de adicionar um jogo!");
            }

            _contexto.Jogos.Add(jogo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Jogo jogo)
        {
            var jogoExistente = _contexto.Jogos.Find(jogo.Id);

            if (jogoExistente != null)
            {
                jogoExistente.Titulo = jogo.Titulo;
                jogoExistente.PrecoOriginal = jogo.PrecoOriginal;
                jogoExistente.AnoLancamento = jogo.AnoLancamento;
                jogoExistente.Classificacao = jogo.Classificacao;

                
                var categoriaExiste = _contexto.Categorias.Any(c => c.Id == jogo.CategoriaId);

                if (categoriaExiste)
                {
                    jogoExistente.CategoriaId = jogo.CategoriaId;
                }
                else
                {
                    
                    var primeira = _contexto.Categorias.FirstOrDefault();
                    if (primeira != null)
                    {
                        jogoExistente.CategoriaId = primeira.Id;
                    }
                }

                _contexto.SaveChanges();
            }
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