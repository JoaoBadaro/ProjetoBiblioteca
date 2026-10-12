using System;

namespace Biblioteca
{
    public class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime? dtDevolucao; // null enquanto o exemplar não foi devolvido

        public DateTime DtEmprestimo
        {
            get { return dtEmprestimo; }
            set { dtEmprestimo = value; }
        }

        public DateTime? DtDevolucao
        {
            get { return dtDevolucao; }
            set { dtDevolucao = value; }
        }

        public Emprestimo()
        {
            dtEmprestimo = DateTime.Now;
            dtDevolucao = null;
        }
    }
}
