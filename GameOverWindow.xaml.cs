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
using System.Windows.Shapes;

namespace Connect4Game
{
    /// <summary>
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        private MainWindow mainWindow;

        public GameOverWindow()
        {
            InitializeComponent();
        }
        //Closes the application after winning
        private void Nobtn_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow = (MainWindow)Application.Current.MainWindow;
            this.Close();
            Application.Current.Shutdown();
        }
        //Continues the application after winning
        private void Yesbtn_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow = (MainWindow)Application.Current.MainWindow;
            this.mainWindow.ResetBoard();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
