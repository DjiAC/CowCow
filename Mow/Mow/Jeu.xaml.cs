using System;
using Mow.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Mow
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuWindow : Window
    {
         
               
        /// <summary>
        /// Initiation de la page Jeu
        /// </summary>
        /// <param name="partie">Envoi de l'objet Partie</param>
        public JeuWindow(Partie partie)
        {
            InitializeComponent(); // Initialisation

            this.partie = partie; // Déclaration contexte issu de partie

            this.DataContext = partie; // Déclaration DataContext pour binding
        }

        /// <summary>
        /// Récupération de l'instance Partie partie
        /// </summary>
        Partie partie;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

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
            
        /// <summary>
        /// Action de lancement du jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LancerClick(object sender, RoutedEventArgs e)
        {
            LancerPartie.Visibility = Visibility.Collapsed; // Disparition du bouton - Ne fonctionne pas
            partie.Test(); // Lancement de la partie
        }

        private void Carte0Click(object sender, RoutedEventArgs e)
        {
            if (partie.Joueurs.ElementAt(0).Main.ElementAt(0).TypeDeCarte == "VacheNormale")
            {

            }
            else
            {

            }
        }

        private void Carte1Click(object sender, RoutedEventArgs e)
        {
            if (partie.Joueurs.ElementAt(0).Main.ElementAt(1).TypeDeCarte == "VacheNormale")
            {

            }else 
            {

            }
        }

        private void Carte2Click(object sender, RoutedEventArgs e)
        {
            if (partie.Joueurs.ElementAt(0).Main.ElementAt(2).TypeDeCarte == "VacheNormale")
            {

            }
            else
            {

            }
        }

        private void Carte3Click(object sender, RoutedEventArgs e)
        {
            if (partie.Joueurs.ElementAt(0).Main.ElementAt(3).TypeDeCarte == "VacheNormale")
            {

            }
            else
            {

            }
        }

        private void Carte4Click(object sender, RoutedEventArgs e)
        {
            if (partie.Joueurs.ElementAt(0).Main.ElementAt(4).TypeDeCarte == "VacheNormale")
            {

            }
            else
            {

            }
        }


    }
}
