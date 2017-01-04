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
        /// <summary>
        /// Initiation de la page Réglages
        /// </summary>
        public ReglagesWindow()
        {
            InitializeComponent(); // Initialisation
        }

        /// <summary>
        /// Paramètre du nombre de joueurs pour la partie (entre 2 et 5)
        /// </summary>
        public int NbJoueursPartie { get; set; }

        /// <summary>
        /// Paramètre du type de partie (solo ou multijoueur)
        /// </summary>
        public string TypePartie { get; set; }

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

        /// <summary>
        /// Action de lancement de la partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayClick(object sender, RoutedEventArgs e)
        {
            // Constructeur de partie avec paramètres Type de partie, Nombre de joueurs, Pseudo joueur Humain et limite de mouches
            Partie partie = new Partie(TypePartie, NbJoueursPartie, (Joueur1.Text), (int.Parse(ChoixNbMouches.Text))); 
            this.Hide(); // Disparition de la page actuelle
            JeuWindow Jeu = new JeuWindow(partie); // Création de la page Jeu
            Jeu.ShowDialog(); // Apparition de la page Jeu
        }        

        /// <summary>
        /// Detection du type de partie via RadioButtons pressé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonTypeChecked(object sender, RoutedEventArgs e)
        {
            var RadioButtonType = sender as RadioButton; // Récupération de l'objet RadioButtonType
            TypePartie = RadioButtonType.Content.ToString(); // Conversion du content du RadioButton pressé en string Type de partie

        }

        /// <summary>
        /// Detection du nombre de joueurs via RadioButtons pressé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonNbChecked(object sender, RoutedEventArgs e)
        {
            var RadioButtonNb = sender as RadioButton; // Récupération de l'objet RadioButtonNb
            NbJoueursPartie = int.Parse(RadioButtonNb.Content.ToString()); // Conversion du content du RadioButton pressé en int Nombre de joueurs

        }      

        /// <summary>
        /// Permettre uniquement des nombres dans la limite de mouches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); // Création de la limite d'expressions 
            e.Handled = regex.IsMatch(e.Text); // Application de cette limite à la propriété Text
        }
    }
}
