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
            try
            {
                MainWindow main = new MainWindow();

                string logInBox = LogIn_Box.Text;
                string passWordBox = PassWord_Box.Password;

                var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
                var collection = storeDB.GetCollection<User>("User");
                var searchFilter = Builders<User>.Filter.Eq("Login", logInBox);
                var logInUser = collection.Find(searchFilter).FirstOrDefault();

                if (logInUser is not null && logInUser.Password == passWordBox)
                {
                    MessageBox.Show("+");
                    main.Show();
                    Close();

                    main.User_Text.Text = logInUser.UserName;
                    main.MoneyCoint_Text.Text = logInUser.Money.ToString();

                    if (logInUser.IsAdmin == false)
                    {
                        main.Change_Button.Visibility = Visibility.Hidden;
                        main.Create_Button.Visibility = Visibility.Hidden;
                        main.Delete_Button.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        main.Change_Button.Visibility = Visibility.Visible;
                        main.Create_Button.Visibility = Visibility.Visible;
                        main.Delete_Button.Visibility = Visibility.Visible;
                    }
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
