using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static RSGymPTv1.Pedido;


namespace RSGymPTv1
{
    internal class Pedido
    {
        #region properties
        private static int nextId = 1;

        public int PedId { get; set; }
        public int UserId { get; set; }
        public int PTId { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string CancellationReason { get; set; }
        #endregion

        #region Enums
        public enum EnumStatus
        {
            Agendado,
            EmProgresso,
            Concluido,
            Cancelado
        }
        #endregion
        
        #region Constructor
        public Pedido(int userId, int ptId, string status, DateTime timestamp, string cancellationReason)
        {
            PedId = nextId++;
            UserId = userId;
            PTId = ptId;
            Status = status;
            Timestamp = timestamp;
            CancellationReason = cancellationReason;
        }
        #endregion

    }


}
        
    
