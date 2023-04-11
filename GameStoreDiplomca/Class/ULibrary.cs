using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDiplomca.Class
{
    public class ULibrary
    {
        public ObjectId Id { get; private set; }
        public string UserName { get; private set; }
        public string GameName { get;  set; }
    }
}
