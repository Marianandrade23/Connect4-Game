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
    /// Interaction logic for AIWindow.xaml
    /// </summary>
    public partial class AIWindow : Window
    {
        // private field instantiation, to have access to mainwindow controls. 
        private MainWindow mainWindow;

        //temporary token for getting source colors
        Token tempToken = new Token();

        // playerID List to store player's name and assign ID. 
        public List<string> playerNames = new List<string>();

        // A field variable to hold AI level. 
        public string choosenAI;

        public AIWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            // Mainwindow instantiation.
            this.mainWindow = mainWindow;

            // Call reset method. 
            reset1();

            // Populating comboBox of AI difficulty level. 
            comboBox(); 
        }

        private void easyAIbtn_Click(object sender, RoutedEventArgs e)
        {
            // Set choosenAI as Easy. 
            choosenAI = "Easy"; 
            reset2();
        }

        private void diffAIbtn_Click(object sender, RoutedEventArgs e)
        {
            // Set choosenAI as Difficult. 
            choosenAI = "Difficult"; 
            reset2();
        }

        private void randomAIbtn_Click(object sender, RoutedEventArgs e)
        {
            // Set choosenAI as Random. 
            choosenAI = "Random";

            // Call reset2 method. 
            reset2();

            // This function letting us randomize difficulty levels with random numbers. 
            // Declaring int variable to hold random number. 
            int randomAI;

            // Creating random object. 
            Random random = new Random();

            // Assign 0 to 2 random numbers in randomAI variable. 
            randomAI = random.Next(2);

            // If randomAI is 0, AI is easy, else it's difficult AI. 
            if (randomAI == 0)
            {
                choosenAI = "Easy";   
            }
            else
            {
                choosenAI = "Difficult";
            }
        }

        private void AIvsAIbtn_Click(object sender, RoutedEventArgs e)
        {
            // Call reset4 method. 
            reset4();
        }


        // Visible - First, choose AI level you want to play. 
        public void reset1()
        {
            nameLabel.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Hidden;
            continueButton.Visibility = Visibility.Hidden;

            tokenLabel.Visibility = Visibility.Hidden;
            redTokenImg.Visibility = Visibility.Hidden;
            yellowTokenImg.Visibility = Visibility.Hidden;
            greenTokenImg.Visibility = Visibility.Hidden;
            blueTokenImg.Visibility = Visibility.Hidden;
            blackTokenImg.Visibility = Visibility.Hidden;
            p1Lbl.Visibility = Visibility.Hidden;
            p2Lbl.Visibility = Visibility.Hidden;

            easyVSeasyBtn.Visibility = Visibility.Hidden;
            levelComboBox.Visibility = Visibility.Hidden;
            continue2Button.Visibility = Visibility.Hidden;
            AILabel.Visibility = Visibility.Hidden;
        }

        // Visible - Second, enter your name. 
        public void reset2()
        {
            lvlLabel.Visibility = Visibility.Hidden;
            easyAIbtn.Visibility = Visibility.Hidden;
            diffAIbtn.Visibility = Visibility.Hidden;
            randomAIbtn.Visibility = Visibility.Hidden;
            AIvsAIbtn.Visibility = Visibility.Hidden;

            tokenLabel.Visibility = Visibility.Hidden;
            redTokenImg.Visibility = Visibility.Hidden;
            yellowTokenImg.Visibility = Visibility.Hidden;
            greenTokenImg.Visibility= Visibility.Hidden;
            blackTokenImg.Visibility = Visibility.Hidden;
            blueTokenImg.Visibility = Visibility.Hidden;
            p1Lbl.Visibility = Visibility.Hidden;
            p2Lbl.Visibility = Visibility.Hidden;

            easyVSeasyBtn.Visibility = Visibility.Hidden;
            levelComboBox.Visibility = Visibility.Hidden;
            continue2Button.Visibility = Visibility.Hidden;
            AILabel.Visibility = Visibility.Hidden;

            nameLabel.Visibility = Visibility.Visible;
            nameTextBox.Visibility = Visibility.Visible;
            continueButton.Visibility = Visibility.Visible;
        }

        // Visible - Third, choose token color you want to play. 
        public void reset3()
        {
            lvlLabel.Visibility = Visibility.Hidden;
            easyAIbtn.Visibility = Visibility.Hidden;
            diffAIbtn.Visibility = Visibility.Hidden;
            randomAIbtn.Visibility = Visibility.Hidden;
            AIvsAIbtn.Visibility = Visibility.Hidden;

            nameLabel.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Hidden;
            continueButton.Visibility = Visibility.Hidden;

            easyVSeasyBtn.Visibility = Visibility.Hidden;
            levelComboBox.Visibility = Visibility.Hidden;
            continue2Button.Visibility = Visibility.Hidden;
            AILabel.Visibility = Visibility.Hidden;

            tokenLabel.Visibility = Visibility.Visible;
            redTokenImg.Visibility = Visibility.Visible;
            yellowTokenImg.Visibility = Visibility.Visible;
            greenTokenImg.Visibility = Visibility.Visible;
            blueTokenImg.Visibility = Visibility.Visible;
            blackTokenImg.Visibility = Visibility.Visible;
            p1Lbl.Visibility = Visibility.Visible;
            p2Lbl.Visibility = Visibility.Visible;
        }

        // If user chooses to watch the game between AI's, chooose Level. 
        public void reset4()
        {
            lvlLabel.Visibility = Visibility.Hidden;
            easyAIbtn.Visibility = Visibility.Hidden;
            diffAIbtn.Visibility = Visibility.Hidden;
            randomAIbtn.Visibility = Visibility.Hidden;
            AIvsAIbtn.Visibility = Visibility.Hidden;

            easyVSeasyBtn.Visibility = Visibility.Visible;
            levelComboBox.Visibility = Visibility.Visible;
            AILabel.Visibility = Visibility.Visible;
            continue2Button.Visibility = Visibility.Visible;
        }


        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            // If AI level is easy. 
            if (choosenAI == "Easy")
            {
                // If list of playerNames equals to 0. 
                if (playerNames.Count == 0)
                {
                    // Add player's name from ListBox and store it in the List. 
                    playerNames.Add(nameTextBox.Text);

                    // Display the player name on name label. 
                    mainWindow.player1NameLbl.Content = playerNames[0];

                    // Display the AI level on name Label. 
                    mainWindow.player2NameLbl.Content = "Easy";

                    // Add easy AI bot to player's list. 
                    playerNames.Add("Easy");

                    // Clear the textBox. 
                    nameTextBox.Clear();

                    // Call reset3 method. 
                    reset3(); 
                }
            }
            else if(choosenAI == "Difficult")
            {
                if (playerNames.Count == 0)
                {
                    playerNames.Add(nameTextBox.Text);

                    mainWindow.player1NameLbl.Content = playerNames[0];

                    mainWindow.player2NameLbl.Content = "Difficult";

                    playerNames.Add("Difficult");

                    nameTextBox.Clear();

                    reset3(); 
                }
            }
            else if (choosenAI == "Random")
            {
                if (playerNames.Count == 0)
                {
                    playerNames.Add(nameTextBox.Text);

                    mainWindow.player1NameLbl.Content = playerNames[0];

                    mainWindow.player2NameLbl.Content = "Random";

                    playerNames.Add("Random"); 
                    
                    nameTextBox.Clear();

                    reset3(); 
                }
            }
        }

        private void continue2Button_Click(object sender, RoutedEventArgs e)
        {
            // The user is able to choose between AI bots depending on selected index of comboBox. 
            // Easy vs Easy
            if(levelComboBox.SelectedIndex == 0)
            {
                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = "Easy";
                this.mainWindow.p2.playerImg = this.tempToken.RedChip;

                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = true;
                this.mainWindow.p1.PlayerName = "Easy";
                this.mainWindow.p1.playerImg = this.tempToken.YellowChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;

                mainWindow.player1NameLbl.Content = "Easy";
                mainWindow.player1NameLbl.Background = Brushes.Red;
                mainWindow.player2NameLbl.Content = "Easy";
                mainWindow.player2NameLbl.Background = Brushes.Yellow;
                MessageBox.Show("You have choosen to see 2 Easy AI's play");
                Close();
            }
            // Easy vs Difficult. 
            else if(levelComboBox.SelectedIndex == 1)
            {
                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = true;
                this.mainWindow.p1.PlayerName = "Easy";
                this.mainWindow.p1.playerImg = this.tempToken.RedChip;

                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot= true;
                this.mainWindow.p2.PlayerName = "Difficult";
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;

                mainWindow.player1NameLbl.Content = "Easy";
                mainWindow.player1NameLbl.Background = Brushes.Red;
                mainWindow.player2NameLbl.Content = "Difficult";
                mainWindow.player2NameLbl.Background = Brushes.Yellow;
                MessageBox.Show("You have choosen to see 1 Easy AI and 1 Difficult AI play");
                Close();

            }
            // Difficult vs Difficult.
            else if(levelComboBox.SelectedIndex == 2)
            {
                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = "Difficult";
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = true;
                this.mainWindow.p1.PlayerName = "Difficult";
                this.mainWindow.p1.playerImg = this.tempToken.RedChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;

                mainWindow.player1NameLbl.Content = "Difficult";
                mainWindow.player1NameLbl.Background = Brushes.Red;
                mainWindow.player2NameLbl.Content = "Difficult";
                mainWindow.player2NameLbl.Background = Brushes.Yellow;
                MessageBox.Show("You have choosen to see 2 Difficult AI's play");
                Close();
            }
            // Difficult vs Easy 
            else if(levelComboBox.SelectedIndex == 3)
            {
                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 1;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = "Easy";
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 2;
                this.mainWindow.p1.isBot = true;
                this.mainWindow.p1.PlayerName = "Difficult";
                this.mainWindow.p1.playerImg = this.tempToken.RedChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;

                mainWindow.player1NameLbl.Content = "Easy";
                mainWindow.player1NameLbl.Background = Brushes.Red;
                mainWindow.player2NameLbl.Content = "Difficult";
                mainWindow.player2NameLbl.Background = Brushes.Yellow;
                MessageBox.Show("You have choosen to see 1 Easy AI and 1 Difficult AI play");
                Close();
            }
        }

        public void ImageClickHandler(object sender, MouseButtonEventArgs e)
        {
            // I found this method while researching wpf image click.
            // Image type of object, sender represents trigger from xaml, assigned to clickedImage type of Image. 
            // Creating click event on Image.
            Image clickedImage = (Image)sender;

            // I created Tag="" on xaml related to token images and assigning it to Imagetag variable. 
            string ImageTag = clickedImage.Tag.ToString();

           // Sets the AI chip color to one different to the current players chip
           if (ImageTag == "red")
           {
                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = this.playerNames[1];
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = false;
                this.mainWindow.p1.PlayerName = this.playerNames[0];
                this.mainWindow.p1.playerImg = this.tempToken.RedChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;
           }
           else if (ImageTag == "yellow")
           {
                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = false;
                this.mainWindow.p1.PlayerName = this.playerNames[0];
                this.mainWindow.p1.playerImg = this.tempToken.YellowChip;

                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = this.playerNames[1];
                this.mainWindow.p2.playerImg = this.tempToken.RedChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;
           }
           else if (ImageTag == "green")
           {
                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = false;
                this.mainWindow.p1.PlayerName = this.playerNames[0];
                this.mainWindow.p1.playerImg = this.tempToken.GreenChip;

                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = this.playerNames[1];
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;
           }
           else if (ImageTag == "blue")
            {
                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = false;
                this.mainWindow.p1.PlayerName = this.playerNames[0];
                this.mainWindow.p1.playerImg = this.tempToken.BlueChip;

                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = this.playerNames[1];
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;
            }
            else if (ImageTag == "black")
            {
                this.mainWindow.p1 = new Player();
                this.mainWindow.p1.PlayerID = 1;
                this.mainWindow.p1.isBot = false;
                this.mainWindow.p1.PlayerName = this.playerNames[0];
                this.mainWindow.p1.playerImg = this.tempToken.BlackChip;

                this.mainWindow.p2 = new Player();
                this.mainWindow.p2.PlayerID = 2;
                this.mainWindow.p2.isBot = true;
                this.mainWindow.p2.PlayerName = this.playerNames[1];
                this.mainWindow.p2.playerImg = this.tempToken.YellowChip;

                this.mainWindow.currentPlayer = this.mainWindow.p1;
            }

            Close(); 
        }

        // Method to populate AI levels. 
        public void comboBox()
        {
            levelComboBox.ItemsSource = new List<string> { "EASY vs EASY", "EASY vs DIFFICULT", "DIFFICULT vs DIFFICULT", "DIFFICULT vs EASY" };
        }
    }
}
