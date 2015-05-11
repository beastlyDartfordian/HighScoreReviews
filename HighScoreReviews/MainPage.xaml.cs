using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;

namespace HighScoreReviews
{
    public partial class MainPage : PhoneApplicationPage
    {

        public class ArticleItem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string ImgSrc { get; set; }
            public string ArticleURL { get; set; }
        }        
        
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            WebClient Articles = new WebClient();

            Articles.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Articles_DownloadStringCompleted);

            Articles.DownloadStringAsync(new Uri("https://public-api.wordpress.com/rest/v1/sites/hiscre.wordpress.com/posts/?number=20"));
        }

        void Articles_DownloadStringCompleted(object senders, DownloadStringCompletedEventArgs e)
        {
            try
            {
                List<ArticleItem> contentList = new List<ArticleItem>();
                JObject ArtObject = JObject.Parse(e.Result);

                JArray ArtArray = (JArray)ArtObject["posts"];
                int count = 0;
                
                while (count < ArtArray.Count)
                {
                    ArticleItem article = new ArticleItem();
                    article.ImgSrc = ArtArray[count]["featured_image"].ToString();
                    article.Title = ArtArray[count]["title"].ToString();
                    article.Author = ArtArray[count]["author"]["name"].ToString();
                    article.ArticleURL = ArtArray[count]["URL"].ToString();
                    contentList.Add(article);
                    count++;
                }

                featured.ItemsSource = contentList.ToList();

            }
            catch (Exception error)
            {
                MessageBox.Show("A network error has occured");
                Console.WriteLine("An error occured: " + error);
            }
        }



        private void NavArticles_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void NavTwitter_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Twitter.xaml", UriKind.Relative));
        }

        private void NavHelp_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }


        private void featured_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void featured_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selected = featured.SelectedValue as ArticleItem;
            var ArticleURL = selected.ArticleURL;

            //NavigationService.Navigate(new Uri("/Article.xaml?ArticleID=" + ArticleID, UriKind.Relative));

            //MessageBox.Show(ArticleURL);
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new Uri (ArticleURL);
            task.Show();
        }
    }
}