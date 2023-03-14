using GameStoreDiplomca.Class;
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
using MongoDB.Driver;
using MongoDB.Bson;

namespace GameStoreDiplomca.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<BsonDocument>("User");
            var logInUser = collection.Find(_=>true);

            string logInBox = LogIn_Box.Text;
            string passWordBox = PassWord_Box.Password;

            var serchFilter = Builders<BsonDocument>.Filter.Eq("Login", logInBox);

            if (serchFilter is not null)
            {
                MessageBox.Show("+");
            }
            else
            {
                MessageBox.Show("-");
            }

        }
    }
}
