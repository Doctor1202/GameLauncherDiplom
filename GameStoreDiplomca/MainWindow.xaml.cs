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
         
        static MainWindow main = new MainWindow();
        static LoginWindow login = new LoginWindow();

        static MongoClient dbClient = new MongoClient();
        static IMongoDatabase? storeDb = dbClient.GetDatabase("StoreDB");
        static IMongoCollection<GamePage>? collection = storeDb.GetCollection<GamePage>("GamePage");
        static IMongoCollection<User>? collectionUser = storeDb.GetCollection<User>("User");


        public MainWindow()
        {

            InitializeComponent();
            DbConnect.ConnectionToDb();
            ReadAllDocument();
        }
       
        public void SelectedName()
        {
            DeleteWindow delete = new DeleteWindow();
            delete.GameName_Text.Text = Game_Text.Text;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow delete = new DeleteWindow();
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

            var dataGrid = collection.Find(filter).ToList();

            GameList_DataGid.DataContext = dataGrid;
            GameList_DataGid.ItemsSource = dataGrid;
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            ReadAllDocument();
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
                MoneyCoint_Text1.Text = gp.GameCost;
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Library_Button_Click(object sender, RoutedEventArgs e)
        {
            LibraryWindow library = new LibraryWindow();
            library.Show();
        }
    }
}
