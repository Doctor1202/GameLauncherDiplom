using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDiplomca.Class
{
    public class User
    {
        public ObjectId Id { get; private set; }
        public string UserName { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Money { get; private set; }
        public bool IsAdmin { get; private set; }


    }
}
