using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using miniTumblr.Resources;
using System.Xml.Linq;

namespace miniTumblr
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            WebClient tumblr = new WebClient();

            tumblr.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tumblr_DownloadStringCompleted);
            tumblr.DownloadStringAsync(new Uri(String.Format("http://{0}.tumblr.com/rss", tumblrName.Text)));
        }

        private void tumblr_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            XElement xmlTumblr = XElement.Parse(e.Result);
            List<TumblrItem> TumblrItems = new List<TumblrItem>();

            // First foreach, no linq for now.
            foreach (var item in xmlTumblr.Descendants("item"))
            {
                TumblrItem tmp = new TumblrItem();
                tmp.Title = item.Element("title").Value.ToString();
                tmp.ImageSource = ExtractImage(item.Element("description").Value.ToString());
                TumblrItems.Add(tmp);
            }

            Lili.ItemsSource = TumblrItems;
        }

        private string ExtractImage(string desc)
        {
            return ExtracterHelper.ExtractString(desc, "img src=\"", "\"/>");
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}