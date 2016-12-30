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
        public object NavigationService { get; private set; }

        public ReglesWindow()
        {
            InitializeComponent();
        }

        private void Regles1Popup_Click(object sender, RoutedEventArgs e)
        {
            Regles1Popup.IsOpen = true;
        }

        private void Regles1Popup_ReClick(object sender, RoutedEventArgs e)
        {
            Regles1Popup.IsOpen = false;
        }

        private void Regles2Popup_Click(object sender, RoutedEventArgs e)
        {
            Regles2Popup.IsOpen = true;
        }

        private void Regles2Popup_ReClick(object sender, RoutedEventArgs e)
        {
            Regles2Popup.IsOpen = false;
        }

        private void Regles3Popup_Click(object sender, RoutedEventArgs e)
        {
            Regles3Popup.IsOpen = true;
        }

        private void Regles3Popup_ReClick(object sender, RoutedEventArgs e)
        {
            Regles3Popup.IsOpen = false;
        }        
    }
}
