using GameStoreDiplomca.Class;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameStoreDiplomca.Windows
{
    /// <summary>
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
        }

        private void AddNewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            string gameName = GameName_TextBox.Text;
            string publisher = PublisherName_TextBox.Text;
            string genre = GenreName_TextBox.Text;
            string descrioption = Description_TextBox.Text;
            string cost = Cost_TextBox.Text;

            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<BsonDocument>("GamePage");
            var filter = Builders<BsonDocument>.Filter.Eq("GameName", gameName);
            var filter2 = collection.Find(filter).FirstOrDefault();

            var gamePageInfo = new BsonDocument
            {
                {"GameName", gameName},
                {"Publisher", publisher},
                {"Genre", genre},
                {"Description", descrioption},
                {"GameCost", cost}
            };

            if(filter2 is not null)
            {
                MessageBox.Show("Can`t create same GamePages");
            }
            else
            {
                MessageBox.Show("Create new GamePage!");
                collection.InsertOne(gamePageInfo);
            }

        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close() ;
        }
    }
}
