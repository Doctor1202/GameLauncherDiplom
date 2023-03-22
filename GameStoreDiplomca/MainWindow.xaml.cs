using GameStoreDiplomca.Class;
using GameStoreDiplomca.Windows;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Windows;




namespace GameStoreDiplomca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DeleteWindow delete = new DeleteWindow();
        static MainWindow main = new MainWindow();
        static LoginWindow login = new LoginWindow();
        public MainWindow()
        {
            InitializeComponent();
            DbConnect.ConnectionToDb();
            ReadAllDocument();
        }
        public void SelectedName()
        {
            delete.GameName_Text.Text = Game_Text.Text;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedName();
            delete.Show();
        }

        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow change = new ChangeWindow();
            change.Show();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow create = new CreateWindow();
            create.Show();
        }
        public void ReadAllDocument()
        {
            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<GamePage>("GamePage");
            var filter = new BsonDocument();

            GameList_DataGid.DataContext = collection.Find(filter).ToList();
            GameList_DataGid.ItemsSource = collection.Find(filter).ToList();
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            ReadAllDocument();
        }

        private void GameList_DataGid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<GamePage>("GamePage");

            GamePage gp = (GamePage)GameList_DataGid.SelectedItem;
            
            Game_Text.Text = gp.GameName;
            Publisher_Text.Text = gp.Publisher;
            Genre_Text.Text = gp.Genre;
            Description_Text.Text = gp.Description;
            MoneyCoint_Text1.Text = gp.GameCost;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
