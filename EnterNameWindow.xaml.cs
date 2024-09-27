using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for EnterNameWindow.xaml
    /// </summary>
    public partial class EnterNameWindow : Window
    {
        private MainWindow mainWindow;
        private PickColorWindow pickColorWindow;

        // playerID List to store player's name and assign ID. 
        public List<string> playerNames = new List<string>();

        public EnterNameWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            // Mainwindow instantiation.
            this.mainWindow  = mainWindow;

            this.pickColorWindow = new PickColorWindow(mainWindow);
        }

        // A button to enter player names. 
        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            // If list of players equal to 0. 
            if (playerNames.Count == 0)
            {
                // Add player's name from ListBox and store it in the List. 
                playerNames.Add(nameTextBox.Text);

                // Display the player name on name label. 
                mainWindow.player1NameLbl.Content = playerNames[0];

                pickColorWindow.ShowDialog();

                // Assign player's name to player ID. 
                this.mainWindow.p1.PlayerName = playerNames[0];

                // Clear the textbox. 
                nameTextBox.Clear();

                // Give the textbox focus.
                nameTextBox.Focus();

            }
            else if (playerNames.Count == 1)
            {
                // Add the 2nd player if playerID index is 1.
                playerNames.Add(nameTextBox.Text);

                // Assign player's name to player ID. 
                mainWindow.player2NameLbl.Content = playerNames[1];

                // Show pickColorWindow. 
                this.pickColorWindow.ShowDialog();

                //Assign player's name to player ID. 
                this.mainWindow.p2.PlayerName = playerNames[1];
                
                // Close the window. 
                Close();
            }

            // Assign player's ID.
            this.mainWindow.p1.PlayerID = 1;
            this.mainWindow.p2.PlayerID = 2;
        }
    }
}
