using System.Collections.Generic;

namespace Biblioteca
{
    public class Livros
    {
        private List<Livro> acervo;

        public Livros()
        {
            acervo = new List<Livro>();
        }

        public void Adicionar(Livro livro)
        {
            acervo.Add(livro);
        }

        // Pesquisa pelo ISBN do livro informado. Retorna null se não encontrar.
        public Livro Pesquisar(Livro livro)
        {
            foreach (Livro l in acervo)
            {
                if (l.Isbn == livro.Isbn)
                    return l;
            }
            return null;
        }
    }
}
