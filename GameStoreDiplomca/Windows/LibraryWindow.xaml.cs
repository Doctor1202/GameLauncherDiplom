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
    /// Логика взаимодействия для LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        public LibraryWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
            LReadAllDocument();
        }

        public void LReadAllDocument()
        {
            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collectionUL = storeDB.GetCollection<ULibrary>("ULibrary");
            var collection = storeDB.GetCollection<User>("User");

            var userName = UserName_Text.Text;
            var filter = Builders<User>.Filter.Eq("UserName", userName);
            //var filter = new BsonDocument();

            var dataGrid = collection.Find(filter).FirstOrDefault();
            var dataGrid2 = dataGrid.ULibrary.ToList();

            GameList_DataGid.DataContext = dataGrid2;
            GameList_DataGid.ItemsSource = dataGrid2;
        }

        private void DataGridTextColumn_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            LReadAllDocument();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your game was started");
        }

        private void Download_Button_Click(object sender, RoutedEventArgs e)
        {
            ULibrary lg = (ULibrary)GameList_DataGid.SelectedItem;
            var userName = UserName_Text.Text;

            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("UserName", userName) &
                         Builders<User>.Filter.Eq("ULibrary.GameName", lg.GameName);
            var game = collection.Find(filter).FirstOrDefault();

            var update = Builders<User>.Update.Set("ULibrary.$.IsDownLoad", true);

            collection.UpdateOne(filter, update);

            MessageBox.Show("Game was downloaded to your computer.");
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            ULibrary lg = (ULibrary)GameList_DataGid.SelectedItem;
            var userName = UserName_Text.Text;

            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("UserName", userName) &  
                         Builders<User>.Filter.Eq("ULibrary.GameName", lg.GameName);
            var game = collection.Find(filter).FirstOrDefault();

            var update = Builders<User>.Update.Set("ULibrary.$.IsDownLoad", false);

            collection.UpdateOne(filter, update);

            MessageBox.Show("Game was delete from your computer.");
        }

        private void GameList_DataGid_MouseUp(object sender, MouseButtonEventArgs e)
        {

            ULibrary lg = (ULibrary)GameList_DataGid.SelectedItem;

            GameName_TextBox.Text = lg.GameName;

            if (lg.IsDownloud == false)
            {
                Download_Button.IsEnabled = true;
                Play_Button.IsEnabled = false;
                Delete_Button.IsEnabled = false;
            }
            else
            {
                Download_Button.IsEnabled = false;
                Play_Button.IsEnabled = true;
                Delete_Button.IsEnabled = true;
            }
        }



        private void LibraryWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            LReadAllDocument();
        }
    }
}
