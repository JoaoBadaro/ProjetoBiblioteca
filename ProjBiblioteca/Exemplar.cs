using System;
using System.Collections.Generic;

namespace Biblioteca
{
    public class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        public int Tombo
        {
            get { return tombo; }
            set { tombo = value; }
        }

        public List<Emprestimo> Emprestimos
        {
            get { return emprestimos; }
        }

        public Exemplar(int tombo)
        {
            this.tombo = tombo;
            this.emprestimos = new List<Emprestimo>();
        }

        // Registra um novo empréstimo, se o exemplar estiver disponível.
        public bool Emprestar()
        {
            if (!Disponivel())
                return false;

            emprestimos.Add(new Emprestimo());
            return true;
        }

        // Registra a devolução do empréstimo em aberto, se houver.
        public bool Devolver()
        {
            if (Disponivel())
                return false;

            emprestimos[emprestimos.Count - 1].DtDevolucao = DateTime.Now;
            return true;
        }

        // Disponível quando não há empréstimo ou o último já foi devolvido.
        public bool Disponivel()
        {
            if (emprestimos.Count == 0)
                return true;

            return emprestimos[emprestimos.Count - 1].DtDevolucao.HasValue;
        }

        public int QtdeEmprestimos()
        {
            return emprestimos.Count;
        }
    }
}
