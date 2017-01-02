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

namespace Mow
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class ReglagesWindow : Window
    {
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
            this.Hide();
            JeuWindow Jeu = new JeuWindow();
            Jeu.ShowDialog();
        }

        private void SoloClick(object sender, RoutedEventArgs e)
        {
            string TypePartie = "solo";
        }

        private void MultiClick(object sender, RoutedEventArgs e)
        {
            string TypePartie = "multi";
        }

        private void Nb2Click(object sender, RoutedEventArgs e)
        {
            int NbJoueurs = 2;
        }

        private void Nb3Click(object sender, RoutedEventArgs e)
        {
            int NbJoueurs = 3;
        }

        private void Nb4Click(object sender, RoutedEventArgs e)
        {
            int NbJoueurs = 4;
        }

        private void Nb5Click(object sender, RoutedEventArgs e)
        {
            int NbJoueurs = 5;
        }
    }
}
