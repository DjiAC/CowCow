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
        public MenuWindow()
        {
            InitializeComponent();
        }
        
        private void JouerClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            JeuWindow Jeu = new JeuWindow();
            Jeu.ShowDialog();            
        }

        private void StatsClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            StatsWindow Stats = new StatsWindow();
            Stats.ShowDialog();
        }

        private void ReglesClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ReglesWindow Regles = new ReglesWindow();
            Regles.ShowDialog();
        }

        private void CreditsClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreditsWindow Credits = new CreditsWindow();
            Credits.ShowDialog();
        }
    }
}
