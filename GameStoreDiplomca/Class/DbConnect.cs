using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameStoreDiplomca.Class
{
    public class DbConnect
    {
        public static MongoClient dbClient { get; private set; }

        public static void ConnectionToDb()
        {
            try
            {
                dbClient = new MongoClient("mongodb://localhost:27017");

            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error" + ex);
                Environment.Exit(1);
            }
        } 
    }
}
