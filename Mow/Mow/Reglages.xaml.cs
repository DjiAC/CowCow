using Mow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Mow
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class ReglagesWindow : Window
    {
        public string TypePartie { get; set; }

        public int NbJoueursPartie { get; set; }

        public ReglagesWindow()
        {
            InitializeComponent();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuWindow Menu = new MenuWindow();
            Menu.ShowDialog();
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {
            Partie partie = new Partie(TypePartie , NbJoueursPartie , (Joueur1.Text), (int.Parse(ChoixNbMouches.Text)));
            partie.JouerPartie();
            this.Hide();
            JeuWindow Jeu = new JeuWindow(partie);
            Jeu.ShowDialog();
        }        

        private void RadioTypeSoloChecked(object sender, RoutedEventArgs e)
        {
            string TypePartie = "solo";
        }

        private void RadioTypeMultiChecked(object sender, RoutedEventArgs e)
        {
            string TypePartie = "multi";
        }

        private void Nb2Checked(object sender, RoutedEventArgs e)
        {
            int NbJoueursPartie = 2;
        }

        private void Nb3Checked(object sender, RoutedEventArgs e)
        {
            int NbJoueursPartie = 3;
        }

        private void Nb4Checked(object sender, RoutedEventArgs e)
        {
            int NbJoueursPartie = 4;
        }

        private void Nb5Checked(object sender, RoutedEventArgs e)
        {
            int NbJoueursPartie = 5;
        }

        private void OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
