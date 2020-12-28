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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.ComponentModel;

namespace CLENER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        readonly double clientversion = 1.0;
        string updatePath;
        string packageFile;
        WebClient webClient;
        public MainWindow()
        {
         InitializeComponent();
           loadprogressbar();
          



            liste.Visibility = Visibility.Hidden;
            quitte.Visibility = Visibility.Hidden;
            time.Visibility = Visibility.Hidden;
            dat.Visibility = Visibility.Hidden;
            taille.Visibility = Visibility.Hidden;
            menu2.Visibility = Visibility.Hidden;
            Annul.Visibility = Visibility.Hidden;
        }
        public void checkupdate()
        {

            webClient = new WebClient();
            Uri webVersion = new Uri("https://pastebin.com/6ik2xT1A");
            webClient.DownloadStringAsync(webVersion);
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;


        }

        public void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string strWebversion = e.Result;
             double webVersion = double.Parse(strWebversion);

            if (clientversion < webVersion)
            {
                MessageBox.Show("une nouvelle vaerssion doit étre télécharger");
                updatePath = System.IO.Path.Combine(@"update\", strWebversion);
                Uri package = new Uri("https://pastebin.com/edit/qTAt8w7f");
                Directory.CreateDirectory(updatePath);
                webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                webClient.DownloadFileAsync(package, packageFile);

            }
            
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Process.Start(packageFile);
            Application.Current.Shutdown();
        }


        // *********************Téléchargement dans progressbar
        private void loadprogressbar()

        {
            pb.Visibility = Visibility.Hidden;
            Duration dur = new Duration(TimeSpan.FromSeconds(30));
            DoubleAnimation dblani = new DoubleAnimation(200.0, dur);
            pb.BeginAnimation(ProgressBar.ValueProperty, dblani);
            


        }

        //***************** vibiliter dans b.annuller

        private void btn_anal1_Click_1(object sender, RoutedEventArgs e)
        {
            head.Text = "Analyse en cours";
            titre1.Visibility = Visibility.Hidden;
            titre2.Visibility = Visibility.Hidden;
            titre3.Visibility = Visibility.Hidden;
            rep1.Visibility = Visibility.Hidden;

            rep2.Visibility = Visibility.Hidden;
            rep3.Visibility = Visibility.Hidden;
            pb.Visibility = Visibility.Visible;
           
            Annul.Visibility = Visibility.Visible;


            btn_v.IsEnabled = false;
            btn_a.IsEnabled = false;
            btn_h.IsEnabled = false;
            btn_o.IsEnabled = false;
            btn_s.IsEnabled = false;



        }

        // ******************* Affichage date temps
        public void AfficheDtA()
        {
            // **************Affichage de date:
            rep3.Text = DateTime.Now.ToLongDateString();
            // **************Affichage de date:
            rep2.Text = DateTime.Now.ToLongTimeString();

        }


        public long size = 0;

        
        // Analyse et taille de repo
        private void Scanner()
        {
            List<string> listFiles = new List<string>();

            var tmpPath = System.IO.Path.GetTempPath();
            var files = Directory.GetFiles(tmpPath, ".", SearchOption.AllDirectories);
            listFiles.AddRange(files);
            var Nombre = listFiles.Count();
            

            FileStream fs = new FileStream(@"C:\Users\youcode\source\repos\story.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var file in files)
            {

                size += file.Length;
                sw.Write(file + "\n");
            }
           rep1.Text = "Espace à nettoyer : " + size / 1000 + " Ko (" + Nombre + " files)";



           
        }


        


        private void Annuler_Click(object sender, RoutedEventArgs e)

        {
            Annul.Visibility = Visibility.Visible;
            pb.Visibility = Visibility.Hidden;

            head.Text = "Analyse terminer";
            titre1.Visibility = Visibility.Visible;
            titre2.Visibility = Visibility.Visible;
            titre3.Visibility = Visibility.Visible;
            rep1.Visibility = Visibility.Visible;
            rep2.Visibility = Visibility.Visible;
            rep3.Visibility = Visibility.Visible;
            AfficheDtA();
            Scanner();
            btn_v.IsEnabled = true;
            btn_a.IsEnabled = true;
            btn_h.IsEnabled = true;
            btn_o.IsEnabled = true;
            btn_s.IsEnabled = true;
            MessageBox.Show("Annalise Terminer");
            regHisto();  
        }


        public void regHisto() 
        {
            // ****************creation fichiers :
            string fichier = @"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\fichierHistorique.txt";

            List<string> listFiles = new List<string>();
            {

                    List<string> listF = File.ReadAllLines(fichier).ToList();
                    // ****************division des ligne de fichiers
                    listF.Add("   " + size + "                                        " + DateTime.Now.ToLongDateString() + "                              " + DateTime.Now.ToLongTimeString());
                    File.WriteAllLines(fichier, listF, Encoding.Unicode);
                    File.ReadAllLines(fichier);

            }
        }

      



    // ******************Historique 
    private void historique_Click(object sender, RoutedEventArgs e)
        {
            liste.Visibility = Visibility.Visible;
            quitte.Visibility = Visibility.Visible;
            dat.Visibility = Visibility.Visible;
            time.Visibility = Visibility.Visible;
            menu2.Visibility = Visibility.Visible;
            taille.Visibility = Visibility.Visible;

            // lire le contenenu de fichier
              StreamReader sr = new StreamReader(@"C:\Users\Youcode\Desktop\breif-WPF1\CLENER\fichierHistorique.txt");
             liste.Text = sr.ReadToEnd();  
            
        }


        //***********************************Nettoyage
        private void netyoer(object sender, RoutedEventArgs e)
        {

            netoyage();
        }

        // suppression des fichiers 
         private static void netoyage()
        {
            string[] files = Directory.GetFiles(@"C:\Users\Youcode\Desktop\test");
            foreach(string file in files)
            {
                File.Delete(file);

            }

            MessageBox.Show("les repo bien nettoyer");
        }

        private void mettreajour_Click(object sender, RoutedEventArgs e)
        {

            checkupdate();
        }

        private void liste_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void quitte_Click(object sender, RoutedEventArgs e)
        {
            liste.Visibility = Visibility.Hidden;
            time.Visibility = Visibility.Hidden;
            dat.Visibility = Visibility.Hidden;
            menu2.Visibility = Visibility.Hidden;
            taille.Visibility = Visibility.Hidden;
            quitte.Visibility = Visibility.Hidden;
            Annul.Visibility = Visibility.Hidden;
        }

    }
}
       
    

