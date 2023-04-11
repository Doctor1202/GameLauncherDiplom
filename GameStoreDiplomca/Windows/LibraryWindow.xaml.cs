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
            var collection = storeDB.GetCollection<ULibrary>("ULibrary");
            var filter = new BsonDocument();

            var dataGrid = collection.Find(filter).ToList();

            GameList_DataGid.DataContext = dataGrid;
            GameList_DataGid.ItemsSource = dataGrid;

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

        }
    }
}
