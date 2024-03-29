using System;
using System.CodeDom;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Kortspil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// Kortene er hentet fra https://acbl.mybigcommerce.com/52-playing-cards/
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int kortnummer;
            string filnavn = "";

            try
            {
                kortnummer = Convert.ToInt32(Kort.Text);
            }
            catch(FormatException) 
            {
                kortnummer = 0;
            }
            
            try
            {
                filnavn = FindBillede(kortnummer);
            }catch(ArgumentException)
            {
                Kort.Text = "Invalid Selection";
                return;
            }
                
            string url = $"/Billeder/{filnavn}";
            Uri uri = new (url, UriKind.Relative);
            BitmapImage image = new(uri);
           
            Billede.Source = image;
            
        }

        private string FindBillede(int kortnummer)
        {

            // Skriv din løsning her...
            string kortVærdi;
            string kortType;
            int kortSet = 13;

            if (kortnummer < 1 || kortnummer > 52)
            {
                throw new ArgumentException("Værdien skal være mellem 1 og 52");
            }
            else if (kortnummer < 14)
            {
                kortType = "Spar";
            }
            else if (kortnummer >= 14 && kortnummer < 27)
            {
                kortType = "Ruder";
                kortnummer -= kortSet;
            }
            else if (kortnummer >= 27 && kortnummer < 40)
            {
                kortType = "Klør";
                kortnummer -= (2 * kortSet);
            }
            else {
                kortType = "Hjerter";
                kortnummer -= (3 * kortSet);
            }

            switch (kortnummer)
            {
                case 1:
                    kortVærdi = "Es";
                    break;
                case 11:
                    kortVærdi = "Knægt";
                    break;
                case 12:
                    kortVærdi = "Dame";
                    break;
                case 13:
                    kortVærdi = "Konge";
                    break;
                default:
                    kortVærdi = Convert.ToString(kortnummer);
                    break;
            }

            string resultat = $"{kortVærdi}-{kortType}.jpg";
            return resultat;
        }
    }
}
