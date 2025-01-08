using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDiplomca.Class
{
    public class User
    {

        [BsonId]
        public ObjectId Id { get; private set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Login")]
        public string Login { get; private set; }

        [BsonElement("Password")]
        public string Password { get; private set; }

        [BsonElement("Money")]
        public int Money { get; set; }

        [BsonElement("IsAdmin")]
        public bool IsAdmin { get; private set; }

        [BsonElement("ULibrary")]
        public List <ULibrary> ULibrary { get; set; }
        public User(string userName, string login, string password, int money, bool isAdmin, List<ULibrary> uLibrary)
        {
            UserName = userName;
            Login = login;
            Password = password;
            Money = money;
            IsAdmin = isAdmin;
            ULibrary = uLibrary;
        }
    }
}
