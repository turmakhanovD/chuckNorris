using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace NorrisApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string url = @"https://api.chucknorris.io/jokes/random";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetJoke(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "GET";

                Task.Run(() =>
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        Joke joke = JsonConvert.DeserializeObject<Joke>(result);

                        MessageBox.Show(joke.Value, "Смешная шутка!");
                    }
                });
            }
            else
            {
                ComboBoxItem typeItem = (ComboBoxItem)comboBox.SelectedItem;
                string value = typeItem.Content.ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + $"?category={value}");
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "GET";

                Task.Run(() =>
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        Joke joke = JsonConvert.DeserializeObject<Joke>(result);

                        MessageBox.Show(joke.Value, "Смешная шутка!");
                    }
                });
            }
        }
    }
}
