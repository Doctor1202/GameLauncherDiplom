using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDiplomca.Class
{
    public class ULibrary
    {

        [BsonElement("GameName")]
        public string GameName { get;  set; }
        [BsonElement("IsDownLoad")]
        public bool IsDownloud { get;  set; }

    }
}
