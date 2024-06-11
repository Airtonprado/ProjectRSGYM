using System;
using System.Collections.Generic;
using System.Linq;
using static RSGymPTv1.Pedido;


namespace RSGymPTv1
{
    public class BookingManager
    {
        #region Fields
        private List<Pedido> pedidos = new List<Pedido>();
        internal List<User> users = new List<User>();
        internal List<PTTeacher> ptTeachers = new List<PTTeacher>();
        #endregion
        #region Properties
        public User LoggedInUser { get; private set; }
        #endregion
        #region Constructor
        public BookingManager()
        {
            // Inicializar listas com dados fictícios
            users.Add(User.CreateMariaSilva());
            users.Add(User.CreateJoaoSantos());

            ptTeachers.Add(PTTeacher.CreateRayCarneiro());
            ptTeachers.Add(PTTeacher.CreateAmandaTaylor());
            ptTeachers.Add(PTTeacher.CreateRafinhaTigresa());
        }
        #endregion
        #region User Management

        public bool Login(string userName, string password)
        {
            var user = users.FirstOrDefault(u => u.UserCode == userName);
            if (user != null && user.VerifyPassword(password))
            {
                LoggedInUser = user;
                Console.WriteLine("Login bem-sucedido.");
                return true;
            }
            else
            {
                Console.WriteLine("Nome de utilizador ou palavra-passe incorretos.");
                return false;
            }
        }


        public void Logout()
        {
            LoggedInUser = null;
        }
        #endregion
        #region Methods
        public void ListarPTsDisponiveis()
        {
            var ptIdsAgendados = pedidos.Select(p => p.PTId).Distinct().ToList();
            var ptDisponiveis = ptTeachers.Where(pt => !ptIdsAgendados.Contains(pt.Id)).ToList();

            Console.WriteLine("Personal Trainers Disponíveis:");
            if (ptDisponiveis.Count == 0)
            {
                Console.WriteLine("Nenhum Personal Trainer disponível no momento.");
            }
            else
            {
                foreach (var pt in ptDisponiveis)
                {
                    Console.WriteLine($"ID: {pt.Id}, Nome: {pt.Name}, Telefone: {pt.Fone}");
                }
            }
        }


        public void RegistarPedido(int ptId, DateTime dateTime)
        {
            var user = LoggedInUser;
            var pt = ptTeachers.FirstOrDefault(t => t.Id == ptId);

            Console.Write("Escolha o ID do Personal Trainer: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                return;
            }
            if (dateTime < DateTime.Now)
            {
                Console.WriteLine("A data do pedido não pode ser anterior à data atual.");
                return;
            }

            try
            {
                Pedido.EnumStatus status = Pedido.EnumStatus.Agendado;
                Pedido novoPedido = new Pedido(LoggedInUser.Id, ptId, status.ToString(), dateTime, null); // Correção aqui: Convertendo o status para string
                pedidos.Add(novoPedido);

                Console.WriteLine($"Pedido registrado com sucesso. ID: {novoPedido.PedId}, Status: {novoPedido.Status}, Data: {novoPedido.Timestamp:dd/MM/yyyy HH:mm}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Ocorreu um erro ao registrar o pedido: Parâmetro inválido. Detalhes: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao registrar o pedido: {ex.Message}");
                // Você pode querer registrar a exceção para análise posterior
            }
        }



        public void AlterarPedido(int pedidoId, DateTime newDateTime)
        {
            var pedido = pedidos.FirstOrDefault(p => p.PedId == pedidoId && p.Status == Pedido.EnumStatus.Agendado.ToString());

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado ou não pode ser alterado.");
                return;
            }

            pedido.Timestamp = newDateTime;
            Console.WriteLine("Pedido alterado com sucesso.");
        }

        public void EliminarPedido(int pedidoId)
        {
            var pedido = pedidos.FirstOrDefault(p => p.PedId == pedidoId && p.Status == Pedido.EnumStatus.Agendado.ToString());


            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado ou não pode ser eliminado.");
                return;
            }

            pedidos.Remove(pedido);
            Console.WriteLine("Pedido eliminado com sucesso.");
        }

        public void ConsultarPedidosPorUsuario(int userId)
        {
            var userPedidos = pedidos.Where(p => p.UserId == userId).ToList();

            if (userPedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado para o usuário.");
                return;
            }

            foreach (var pedido in userPedidos)
            {
                Console.WriteLine($"Pedido ID: {pedido.PedId}, Status: {pedido.Status}, Data: {pedido.Timestamp:dd/MM/yyyy HH:mm}");
            }
        }

        public void TerminarPedido(int pedidoId)
        {
            var pedido = pedidos.FirstOrDefault(p => p.PedId == pedidoId && p.Status == Pedido.EnumStatus.Agendado.ToString());

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado ou não pode ser terminado.");
                return;
            }

            pedido.Status = Pedido.EnumStatus.Concluido.ToString();
            pedido.Timestamp = DateTime.Now;
            Console.WriteLine("Pedido terminado com sucesso.");
        }


        public void PesquisarPTPorCodigo(string ptCode)
        {
            var pt = ptTeachers.FirstOrDefault(t => t.Name == ptCode);

            if (pt == null)
            {
                Console.WriteLine("PT não encontrado.");
                return;
            }

            Console.WriteLine($"PT encontrado: {pt.Name}, Telefone: {pt.Fone}");
        }

        public void PesquisarPT(string ptName)
        {
            var pt = ptTeachers.FirstOrDefault(t => t.Name.Equals(ptName, StringComparison.OrdinalIgnoreCase));

            if (pt == null)
            {
                Console.WriteLine("PT não encontrado.");
                return;
            }

            Console.WriteLine($"PT encontrado: ID: {pt.Id}, Nome: {pt.Name}, Telefone: {pt.Fone}");
        }


        public void ListarUsuarios()
        {
            foreach (var user in users)
            {
                 Console.WriteLine($"ID: {user.Id}, Nome: {user.Name}, Código: {user.UserCode}");
            }
        }
        #endregion   
    }
}
