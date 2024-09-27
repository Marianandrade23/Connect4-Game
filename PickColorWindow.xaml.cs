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
    /// Interaction logic for PickColorWindow.xaml
    /// </summary>
    public partial class PickColorWindow : Window
    {
        private MainWindow mainWindow;

        private Token tempToken = new Token();

        public PickColorWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        //Allows you to select the color you want for your connect 4 chip
        public void ImageClickHandler(object sender, MouseEventArgs e)
        {
            Image clickedImage = (Image)sender;

            string imgTag = clickedImage.Tag.ToString();

            if(imgTag == "red")
            {
                if (this.mainWindow.p1.playerImg == null)
                {
                    this.mainWindow.p1 = new Player();
                    this.mainWindow.p1.playerImg = tempToken.RedChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;
 
                }
                else
                {
                    this.mainWindow.p2 = new Player();
                    this.mainWindow.p2.playerImg = tempToken.RedChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                    
                }
            }
            else if(imgTag == "yellow")
            {
                if (this.mainWindow.p1.playerImg == null)
                {
                    this.mainWindow.p1 = new Player();
                    this.mainWindow.p1.playerImg = tempToken.YellowChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                   
                }
                else
                {
                    this.mainWindow.p2 = new Player();
                    this.mainWindow.p2.playerImg = tempToken.YellowChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                    
                }
            }
            else if (imgTag == "green")
            {
                if (this.mainWindow.p1.playerImg == null)
                {
                    this.mainWindow.p1 = new Player();
                    this.mainWindow.p1.playerImg = tempToken.GreenChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                   
                }
                else
                {
                    this.mainWindow.p2 = new Player();
                    this.mainWindow.p2.playerImg = tempToken.GreenChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                   
                }
            }
            else if (imgTag == "blue")
            {
                if (this.mainWindow.p1.playerImg == null)
                {
                    this.mainWindow.p1 = new Player();
                    this.mainWindow.p1.playerImg = tempToken.BlueChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                   
                }
                else
                {
                    this.mainWindow.p2 = new Player();
                    this.mainWindow.p2.playerImg = tempToken.BlueChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                   
                }
            }
            else if (imgTag == "black")
            {
                if (this.mainWindow.p1.playerImg == null)
                {
                    this.mainWindow.p1 = new Player();
                    this.mainWindow.p1.playerImg = tempToken.BlackChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                    
                }
                else
                {
                    this.mainWindow.p2 = new Player();
                    this.mainWindow.p2.playerImg = tempToken.BlackChip;
                    clickedImage.IsEnabled = false;
                    this.Visibility = Visibility.Collapsed;

                    
                }
            }
        }
    }
}
