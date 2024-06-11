using D00_Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RSGymPTv1
{
    class Program
    {
        static void Main(string[] args)
        {
            Utility.SetUnicodeConsole();
            BookingManager bookingManager = new BookingManager();
            bool loggedIn = false;

            while (!loggedIn)
            {
                Console.Clear();
                Utility.WriteTitle("\tWelcome - RSGYMPT - by cp","\n");
                Console.Write("\tNome de utilizador: ");
                string userName = Console.ReadLine();
                Console.Write("\tPalavra-passe: ");
                string password = ReadPassword();

                loggedIn = bookingManager.Login(userName, password);
                
                if (!loggedIn)
                {
                    Console.WriteLine("Nome de utilizador ou palavra-passe incorretos. Tente novamente.");
                    break;
                }
            }

            ShowMainMenu(bookingManager);
        }

        private static void ShowMainMenu(BookingManager bookingManager)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Utility.WriteTitle($"ShowMainMenu--{bookingManager.LoggedInUser.Name}");
                Console.WriteLine("\nMenu de Navegação:");
                Console.WriteLine("\t1. Pedido");
                Console.WriteLine("\t2. Personal Trainer");
                Console.WriteLine("\t3. Utilizador");
                Console.WriteLine("\t4. Logout");

                Console.Write("Escolha uma opção: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowPedidoMenu(bookingManager);
                        break;
                    case "2":
                        ShowPTMenu(bookingManager);
                        break;
                    case "3":
                        ShowUserMenu(bookingManager);
                        break;
                    case "4":
                        bookingManager.Logout();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
        private static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password.Append(info.KeyChar);
                    Console.Write("*");
                }
            }
            return password.ToString();
        }

        private static void ShowPedidoMenu(BookingManager bookingManager)
        {
            Console.Clear();
            Utility.WriteTitle($"\nMenu de PedidoUsuário-- {bookingManager.LoggedInUser.Name}");
            Console.WriteLine("\t1. Registar");
            Console.WriteLine("\t2. Alterar");
            Console.WriteLine("\t3. Eliminar");
            Console.WriteLine("\t4. Consultar");
            Console.WriteLine("\t5. Terminar");

            Console.Write("Escolha uma opção: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Criar uma instância de BookingManager
                    BookingManager bookingManager1 = new BookingManager();
                    bookingManager1.ListarPTsDisponiveis();
                    Console.Write("ID do Personal Trainer: ");
                    int ptId = int.Parse(Console.ReadLine());
                    Console.Write("Data e Hora do Pedido (dd/MM/yyyy HH:mm): ");
                    DateTime dateTime;
                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Console.WriteLine("Data e Hora inválidas. Tente novamente.");
                        break;
                    }
                    // Listar Personal Trainers disponíveis
                   
                    bookingManager.RegistarPedido(ptId, dateTime);
                    break;
                case "2":
                    Console.Write("ID do Pedido a Alterar: ");
                    int pedidoId;
                    if (!int.TryParse(Console.ReadLine(), out pedidoId))
                    {
                        Console.WriteLine("ID do Pedido inválido. Tente novamente.");
                        break;
                    }
                    Console.Write("Nova Data e Hora do Pedido (dd/MM/yyyy HH:mm): ");
                    DateTime newDateTime;
                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDateTime))
                    {
                        Console.WriteLine("Nova Data e Hora inválidas. Tente novamente.");
                        break;
                    }
                    bookingManager.AlterarPedido(pedidoId, newDateTime);
                    break;
                case "3":
                    Console.Write("ID do Pedido a Eliminar: ");
                    if (!int.TryParse(Console.ReadLine(), out int pedidoIdToDelete))
                    {
                        Console.WriteLine("ID do Pedido inválido. Tente novamente.");
                        break;
                    }
                    bookingManager.EliminarPedido(pedidoIdToDelete);
                    break;
                case "4":
                    bookingManager.ConsultarPedidosPorUsuario(bookingManager.LoggedInUser.Id);
                    break;
                case "5":
                    Console.Write("ID do Pedido a Terminar: ");
                    if (!int.TryParse(Console.ReadLine(), out int pedidoIdToFinish))
                    {
                        Console.WriteLine("ID do Pedido inválido. Tente novamente.");
                        break;
                    }
                    bookingManager.TerminarPedido(pedidoIdToFinish);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }

        private static void ShowPTMenu(BookingManager bookingManager)
        {
            Utility.WriteTitle($"\nMenu de Personal Trainer--Usuário: {bookingManager.LoggedInUser.Name}");
            Console.WriteLine("\t1. Pesquisar");
            Console.WriteLine("\t2. Consultar");

            Console.Write("Escolha uma opção: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Personal Trainers Disponíveis:");
                    bookingManager.ListarPTsDisponiveis();
                    Console.Write("Nome do Personal Trainer: ");
                    string ptCode = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(ptCode))
                    {
                        Console.WriteLine("Código do Personal Trainer inválido. Tente novamente.");
                        break;
                    }
                    bookingManager.PesquisarPT(ptCode);
                    break;
                case "2":
                    Console.WriteLine("Personal Trainers Disponíveis:");
                    bookingManager.ListarPTsDisponiveis();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }

        private static void ShowUserMenu(BookingManager bookingManager)
        {
            Utility.WriteTitle($"\nMenu de Utilizador--Usuário: {bookingManager.LoggedInUser.Name}");
            Console.WriteLine("\t1. Consultar");

            Console.Write("Escolha uma opção: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    bookingManager.ListarUsuarios();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}
