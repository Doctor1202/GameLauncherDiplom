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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameStoreDiplomca.Class;
using GameStoreDiplomca.Windows;
using MongoDB.Driver;


namespace GameStoreDiplomca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow change = new ChangeWindow();
            change.Show();
        }
    }
}
