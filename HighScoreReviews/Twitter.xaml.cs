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
    public partial class Twitter : PhoneApplicationPage
    {
        
        public class TwitterItem
        {
            public string UserName {get; set;}
            public string Message {get; set;}
            public string ImageSource {get; set;}
            public string TweetID {get; set;}
        }
        
        public Twitter()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WebClient Twitter = new WebClient();
            
            Twitter.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Twitter_DownloadStringCompleted);

            // Twitter.DownloadStringAsync(new Uri("http://derekfoster.cloudapp.net/tweet/userjson.php?c=20&user=HighScoreReview"));

             Twitter.DownloadStringAsync(new Uri("http://derekfoster.cloudapp.net/tweet2/userjson.php?c=20&user=HighScoreReview"));

            // Twitter.DownloadStringAsync(new Uri("http://derekfoster.cloudapp.net/tweet3/userjson.php?c=20&user=HighScoreReview"));
        }

        void Twitter_DownloadStringCompleted(object senders, DownloadStringCompletedEventArgs e)
        {
            try
            {
                List<TwitterItem> contentList = new List<TwitterItem>();
                JArray Twit = JArray.Parse(e.Result);
                int count = 0;

                while (count < Twit.Count)
                {
                    TwitterItem tweet = new TwitterItem();
                    tweet.ImageSource = Twit[count]["user"]["profile_image_url"].ToString();
                    tweet.UserName = Twit[count]["user"]["screen_name"].ToString();
                    tweet.Message = Twit[count]["text"].ToString();
                    tweet.TweetID = Twit[count]["id_str"].ToString();
                    contentList.Add(tweet);
                    count++;
                }
                tweets.ItemsSource = contentList.ToList();

            }
            catch (Exception error)
            {                
                 MessageBox.Show("A network error has occured");
                 Console.WriteLine("An error occured: " + error);
            }

        }
        
        private void tweets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tweets_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selected = tweets.SelectedValue as TwitterItem;
            var SpecificTweet = selected.TweetID;

            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new Uri("https://twitter.com/HighScoreReview/status/" + SpecificTweet);
            task.Show();

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

        private void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new Uri("https://twitter.com/intent/follow?original_referer=http%3A%2F%2Fhiscre.wordpress.com%2F&screen_name=HighScoreReview&tw_p=followbutton&variant=2.0");
            task.Show();
        }
    }
}