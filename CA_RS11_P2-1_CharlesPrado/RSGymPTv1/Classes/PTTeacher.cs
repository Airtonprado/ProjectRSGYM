using RSGymPTv1;
using System;

namespace RSGymPTv1
{
    public class PTTeacher
    {
        #region Properties
        private static int idCounter = 1;
        public int Id { get; set; }

        public string Name { get; set; }
        public string Client { get; set; }

        public string Trainer { get; set; }

        public string Fone { get; set; }

        #endregion
        #region Constructor

        public PTTeacher(string client, string namept, string trainer, string fone)
        {
            Id = idCounter++;
            Client = client;
            Name = namept;
            Trainer = trainer;
            Fone = fone;

            
        }
        #endregion
        #region PT Ficticios
        public static PTTeacher CreateRayCarneiro()
        {
            return new PTTeacher("1", "Ray Carneiro", "Personal", "+351-93355665");
        }

        public static PTTeacher CreateAmandaTaylor()
        {
            return new PTTeacher("2", "Amanda Taylor", "Personal Fitness", "+351-92244665");
        }

        public static PTTeacher CreateRafinhaTigresa()
        {
            return new PTTeacher("3", "Rafinha Tigresa", "Personal Zumba", "+351-92277665");
        }
        #endregion
    }
}
