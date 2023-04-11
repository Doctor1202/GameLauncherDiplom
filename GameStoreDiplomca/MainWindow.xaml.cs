using GameStoreDiplomca.Class;
using GameStoreDiplomca.Windows;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GameStoreDiplomca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         
        static MainWindow main = new MainWindow();
        static LoginWindow login = new LoginWindow();
        static DeleteWindow delete = new DeleteWindow();
        static LibraryWindow library = new LibraryWindow();
        static CreateWindow create = new CreateWindow();
        static ChangeWindow change = new ChangeWindow();

        static MongoClient dbClient = new MongoClient();
        static IMongoDatabase storeDb = dbClient.GetDatabase("StoreDB");
        static IMongoCollection<GamePage> collection = storeDb.GetCollection<GamePage>("GamePage");
        static IMongoCollection<User> collectionUser = storeDb.GetCollection<User>("User");


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
            change.Show();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            create.Show();
        }
        public void ReadAllDocument()
        {
            //Обновлення списку ігор
            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<GamePage>("GamePage");
            var filter = new BsonDocument();
            var dataGrid = collection.Find(filter).ToList();

            GameList_DataGid.DataContext = dataGrid;
            GameList_DataGid.ItemsSource = dataGrid;

        }
        public void ReadUserMonye()
        {
            //Обновлення грошей користувача
            var storeDB = DbConnect.dbClient.GetDatabase("StoreDB");
            var collection = storeDB.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq("UserName", User_Text.Text);
            var userMonye = collection.Find(filter).FirstOrDefault();

            MoneyCoint_Text.Text = userMonye.Money.ToString();
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            ReadAllDocument();
            ReadUserMonye();
        }

        private void GameList_DataGid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GamePage gp = (GamePage)GameList_DataGid.SelectedItem;

            if (gp == null)
            {
                
            }
            else {

                Game_Text.Text = gp.GameName;
                Publisher_Text.Text = gp.Publisher;
                Genre_Text.Text = gp.Genre;
                Description_Text.Text = gp.Description;
                MoneyCoint_Text1.Text = gp.GameCost.ToString();
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Library_Button_Click(object sender, RoutedEventArgs e)
        {
            library.Show();
        }

        private void Buy_Button_Click(object sender, RoutedEventArgs e)
        {
            //вивід грошей користувача
            var userName = User_Text.Text;
            var filter = Builders<User>.Filter.Eq("UserName", userName);
            var user = collectionUser.Find(filter).FirstOrDefault();

            //вивід ціни гри
            GamePage gp = (GamePage)GameList_DataGid.SelectedItem;


            int costOfGame = gp.GameCost;
            int uMonye = user.Money;

            if (uMonye < costOfGame)
            {
                MessageBox.Show("You can`t buy this product.\n" +
                                "Check your account balans");
            }
            else
            {




                MessageBox.Show("Thank you for purchase. \n" +
                                "Game was added to your library.");
            }
        }
    }
}
