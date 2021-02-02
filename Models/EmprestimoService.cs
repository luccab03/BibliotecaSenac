using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService
    {
        public void Inserir(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                emprestimo.Devolvido = e.Devolvido;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro = null)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                if (filtro != null)
                {
                    if (filtro.TipoFiltro == "Usuario")
                    {
                        return bc.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro)).OrderByDescending(e => e.DataDevolucao).ToList();
                    }
                    if (filtro.TipoFiltro == "Livro")
                    {
                        return bc.Emprestimos.Where(e => e.Livro.Titulo.Contains(filtro.Filtro)).OrderByDescending(e => e.DataDevolucao).ToList();
                    }
                }


                return bc.Emprestimos.ToList();
            }
        }
        public Emprestimo ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}