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
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        static DeleteWindow delete = new DeleteWindow();
        static MainWindow main = new MainWindow();

        static MongoClient dbClient = new MongoClient();
        static IMongoDatabase storeDb = dbClient.GetDatabase("StoreDB");
        static IMongoCollection<BsonDocument> collection = storeDb.GetCollection<BsonDocument>("GamePage");

        public DeleteWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();

        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            var gameName = GameName_Text.Text;
            var filter = Builders<BsonDocument>.Filter.Eq("GameName", gameName);

            collection.DeleteOne(filter);
            MessageBox.Show("Object was deleted.");
            Close();
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
