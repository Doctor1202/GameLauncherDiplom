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
        static MainWindow main = new MainWindow();
        static MongoClient dbClient = new MongoClient();
        static IMongoDatabase storeDb = dbClient.GetDatabase("StoreDB");
        static IMongoCollection<BsonDocument> collection = storeDb.GetCollection<BsonDocument>("User");
        static IMongoCollection<User>? collectionUser = storeDb.GetCollection<User>("User");

        public LoginWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
        }

        public void UserName()
        {
            string logInBox = LogIn_Box.Text;

            var searchFilter = Builders<User>.Filter.Eq("Login", logInBox);
            var userInfo = collectionUser.Find(searchFilter).FirstOrDefault();

            main.User_Text.Text = userInfo.UserName;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string logInBox = LogIn_Box.Text;
                string passWordBox = PassWord_Box.Password;

                var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
                var collection = storeDB.GetCollection<BsonDocument>("User");
                var searchFilter = Builders<BsonDocument>.Filter.Eq("Login", logInBox);
                var logInUser = collection.Find(searchFilter).FirstOrDefault();

                if (logInUser is not null && logInUser["Password"] == passWordBox)
                {
                    MessageBox.Show("+");
                    main.Show();
                    Close();
                    UserName();
                }
                else
                {
                    MessageBox.Show("-");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error" + ex); }


        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            Registration_Window reg = new Registration_Window();
            reg.Show();
        }
    }
}
