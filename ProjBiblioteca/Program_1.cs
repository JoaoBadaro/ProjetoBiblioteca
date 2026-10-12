using System;

namespace Biblioteca
{
    class Program
    {
        static Livros livros = new Livros();

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                MostrarMenu();
                opcao = LerInt("Opção: ");
                Console.WriteLine();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando...");
                        break;
                    case 1:
                        AdicionarLivro();
                        break;
                    case 2:
                        PesquisarSintetico();
                        break;
                    case 3:
                        PesquisarAnalitico();
                        break;
                    case 4:
                        AdicionarExemplar();
                        break;
                    case 5:
                        RegistrarEmprestimo();
                        break;
                    case 6:
                        RegistrarDevolucao();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine();
                    Console.Write("Pressione ENTER para continuar...");
                    Console.ReadLine();
                }
            } while (opcao != 0);
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("=== BIBLIOTECA ===");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("1 - Adicionar livro");
            Console.WriteLine("2 - Pesquisar livro (sintético)");
            Console.WriteLine("3 - Pesquisar livro (analítico)");
            Console.WriteLine("4 - Adicionar exemplar");
            Console.WriteLine("5 - Registrar empréstimo");
            Console.WriteLine("6 - Registrar devolução");
            Console.WriteLine();
        }

        // ---------- Opções do menu ----------

        static void AdicionarLivro()
        {
            Console.WriteLine("--- Adicionar livro ---");
            long isbn = LerLong("ISBN: ");

            if (livros.Pesquisar(new Livro(isbn)) != null)
            {
                Console.WriteLine("Já existe um livro cadastrado com esse ISBN.");
                return;
            }

            string titulo = LerTexto("Título: ");
            string autor = LerTexto("Autor: ");
            string editora = LerTexto("Editora: ");

            livros.Adicionar(new Livro(isbn, titulo, autor, editora));
            Console.WriteLine("Livro adicionado com sucesso!");
        }

        static void PesquisarSintetico()
        {
            Console.WriteLine("--- Pesquisar livro (sintético) ---");
            Livro livro = BuscarLivro();
            if (livro == null) return;

            MostrarDadosSinteticos(livro);
        }

        static void PesquisarAnalitico()
        {
            Console.WriteLine("--- Pesquisar livro (analítico) ---");
            Livro livro = BuscarLivro();
            if (livro == null) return;

            MostrarDadosSinteticos(livro);

            Console.WriteLine();
            Console.WriteLine("Exemplares:");
            if (livro.QtdeExemplares() == 0)
            {
                Console.WriteLine("  (nenhum exemplar cadastrado)");
                return;
            }

            foreach (Exemplar e in livro.Exemplares)
            {
                string situacao = e.Disponivel() ? "Disponível" : "Emprestado";
                Console.WriteLine($"  Tombo: {e.Tombo} | {situacao} | Empréstimos: {e.QtdeEmprestimos()}");

                foreach (Emprestimo emp in e.Emprestimos)
                {
                    string devolucao = emp.DtDevolucao.HasValue
                        ? emp.DtDevolucao.Value.ToString("dd/MM/yyyy HH:mm")
                        : "ainda não devolvido";
                    Console.WriteLine($"      Empréstimo: {emp.DtEmprestimo:dd/MM/yyyy HH:mm} | Devolução: {devolucao}");
                }
            }
        }

        static void AdicionarExemplar()
        {
            Console.WriteLine("--- Adicionar exemplar ---");
            Livro livro = BuscarLivro();
            if (livro == null) return;

            int tombo = LerInt("Tombo do exemplar: ");
            if (livro.BuscarExemplar(tombo) != null)
            {
                Console.WriteLine("Já existe um exemplar com esse tombo para este livro.");
                return;
            }

            livro.AdicionarExemplar(new Exemplar(tombo));
            Console.WriteLine("Exemplar adicionado com sucesso!");
        }

        static void RegistrarEmprestimo()
        {
            Console.WriteLine("--- Registrar empréstimo ---");
            Exemplar exemplar = BuscarExemplar();
            if (exemplar == null) return;

            if (exemplar.Emprestar())
                Console.WriteLine("Empréstimo registrado com sucesso!");
            else
                Console.WriteLine("Não foi possível emprestar: o exemplar já está emprestado.");
        }

        static void RegistrarDevolucao()
        {
            Console.WriteLine("--- Registrar devolução ---");
            Exemplar exemplar = BuscarExemplar();
            if (exemplar == null) return;

            if (exemplar.Devolver())
                Console.WriteLine("Devolução registrada com sucesso!");
            else
                Console.WriteLine("Não foi possível devolver: o exemplar não possui empréstimo em aberto.");
        }

        // ---------- Auxiliares ----------

        static void MostrarDadosSinteticos(Livro livro)
        {
            Console.WriteLine($"ISBN: {livro.Isbn}");
            Console.WriteLine($"Título: {livro.Titulo}");
            Console.WriteLine($"Autor: {livro.Autor}");
            Console.WriteLine($"Editora: {livro.Editora}");
            Console.WriteLine($"Total de exemplares: {livro.QtdeExemplares()}");
            Console.WriteLine($"Exemplares disponíveis: {livro.QtdeDisponiveis()}");
            Console.WriteLine($"Total de empréstimos: {livro.QtdeEmprestimos()}");
            Console.WriteLine($"Disponibilidade: {livro.PercDisponibilidade():F2}%");
        }

        static Livro BuscarLivro()
        {
            long isbn = LerLong("ISBN do livro: ");
            Livro livro = livros.Pesquisar(new Livro(isbn));
            if (livro == null)
                Console.WriteLine("Livro não encontrado.");
            return livro;
        }

        static Exemplar BuscarExemplar()
        {
            Livro livro = BuscarLivro();
            if (livro == null) return null;

            int tombo = LerInt("Tombo do exemplar: ");
            Exemplar exemplar = livro.BuscarExemplar(tombo);
            if (exemplar == null)
                Console.WriteLine("Exemplar não encontrado para este livro.");
            return exemplar;
        }

        static string LerTexto(string mensagem)
        {
            string texto;
            do
            {
                Console.Write(mensagem);
                texto = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(texto));
            return texto.Trim();
        }

        static int LerInt(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. " + mensagem);
            }
            return valor;
        }

        static long LerLong(string mensagem)
        {
            long valor;
            Console.Write(mensagem);
            while (!long.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. " + mensagem);
            }
            return valor;
        }
    }
}
