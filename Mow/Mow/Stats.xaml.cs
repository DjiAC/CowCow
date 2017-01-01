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

        public StatsWindow()
        {
            InitializeComponent();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MenuWindow Menu = new MenuWindow();
            Menu.ShowDialog();
        }
    }
}
