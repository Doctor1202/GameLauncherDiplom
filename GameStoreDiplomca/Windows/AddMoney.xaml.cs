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
    /// Логика взаимодействия для AddMoney.xaml
    /// </summary>
    public partial class AddMoney : Window
    {
        public AddMoney()
        {
            InitializeComponent();
        }

        private void AddMoney_Button_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserName_Text.Text;
            var money = AddMoney_TextBox.Text;
            var uMoney = Money_Text.Text;

            int.TryParse(money, out int moneyInt);
            int.TryParse(uMoney, out int uMoneyInt);
            
            moneyInt += uMoneyInt;

            var storeDb = DbConnect.dbClient.GetDatabase("StoreDB");
            var collectionUser = storeDb.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("UserName", userName);
            var user = collectionUser.Find(filter).FirstOrDefault();
            var update = Builders<User>.Update.Set("Money", moneyInt);

            collectionUser.UpdateOne(filter, update);
            MessageBox.Show("Money was added to your account.");
        }
    }
}
