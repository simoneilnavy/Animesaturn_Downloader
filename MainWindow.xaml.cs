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
using HtmlAgilityPack;
using System.Net;
using System.Diagnostics;

namespace AnimeSaturn_Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string url = "";
        string final_link = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        void start()
        {
            string baselink1 = "";
            string baselink2 = "";
            string strStart = "\"";
            string strEnd = "\",";
            string source = GetSource(url);
            if (source == "")
            {
                return;
            }
            HtmlAgilityPack.HtmlDocument documento = new HtmlAgilityPack.HtmlDocument();

            documento.LoadHtml(source);
            HtmlNodeCollection nodes = documento.DocumentNode.SelectNodes("/html/body/center/div[2]/div/div/div/div/div/div/script[2]/text()");
            final_link = nodes[0].InnerText;
            final_link = getBetween(final_link, strStart, strEnd);
            baselink1 = getBetween(final_link, "h", "ANIME/");
            baselink1 = 'h' + baselink1 + "ANIME/";
            baselink2 = getBetween(final_link, baselink1, "/");
            final_link = "/C python C:\\Users\\pino2\\OneDrive\\Desktop\\ANIMESATURN\\downloader.py " + baselink1 + baselink2;
            System.Diagnostics.Process.Start("CMD.exe",final_link);
            return;
        }
        static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        string GetSource(string url)
        {
            try
            {
                WebClient mywebClient = new WebClient();
                byte[] mydatabuffer = mywebClient.DownloadData(url);
                string source = Encoding.ASCII.GetString(mydatabuffer);
                return source;
            }
            catch (Exception ex)
            {
                return "";
            }


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            url = linkpage.Text;
            start();
        }
    }
}
