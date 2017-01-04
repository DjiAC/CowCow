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
    public partial class StatsWindow : Window
    {
        /// <summary>
        /// Initiation de la page Statistiques
        /// </summary>
        public StatsWindow()
        {
            InitializeComponent(); // Initialisation
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
