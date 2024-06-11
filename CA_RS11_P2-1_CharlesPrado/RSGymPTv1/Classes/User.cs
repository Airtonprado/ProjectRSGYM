using System;

namespace RSGymPTv1
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string UserCode { get; private set; }
        private string Password { get; set; }

        private static int _idCounter = 1;

        private static int GenerateId()
        {
            return _idCounter++;
        }
        public User(string name, DateTime birthDate, string userCode, string password)
        {
            Id = GenerateId();
            Name = name;
            BirthDate = birthDate;
            UserCode = userCode;
            Password = password;
        }

        public bool VerifyPassword(string password)
        {
            return Password == password;
        }


        public static User CreateMariaSilva()
        {
            return new User("Maria Silva", new DateTime(1990, 5, 15), "Maria01", "senha123");
        }

        public static User CreateJoaoSantos()
        {
            return new User("João Santos", new DateTime(1995, 5, 20), "Joao", "senha456");
        }
    }
}
