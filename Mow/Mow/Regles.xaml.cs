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
    /// Logique d'interaction pour Regles.xaml
    /// </summary>
    public partial class ReglesWindow : Window
    {
        /// <summary>
        /// Initiation de la page Règles
        /// </summary>
        public ReglesWindow()
        {
            InitializeComponent(); // Initialisation
        }

        /// <summary>
        /// Popup Carte Règles 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regles1Popup_Click(object sender, RoutedEventArgs e)
        {
            Regles1Popup.IsOpen = true; // Apparition du popup détaillant
        }

        /// <summary>
        /// Popup Carte Règles 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regles2Popup_Click(object sender, RoutedEventArgs e)
        {
            Regles2Popup.IsOpen = true; // Apparition du popup détaillant
        }

        /// <summary>
        /// Popup Carte Règles 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regles3Popup_Click(object sender, RoutedEventArgs e)
        {
            Regles3Popup.IsOpen = true; // Apparition du popup détaillant
        }

        /// <summary>
        /// Action de retour au menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Disparition de la page actuelle
            MenuWindow Menu = new MenuWindow(); // Création de la page Menu
            Menu.ShowDialog(); // Apparition de la page Menu
        }
    }
}
