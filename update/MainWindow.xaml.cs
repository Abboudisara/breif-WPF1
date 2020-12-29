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
using System.Diagnostics;
using System.Net;
using System.IO.Compression;
using System.IO;

namespace update
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void oui_Click(object sender, RoutedEventArgs e)
        {

            WebClient webClient = new WebClient();
            try
            {


                //System.Threading.Thread.Sleep(5000);
                webClient.DownloadFile("https://sendeyo.com/show/99d5a21892", @"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\bin\Release\netcoreapp3.1\verssion2.zip");
                string zipPath = @"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\bin\Release\netcoreapp3.1\verssion2.zip";
                string extractPath = @"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\bin\Release";
                ZipFile.ExtractToDirectory(zipPath, extractPath);
               // File.Delete(zipPath);

                Process.Start(@"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\bin\Release\CLENER.exe");
                this.Close();

            }
            catch
            {
                Process.Start(@"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\bin\Release\netcoreapp3.1\CLENER.exe");
                this.Close();

            }



        }

}
    }

