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
    /// Логика взаимодействия для Registration_Window.xaml
    /// </summary>
    public partial class Registration_Window : Window
    {
        public Registration_Window()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Registration_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = UserName_TextBox.Text;
                string logInBox = Login_TextBox.Text;
                string passWordBox = Password_TextBox.Password;

                var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
                var collection = storeDB.GetCollection<BsonDocument>("User");
                var filter = Builders<BsonDocument>.Filter.Eq("Login", logInBox);
                var filter2 = collection.Find(filter).FirstOrDefault();

                var logInUser = new BsonDocument
                {
                    {"UserName", userName },
                    { "Login",  logInBox },
                    {"Password", passWordBox },
                    {"Money", 0 },
                    {"IsAdmin", false }
                };

                if (filter2 is not null)
                {
                    MessageBox.Show("Can`t create two same accounts!");
                }
                else
                {
                    collection.InsertOne(logInUser);
                    MessageBox.Show("All alright");
                    Close();
                }


            }
            catch(Exception ex) { MessageBox.Show("Error" + ex); }

        }
    }
}
