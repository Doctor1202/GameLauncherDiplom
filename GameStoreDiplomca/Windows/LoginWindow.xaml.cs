using GameStoreDiplomca.Class;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
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
            var searchFilter = Builders<BsonDocument>.Filter.Eq("Login", logInBox) |
                               Builders<BsonDocument>.Filter.Eq("Password", passWordBox);
            var logInUser = collection.Find(searchFilter).SingleOrDefault();
            var logInUser2 = collection.Find(searchFilter).FirstOrDefault();

            if (logInUser == logInUser2 && logInUser is not null)
            {
                MessageBox.Show("+");

                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                MessageBox.Show("-");
            }


        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string logInBox = LogIn_Box.Text;
                string passWordBox = PassWord_Box.Password;

                var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
                var collection = storeDB.GetCollection<BsonDocument>("User");
                var filter = Builders<BsonDocument>.Filter.Eq("Login", logInBox) |
                             Builders<BsonDocument>.Filter.Eq("Password", passWordBox);
                var filter2 = collection.Find(filter).SingleOrDefault();

                var logInUser = new BsonDocument
                {
                    { "Login",  logInBox },
                    {"Password", passWordBox }
                };

                if (filter2["Login"] == logInUser["Login"] && filter2["Password"] == logInUser["Password"])
                {
                    MessageBox.Show("Can`t create two same accounts!");
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
