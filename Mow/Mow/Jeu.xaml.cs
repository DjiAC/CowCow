using System;
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

namespace Mow
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class JeuWindow : Window
    {
        public JeuWindow()
        {
            InitializeComponent();
        }

        public object NavigationService { get; private set; }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }  

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuWindow Menu = new MenuWindow();
            Menu.ShowDialog();
        }
    }
}
