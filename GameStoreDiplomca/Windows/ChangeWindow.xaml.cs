using GameStoreDiplomca.Class;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {

        public ChangeWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
        }


        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
            string gameName = GameName_TextBox.Text;
            string publisher = PublisherName_TextBox.Text;
            string genre = GenreName_TextBox.Text;
            string descrioption = Description_TextBox.Text;
            string cost = Cost_TextBox.Text;
            string search = Search_TextBox.Text;


            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<BsonDocument>("GamePage");
            var filter = Builders<BsonDocument>.Filter.Eq("GameName", search);
            var filter2 = collection.Find(filter).FirstOrDefault();


            var update = Builders<BsonDocument>.Update.Set("GameName", gameName)
                                                      .Set("Publisher", publisher)
                                                      .Set("Genre", genre)
                                                      .Set("Description", descrioption)
                                                      .Set("GameCost", cost);

            collection.UpdateOne(filter2, update);

            MessageBox.Show("GamePage was updated.");
        }
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string search = Search_TextBox.Text;

                var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
                var collection = storeDB.GetCollection<BsonDocument>("GamePage");
                var filter = Builders<BsonDocument>.Filter.Eq("GameName", search);
                var filter2 = collection.Find(filter).FirstOrDefault();

                if (filter2 is not null) 
                {
                    GameName_TextBox_Copy.Text = filter2["GameName"].ToString();
                    PublisherName_TextBox_Copy.Text = filter2["Publisher"].ToString();
                    GenreName_TextBox_Copy.Text = filter2["Genre"].ToString();
                    Description_TextBox_Copy.Text = filter2["Description"].ToString();
                    Cost_TextBox_Copy.Text = filter2["GameCost"].ToString();

                    GameName_TextBox.Text = filter2["GameName"].ToString();
                    PublisherName_TextBox.Text = filter2["Publisher"].ToString();
                    GenreName_TextBox.Text = filter2["Genre"].ToString();
                    Description_TextBox.Text = filter2["Description"].ToString();
                    Cost_TextBox.Text = filter2["GameCost"].ToString();
                }
                else
                {
                    MessageBox.Show("Write game name again.");
                }
            }
            catch(Exception ex) { }
        }
    }
}
