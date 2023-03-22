using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDiplomca.Class
{
    public class GamePage
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("GameName")]
        public string GameName { get; set; }

        [BsonElement("Publisher")]
        public string Publisher { get; set; }

        [BsonElement("Genre")]
        public string Genre { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("GameCost")]
        public string GameCost { get; set; }

        public GamePage(string gameName, string publisher, string genre, string description, string gameCost)
        {
            GameName = gameName;
            Publisher = publisher;
            Genre = genre;
            Description = description;
            GameCost = gameCost;
        }
    }
}
