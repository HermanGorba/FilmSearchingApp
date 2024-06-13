using System.Net.Http;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace HTTP_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.omdbapi.com/?i=tt3896198&apikey=78aaf11c" + "&t=" + filmTitleTextBox.Text + "&y" + releaseYearTextBox.Text;

            using (HttpClient client = new())
            {
                string jsonString = await client.GetStringAsync(url);
                JObject jObject = JObject.Parse(jsonString);

                if (jObject == null)
                { 
                    responseListView.Items.Add("Movie not found.");
                    return;
                }

                Movie movie = new Movie
                {
                    Title = jObject.GetValue("Title").ToString(),
                    Poster = jObject.GetValue("Poster").ToString(),
                    Year = jObject.GetValue("Year").ToString(),
                    Released = jObject.GetValue("Released").ToString(),
                    Plot = jObject.GetValue("Plot").ToString(),
                    Country = jObject.GetValue("Country").ToString(),
                    Genre = jObject.GetValue("Genre").ToString()
                };

                responseListView.ItemsSource = new[] { movie };
                
            }

        }


        public class Movie
        {
            public string Title { get; set; }
            public string Poster { get; set; }
            public string Year { get; set; }
            public string Released { get; set; }
            public string Plot { get; set; }
            public string Country { get; set; }
            public string Genre { get; set; }
        }
    }
}
