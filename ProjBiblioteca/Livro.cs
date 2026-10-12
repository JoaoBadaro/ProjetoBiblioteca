using System.Collections.Generic;

namespace Biblioteca
{
    public class Livro
    {
        private long isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares;

        public long Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        public string Editora
        {
            get { return editora; }
            set { editora = value; }
        }

        public List<Exemplar> Exemplares
        {
            get { return exemplares; }
        }

        public Livro(long isbn, string titulo, string autor, string editora)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            this.exemplares = new List<Exemplar>();
        }

        // Construtor auxiliar, usado apenas como "chave" de pesquisa em Livros.Pesquisar.
        public Livro(long isbn) : this(isbn, "", "", "")
        {
        }

        public void AdicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        public int QtdeExemplares()
        {
            return exemplares.Count;
        }

        public int QtdeDisponiveis()
        {
            int total = 0;
            foreach (Exemplar e in exemplares)
            {
                if (e.Disponivel())
                    total++;
            }
            return total;
        }

        public int QtdeEmprestimos()
        {
            int total = 0;
            foreach (Exemplar e in exemplares)
                total += e.QtdeEmprestimos();
            return total;
        }

        public double PercDisponibilidade()
        {
            if (exemplares.Count == 0)
                return 0;

            return (double)QtdeDisponiveis() / QtdeExemplares() * 100;
        }

        // Auxiliar: localiza um exemplar do livro pelo tombo (null se não existir).
        public Exemplar BuscarExemplar(int tombo)
        {
            foreach (Exemplar e in exemplares)
            {
                if (e.Tombo == tombo)
                    return e;
            }
            return null;
        }
    }
}
