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
            var logInUser = collection.Find(_ => true);



            if (serchFilter == logInUser)
            {
                MessageBox.Show("+");
            }
            else
            {
                MessageBox.Show("-");
            }

        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            string logInBox = LogIn_Box.Text;
            string passWordBox = PassWord_Box.Password;

            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<BsonDocument>("User");
            var filter = Builders<BsonDocument>.Filter.Text("string"); ;
            var filter2 = collection.Find(filter).FirstOrDefault();

            try
            {
            var logInUser = new BsonDocument
            {
                {
                    "scores",
                    new BsonArray
                    {
                        new BsonDocument { { "type", "login" }, { "string", logInBox } },
                        new BsonDocument { { "type", "password" }, { "string", passWordBox } }
                    }
                }
            };
                foreach () 
                {
                }

                if (logInUser == filter2)
                {
                    MessageBox.Show("Can`t creat two same acaunts!");
                }
                else
                {
                    collection.InsertOne(logInUser);
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
