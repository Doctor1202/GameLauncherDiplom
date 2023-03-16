using GameStoreDiplomca.Class;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Windows;

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
            string logInBox = LogIn_Box.Text;
            string passWordBox = PassWord_Box.Password;

            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<BsonDocument>("User");
            var serchFilter = Builders<BsonDocument>.Filter.Eq("Login", logInBox);
            var logInUser = collection.Find(serchFilter);




            /*if (logInUser) 
            {
                MessageBox.Show("+");
            }
            else
            {
                MessageBox.Show("-");

            }*/

            MainWindow main = new MainWindow();
            main.Show();

        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            string logInBox = LogIn_Box.Text;
            string passWordBox = PassWord_Box.Password;

            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<BsonDocument>("User");
            var filter = Builders<BsonDocument>.Filter.Text("scores"); ;
            var filter2 = collection.Find(filter).FirstOrDefault();

            try
            {
            var logInUser = new BsonDocument
            {
            new BsonDocument { { "type", "login" }, { "string", logInBox } },
            new BsonDocument { { "type", "password" }, { "string", passWordBox } }
            };

                if (logInUser == filter2)
                {
                    MessageBox.Show("Can`t creat two same acaunts!");
                }
                else
                {
                    collection.InsertOneAsync(logInUser);
                    MessageBox.Show("All alright");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);

            }
        }
    }
}
