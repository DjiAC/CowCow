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
            Partie partie = new Partie("solo" , 5, (Joueur1.Text), (int.Parse(ChoixNbMouches.Text)));
            // Console.WriteLine(NbJoueursPartie);
            partie.JouerPartie();
            this.Hide();
            JeuWindow Jeu = new JeuWindow(partie);
            Jeu.ShowDialog();
        }

        private void RadioButtonTypeChecked(object sender, RoutedEventArgs e)
        {
            var RadioButtonType = sender as RadioButton;
            if (RadioButtonType == null)
                return;
             // string TypePartie = RadioButtonType.Content.ToString();
        }

        private void RadioButtonNbChecked(object sender, RoutedEventArgs e)
        {
            var RadioButtonNb = sender as RadioButton;
            if (RadioButtonNb == null)
                return;
            int NbJoueursPartie = int.Parse(RadioButtonNb.Content.ToString());
        }      

        private void OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
