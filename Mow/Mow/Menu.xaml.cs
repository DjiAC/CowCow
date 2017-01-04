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
    public partial class MenuWindow : Window
    {
        /// <summary>
        /// Initiation de la page Menu
        /// </summary>
        public MenuWindow()
        {
            InitializeComponent(); // Initialisation
        }

        /// <summary>
        /// Choix vers le jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JouerClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Disparition de la page actuelle
            ReglagesWindow Reglages = new ReglagesWindow(); // Création de la page Reglages
            Reglages.ShowDialog(); // Apparition de la page Reglages
        }

        /// <summary>
        /// Choix vers les statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatsClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Disparition de la page actuelle
            StatsWindow Stats = new StatsWindow(); // Création de la page Statistiques
            Stats.ShowDialog(); // Apparition de la page Statistiques
        }

        /// <summary>
        /// Choix vers les règles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReglesClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Disparition de la page actuelle
            ReglesWindow Regles = new ReglesWindow(); // Création de la page Règles
            Regles.ShowDialog(); // Apparition de la page Règles
        }

        /// <summary>
        /// Choix vers les crédits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreditsClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Disparition de la page actuelle
            CreditsWindow Credits = new CreditsWindow(); // Création de la page Crédits
            Credits.ShowDialog(); // Apparition de la page Crédits
        }
    }
}
