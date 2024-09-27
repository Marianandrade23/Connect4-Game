using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.IO;
using System.Text.RegularExpressions;


namespace Connect4Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //Disabling buttons so you can't place pieces until new gameboard is created
            DisableBoard();

            Disable6x7Board();

            Disable10x7Board(); 

            //Populating comboBox when the main window opens. 
            SetComboBox();

            Top10ScoreWindow scoreWindow = new Top10ScoreWindow(this);

            scoreWindow.ShowDialog();
        }

        //Field instiation of gameboard. 
        public GameBoard gameboard = new GameBoard();

        // Field instantiation of Player class. 
        public Player player;

        //int for player turn
        public int playerTurn = 1;

        //int for the total number of moves made
        //so we can know when to start checking for winning
        public int numOfMoves = 0;

        //initialize a list of images in the board. 
        public List<Image> boardImgs = new List<Image>();

        public List<Image> board6x7Imgs = new List<Image>();

        public List<Image> board10x7Imgs = new List<Image>();

        //initialize player 1 and player 2 from player class. 
        public Player p1 = new Player();
        public Player p2 = new Player();

        //a variable to store the current player.
        public Player currentPlayer;

        // a bool to check if ai is blocking
        public bool aiIsBlocking = false;

        private Token lastSpotPlaced;

        public List<int> tempScores = new List<int>();

        private Rules rules = new Rules();

        private AboutUs aboutUs = new AboutUs();

        private void rulesBtn_Click(object sender, RoutedEventArgs e)
        {
            rules.ShowDialog();
        }

        //Start the game, allow the user to pick between difficult levels.
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            

            //statements to add the Images on the XAML to a list to use in our code.
            boardImgs.Add(tokenImage0_0);
            boardImgs.Add(tokenImage0_1);
            boardImgs.Add(tokenImage0_2);
            boardImgs.Add(tokenImage0_3);
            boardImgs.Add(tokenImage0_4);
            boardImgs.Add(tokenImage0_5);
            boardImgs.Add(tokenImage0_6);
            boardImgs.Add(tokenImage1_0);
            boardImgs.Add(tokenImage1_1);
            boardImgs.Add(tokenImage1_2);
            boardImgs.Add(tokenImage1_3);
            boardImgs.Add(tokenImage1_4);
            boardImgs.Add(tokenImage1_5);
            boardImgs.Add(tokenImage1_6);
            boardImgs.Add(tokenImage2_0);
            boardImgs.Add(tokenImage2_1);
            boardImgs.Add(tokenImage2_2);
            boardImgs.Add(tokenImage2_3);
            boardImgs.Add(tokenImage2_4);
            boardImgs.Add(tokenImage2_5);
            boardImgs.Add(tokenImage2_6);
            boardImgs.Add(tokenImage3_0);
            boardImgs.Add(tokenImage3_1);
            boardImgs.Add(tokenImage3_2);
            boardImgs.Add(tokenImage3_3);
            boardImgs.Add(tokenImage3_4);
            boardImgs.Add(tokenImage3_5);
            boardImgs.Add(tokenImage3_6);
            boardImgs.Add(tokenImage4_0);
            boardImgs.Add(tokenImage4_1);
            boardImgs.Add(tokenImage4_2);
            boardImgs.Add(tokenImage4_3);
            boardImgs.Add(tokenImage4_4);
            boardImgs.Add(tokenImage4_5);
            boardImgs.Add(tokenImage4_6);
            boardImgs.Add(tokenImage5_0);
            boardImgs.Add(tokenImage5_1);
            boardImgs.Add(tokenImage5_2);
            boardImgs.Add(tokenImage5_3);
            boardImgs.Add(tokenImage5_4);
            boardImgs.Add(tokenImage5_5);
            boardImgs.Add(tokenImage5_6);

            // statements to add the Images on the XAML to a list to use in our 6x7 board. 
            board6x7Imgs.Add(img0_0);
            board6x7Imgs.Add(img0_1);
            board6x7Imgs.Add(img0_2);
            board6x7Imgs.Add(img0_3);
            board6x7Imgs.Add(img0_4);
            board6x7Imgs.Add(img0_5);
            board6x7Imgs.Add(img1_0);
            board6x7Imgs.Add(img1_1);
            board6x7Imgs.Add(img1_2);
            board6x7Imgs.Add(img1_3);
            board6x7Imgs.Add(img1_4);
            board6x7Imgs.Add(img1_5);
            board6x7Imgs.Add(img2_0);
            board6x7Imgs.Add(img2_1);
            board6x7Imgs.Add(img2_2);
            board6x7Imgs.Add(img2_3);
            board6x7Imgs.Add(img2_4);
            board6x7Imgs.Add(img2_5);
            board6x7Imgs.Add(img3_0);
            board6x7Imgs.Add(img3_1);
            board6x7Imgs.Add(img3_2);
            board6x7Imgs.Add(img3_3);
            board6x7Imgs.Add(img3_4);
            board6x7Imgs.Add(img3_5);
            board6x7Imgs.Add(img4_0);
            board6x7Imgs.Add(img4_1);
            board6x7Imgs.Add(img4_2);
            board6x7Imgs.Add(img4_3);
            board6x7Imgs.Add(img4_4);
            board6x7Imgs.Add(img4_5);
            board6x7Imgs.Add(img5_0);
            board6x7Imgs.Add(img5_1);
            board6x7Imgs.Add(img5_2);
            board6x7Imgs.Add(img5_3);
            board6x7Imgs.Add(img5_4);
            board6x7Imgs.Add(img5_5);
            board6x7Imgs.Add(img6_0);
            board6x7Imgs.Add(img6_1);
            board6x7Imgs.Add(img6_2);
            board6x7Imgs.Add(img6_3);
            board6x7Imgs.Add(img6_4);
            board6x7Imgs.Add(img6_5);

            // statements to add the Images on the XAML to a list to use in 10x7 board. 
            board10x7Imgs.Add(img0_0);
            board10x7Imgs.Add(img0_1);
            board10x7Imgs.Add(img0_2);
            board10x7Imgs.Add(img0_3);
            board10x7Imgs.Add(img0_4);
            board10x7Imgs.Add(img0_5);
            board10x7Imgs.Add(img0_6);
            board10x7Imgs.Add(img0_7);
            board10x7Imgs.Add(img0_8);
            board10x7Imgs.Add(img0_9);
            board10x7Imgs.Add(img1_0);
            board10x7Imgs.Add(img1_1);
            board10x7Imgs.Add(img1_2);
            board10x7Imgs.Add(img1_3);
            board10x7Imgs.Add(img1_4);
            board10x7Imgs.Add(img1_5);
            board10x7Imgs.Add(img1_6);
            board10x7Imgs.Add(img1_7);
            board10x7Imgs.Add(img1_8);
            board10x7Imgs.Add(img1_9);
            board10x7Imgs.Add(img2_0);
            board10x7Imgs.Add(img2_1);
            board10x7Imgs.Add(img2_2);
            board10x7Imgs.Add(img2_3);
            board10x7Imgs.Add(img2_4);
            board10x7Imgs.Add(img2_5);
            board10x7Imgs.Add(img2_6);
            board10x7Imgs.Add(img2_7);
            board10x7Imgs.Add(img2_8);
            board10x7Imgs.Add(img2_9);
            board10x7Imgs.Add(img3_0);
            board10x7Imgs.Add(img3_1);
            board10x7Imgs.Add(img3_2);
            board10x7Imgs.Add(img3_3);
            board10x7Imgs.Add(img3_4);
            board10x7Imgs.Add(img3_5);
            board10x7Imgs.Add(img3_6);
            board10x7Imgs.Add(img3_7);
            board10x7Imgs.Add(img3_8);
            board10x7Imgs.Add(img3_9);
            board10x7Imgs.Add(img4_0);
            board10x7Imgs.Add(img4_1);
            board10x7Imgs.Add(img4_2);
            board10x7Imgs.Add(img4_3);
            board10x7Imgs.Add(img4_4);
            board10x7Imgs.Add(img4_5);
            board10x7Imgs.Add(img4_6);
            board10x7Imgs.Add(img4_7);
            board10x7Imgs.Add(img4_8);
            board10x7Imgs.Add(img4_9);
            board10x7Imgs.Add(img5_0);
            board10x7Imgs.Add(img5_1);
            board10x7Imgs.Add(img5_2);
            board10x7Imgs.Add(img5_3);
            board10x7Imgs.Add(img5_4);
            board10x7Imgs.Add(img5_5);
            board10x7Imgs.Add(img5_6);
            board10x7Imgs.Add(img5_7);
            board10x7Imgs.Add(img5_8);
            board10x7Imgs.Add(img5_9);
            board10x7Imgs.Add(img6_0);
            board10x7Imgs.Add(img6_1);
            board10x7Imgs.Add(img6_2);
            board10x7Imgs.Add(img6_3);
            board10x7Imgs.Add(img6_4);
            board10x7Imgs.Add(img6_5);
            board10x7Imgs.Add(img6_6);
            board10x7Imgs.Add(img6_7);
            board10x7Imgs.Add(img6_8);
            board10x7Imgs.Add(img6_9);

            //Enable Buttons
            //column1Button.IsEnabled = true;
            //column2Button.IsEnabled = true;
            //column3Button.IsEnabled = true;
            //column4Button.IsEnabled = true;
            //column5Button.IsEnabled = true;
            //column6Button.IsEnabled = true;
            //column7Button.IsEnabled = true;


            // Instantiate new Gameboard class and call the method for filiing the board spots. 
            // The if/else-if ensures we instantiate the correct sized board img and array 
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                gameboard.board = gameboard.makeBoard(6,7);
                gameboard.FillTheBoard(boardImgs,gameboard.board);

                EnableBoard();
                Disable10x7Board();
                Disable6x7Board();
                
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                gameboard.board = gameboard.makeBoard(7,6);
                gameboard.FillTheBoard(board6x7Imgs,gameboard.board);
                Enable6x7Board();
                Disable10x7Board();
                DisableBoard();
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                gameboard.board = gameboard.makeBoard(7,10);
                gameboard.FillTheBoard(board10x7Imgs, gameboard.board);
                Enable10x7Board();
                Disable6x7Board();
                DisableBoard();
            }
            else
            {
                gameboard.board = gameboard.makeBoard(6, 7);
                gameboard.FillTheBoard(boardImgs, gameboard.board);

                EnableBoard();
                Disable10x7Board();
                Disable6x7Board();
                chooseSizeComboBox.SelectedIndex = 0;
            }

            //Start timer
            StartTimer();

            // Set the current player as 1. 
            currentPlayer = p1;

            //Checks if we have a bot going first and calls the function to start their turn.
            if (currentPlayer.isBot == true && currentPlayer.PlayerName == "Difficult")
            {
                HardAIPlayer(gameboard.board, gameboard, currentPlayer.PlayerID);
            }
            else if (currentPlayer.isBot == true && currentPlayer.PlayerName == "Easy")
            {
                EasyAIplayer();
            }

            // This method chooses visibility of board. 
            boardVisible();

            //Disables all of the play options when you click the start button
            chooseSizeComboBox.IsEnabled = false;
            chooseAIButton.IsEnabled = false;
            choosePlayerButton.IsEnabled = false;
        }

        // Resets the game and game board, clears the score and resets the timer
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetBoard();
        }

        //Close the application
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Close Timer
            CloseTimer();
            //Close the application 
            Application.Current.Shutdown();
        }


        //click event for the first game board column
        private void column1btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 6;
            int row_to_search = 0;


            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {


                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }
                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    //Increments the row to search count
                    row_to_search++;
                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();


        }

        //click event for the second game board column
        private void column2btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 5;
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }


                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        //click event for the third game board column
        private void column3btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 4;
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        //click event for the fourth game board column
        private void column4btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 3;
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        //click event for the fourth game board column
        private void column5btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 2;
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        //click event for the fourth game board column
        private void column6btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 1;
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;
                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        //click event for the fourth game board column
        private void column7btn_Click(object sender, RoutedEventArgs e)
        {
            //variables for checking the column and row for each click
            int column_num = 0;
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[5, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;
                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;
                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        // Method to switch to the next player's turn
        private async void SwitchPlayerTurn()
        {
            // Stop the timer
            CloseTimer();
            if (currentPlayer.PlayerID == 1)
            {
                currentPlayer = p2;
                playerTurn = 2;
            }
            else if (currentPlayer.PlayerID == 2)
            {
                currentPlayer = p1;
                playerTurn = 1;
            }

            if (playerTurn == 1)
            {

                currentPlayer = p1;
                // Display a message box indicating the next player's turn
                player1Turnlbl.Content = "Your Turn";
                player2Turnlbl.Content = "";
                if (currentPlayer.isBot == true && currentPlayer.PlayerName == "Difficult")
                {
                    await Task.Delay(1000);
                    HardAIPlayer(gameboard.board, gameboard, currentPlayer.PlayerID);
                }
                else if (currentPlayer.isBot == true && currentPlayer.PlayerName == "Easy")
                {
                    await Task.Delay(1000);
                    EasyAIplayer();
                }
                else
                {
                    return;
                }

            }
            else if (playerTurn == 2)
            {
                currentPlayer = p2;
                // Display a message box indicating the next player's turn
                player2Turnlbl.Content = "Your Turn";
                player1Turnlbl.Content = "";
                if (currentPlayer.isBot == true && currentPlayer.PlayerName == "Difficult")
                {
                    await Task.Delay(1000);
                    HardAIPlayer(gameboard.board, gameboard, currentPlayer.PlayerID);
                    return;
                }
                else if (currentPlayer.isBot == true && currentPlayer.PlayerName == "Easy")
                {
                    await Task.Delay(1000);
                    EasyAIplayer();
                    return;
                }
                else
                {
                    return;
                }
            }


            // Reset and start the timer for the next player
            ResetTimer();
            StartTimer();
        }

        private void choosePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Another form created for getting player name and can access controls in another windows.  
            EnterNameWindow MyEnterNameWindow = new EnterNameWindow(this);

            // Brings up the other window  
            MyEnterNameWindow.ShowDialog();
        }

        private void chooseAIButton_Click(object sender, RoutedEventArgs e)
        {
            // Another form created for getting player name and can access controls in another windows.  
            AIWindow MyAIWindow = new AIWindow(this);

            // Brings up the other window  
            MyAIWindow.ShowDialog();
        }

        //Timer for the game
        private DispatcherTimer timer;

        // Declare a DispatcherTimer at the class level
        private int remainingSeconds = 30;

        // Method to start the timer
        private void StartTimer()
        {
            remainingSeconds = 30;
            TimerLabel.Content = remainingSeconds.ToString();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Method to reset the timer
        private void ResetTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                remainingSeconds = 30;
                TimerLabel.Content = remainingSeconds.ToString();
            }
        }

        // Method to stop the timer
        private void CloseTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        // Timer tick event handler
        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;

            // Update the timer label
            TimerLabel.Content = remainingSeconds.ToString();

            if (remainingSeconds == 0)
            {
                // Timer reached zero, switch to the next player's turn
                SwitchPlayerTurn();
            }
        }

        // A method to reset the board. 
        public void ResetBoard()
        {
            // Loop through the list of board images and set it them to null/reset. 
            foreach (Image img in boardImgs)
            {
                img.Source = null;
            }
            //refills the gameboard with new Token objects
            gameboard.FillTheBoard(boardImgs, gameboard.board);

            //Reset Timer
            ResetTimer();

            //Renables all of the play options when you click the reset button
            chooseSizeComboBox.IsEnabled = true;
            chooseAIButton.IsEnabled = true;
            choosePlayerButton.IsEnabled = true;
        }

        //method to disable the buttons that allow you to place tokens on the board
        public void DisableBoard()
        {
            column1btn.IsEnabled = false;
            column2btn.IsEnabled = false;
            column3btn.IsEnabled = false;
            column4btn.IsEnabled = false;
            column5btn.IsEnabled = false;
            column6btn.IsEnabled = false;
            column7btn.IsEnabled = false;

            boardImage.IsEnabled = false;


            //Margin="8,73,0,0" 
            //Margin="56,79,0,0" 
            //Margin="101,80,0,0"
            //Margin="147,74,0,0" 
            //Margin="196,79,0,0" - 5 
            //Margin="242,79,0,0" - 6 
            //Margin="291,71,0,0" - 7
            //Margin="341,68,0,0" - 8
            //Margin="44,73,0,0"  - 9
            //column10 - Margin="90,71,0,0"


            column10Button.Margin = new Thickness(1000, 0, 0, 0);
            column9Button.Margin = new Thickness(1000, 0, 0, 0);
            column8Button.Margin = new Thickness(1000, 0, 0, 0);
            column7Button.Margin = new Thickness(1000, 0, 0, 0);
            column6Button.Margin = new Thickness(1000, 0, 0, 0);
            column5Button.Margin = new Thickness(1000, 0, 0, 0);
            column6Button.Margin = new Thickness(1000, 0, 0, 0);
        }

        //method to enable the buttons that allow you to place tokens on board. 
        public void EnableBoard()
        {
            column1btn.IsEnabled = true;
            column2btn.IsEnabled = true;
            column3btn.IsEnabled = true;
            column4btn.IsEnabled = true;
            column5btn.IsEnabled = true;
            column6btn.IsEnabled = true;
            column7btn.IsEnabled = true;
            boardImage.IsEnabled = true;
        }

        //method to disable the buttons that allow you to place tokens on 6x7 board. 
        public void Disable6x7Board()
        {
            column1Button.IsEnabled = false;
            column2Button.IsEnabled = false;
            column3Button.IsEnabled = false;
            column4Button.IsEnabled = false; 
            column5Button.IsEnabled = false;
            column6Button.IsEnabled = false;

            board6x7Img.IsEnabled = false;
        }

        //method to enable the buttons that allow you to place tokens on 6x7 board. 
        public void Enable6x7Board() 
        {
            column1Button.IsEnabled = true;
            column2Button.IsEnabled = true;
            column3Button.IsEnabled = true;
            column4Button.IsEnabled = true;
            column5Button.IsEnabled = true;
            column6Button.IsEnabled = true;

            board6x7Img.IsEnabled = true;
        }

        //method that disable the buttons that allow you to place tokens on 10x7 board. 
        public void Disable10x7Board() 
        {
            column7Button.IsEnabled = false;
            column8Button.IsEnabled = false;
            column9Button.IsEnabled = false;
            column10Button.IsEnabled = false;

            board10x7Img.IsEnabled = false;
        }

        //method that enable the buttons that allow you to place tokens on 10x7 board. 
        public void Enable10x7Board()
        {
            column7Button.IsEnabled = true;
            column8Button.IsEnabled = true;
            column9Button.IsEnabled = true;
            column10Button.IsEnabled = true;

            board10x7Img.IsEnabled = true;
        }

        // Populating size combobox, it will automatically take us to assigned board when we select the board size. 
        public void SetComboBox()
        {
            chooseSizeComboBox.ItemsSource = new List<string> { "Standard", "6X7", "10X7" };

            board6x7Img.Visibility = Visibility.Hidden;
            board10x7Img.Visibility = Visibility.Hidden;
        }

        //Make the board visible and enable the board when player chooses the board size from combobox. 
        public void boardVisible()
        {
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                board6x7Img.Visibility = Visibility.Hidden;
                board10x7Img.Visibility = Visibility.Hidden;
                boardImage.Visibility = Visibility.Visible;
                EnableBoard(); 
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                boardImage.Visibility = Visibility.Hidden;
                board10x7Img.Visibility = Visibility.Hidden;
                board6x7Img.Visibility = Visibility.Visible;
                Enable6x7Board();
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                boardImage.Visibility = Visibility.Hidden;
                board6x7Img.Visibility = Visibility.Hidden;
                board10x7Img.Visibility = Visibility.Visible;
                Enable10x7Board();
            }
        }

        // A method for AI easy mode. 
        public async void EasyAIplayer()
        {
            // Creating a random object.
            Random rand = new Random();

            // Declare and assign AIcolumn variable to hold random integer between 0-6. 
            int AIcolumn = rand.Next(0, 6);

            // Slowing down reaction time by 2 seconds. 
            await Task.Delay(2000);

            // Loop to check lowest empty spot on board. 
            for (int row_to_search = 0; row_to_search <= 5; row_to_search++)
            {
                // Check if the spot in column is empty and the last row is empty. 
                if (gameboard.board[row_to_search, AIcolumn].spotID == 0)
                {
                    if (gameboard.board[5, AIcolumn].spotID == 0)
                    {
                        // If true, AI will make move - assign AIcolumn random int to spot. 
                        gameboard.board[row_to_search, AIcolumn].spotID = currentPlayer.PlayerID;
                        gameboard.board[row_to_search, AIcolumn].boardImg.Source =
                            currentPlayer.playerImg;
                        lastSpotPlaced = gameboard.board[row_to_search, AIcolumn];

                        //check to make sure we only check for winning after the 7th turn
                        if (numOfMoves >= 6)
                        {
                            if (gameboard.checkForWin(row_to_search, AIcolumn, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName) == true)
                            {
                                MessageBox.Show("winner");
                            }
                            //checks for a draw
                            if (gameboard.CheckForDraw() == true)
                            {
                                MessageBox.Show("There is a draw!");
                            }
                        }

                        // Go back to Human Player and switch on and on.  
                        playerTurn = 1;
                        numOfMoves++;
                        SwitchPlayerTurn();
                        break;
                    }
                    else
                    {
                        AIcolumn = rand.Next(0, 7);
                        row_to_search = 0;
                    }
                }
            }
        }

        //function for the hard version of the AI player
        public void HardAIPlayer(Token[,] board, GameBoard gb, int pturn)
        {
            int temp = 0;

            if (currentPlayer.PlayerID == 1)
            {
                temp = 2;
            }
            else if (currentPlayer.PlayerID == 2)
            {
                temp = 1;
            }

            if (currentPlayer.isBot == true && currentPlayer.PlayerID == pturn)
            {
                if (board[0, 3].spotID == 0)
                {
                    board[0, 3].spotID = pturn;
                    board[0, 3].boardImg.Source = currentPlayer.playerImg;
                    numOfMoves++;
                    SwitchPlayerTurn();
                    return;
                }
            }

            if (numOfMoves >= 5)
            {
                AiCheckForWin(board, gameboard, pturn);

                if (AiCheckForBlock(board, gb, pturn) == true)
                {
                    numOfMoves++;
                    SwitchPlayerTurn();
                    return;
                }
                else
                {
                    EasyAIplayer();
                    return;
                }
            }



            //Checking if the AI Hard player can block if two tokens are in a row
            if (board[0, 3].spotID == 0)
            {
                board[0, 3].spotID = pturn;
                board[0, 3].boardImg.Source = currentPlayer.playerImg;
                lastSpotPlaced = gameboard.board[0, 3];
                numOfMoves++;
                SwitchPlayerTurn();
                return;
            }
            else
            {
                GameBoard.hasTwo newEnum = gameboard.checkForTwoInARow(lastSpotPlaced, temp, board);
                bool hadTwo = false;
                if (newEnum == GameBoard.hasTwo.hasTwoDown)
                {
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column].spotID == 0 && gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row + 1, lastSpotPlaced.column, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
                else if (newEnum == GameBoard.hasTwo.hasTwoLeft)
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column - 1].spotID == 0 && gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column - 1].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column - 1].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column - 1].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row, lastSpotPlaced.column - 1, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }

                else if (newEnum == GameBoard.hasTwo.hasTwoRight)
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column + 1].spotID == 0 && gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column + 1].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column + 1].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column + 1].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row, lastSpotPlaced.column + 1, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }

                else if (newEnum == GameBoard.hasTwo.hasTwoUpRightDiagonal)
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column - 1].spotID == 0 && gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column - 1].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column - 1].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column - 1].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row + 1, lastSpotPlaced.column - 1, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }

                else if (newEnum == GameBoard.hasTwo.hasTwoUpLeftDiagonal)
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column + 1].spotID == 0 && gameboard.board[lastSpotPlaced.row, lastSpotPlaced.column + 1].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column + 1].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row + 1, lastSpotPlaced.column + 1].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row + 1, lastSpotPlaced.column + 1, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {


                    }

                else if (newEnum == GameBoard.hasTwo.hasTwoDownRightDiagonal)
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column - 1].spotID == 0 && gameboard.board[lastSpotPlaced.row - 2, lastSpotPlaced.column - 1].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column - 1].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column - 1].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row - 1, lastSpotPlaced.column - 1, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {


                    }

                else if (newEnum == GameBoard.hasTwo.hasTwoDownLeftDiagnoal)
                    try
                    {
                        if (gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column + 1].spotID == 0 && gameboard.board[lastSpotPlaced.row - 2, lastSpotPlaced.column + 1].spotID != 0)
                        {
                            gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column + 1].spotID = pturn;
                            gameboard.board[lastSpotPlaced.row - 1, lastSpotPlaced.column + 1].boardImg.Source = currentPlayer.playerImg;
                            gameboard.checkForWin(lastSpotPlaced.row - 1, lastSpotPlaced.column + 1, pturn, board, currentPlayer.PlayerName);
                            numOfMoves++;
                            SwitchPlayerTurn();
                            hadTwo = true;
                        }
                        else
                        {

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                if (hadTwo == false)
                {
                    EasyAIplayer();
                }

            }

        }


        //Checks for the AI's ability to win
        public void AiCheckForWin(Token[,] board, GameBoard gb, int id)
        {
            foreach (Token spot in board)
            {
                if (spot.spotID == 0)
                {
                    spot.spotID = id;

                    if (gb.checkForWin(spot.row, spot.column, id, board, currentPlayer.PlayerName) == true)
                    {

                        try
                        {
                            if (gameboard.board[spot.row - 1, spot.column].spotID != 0)
                            {
                                gameboard.board[spot.row, spot.column].boardImg.Source = currentPlayer.playerImg;
                                break;
                            }
                            else if (gameboard.board[spot.row - 1, spot.column].spotID == 0)
                            {
                                EasyAIplayer();
                                return;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                    spot.spotID = 0;
                }
                else
                {
                    continue;
                }
            }
        }


        //Checks for the ability for the AI to block
        public bool AiCheckForBlock(Token[,] board, GameBoard gb, int id)
        {
            aiIsBlocking = true;
            foreach (Token spot in board)
            {
                int temp = 0;
                string tempName = "";
                if (spot.spotID == 0)
                {

                    if (currentPlayer.PlayerID == 1)
                    {
                        temp = 2;
                        tempName = p2.PlayerName;
                    }
                    else if (currentPlayer.PlayerID == 2)
                    {
                        temp = 1;
                        tempName = p1.PlayerName;
                    }
                    spot.spotID = temp;

                    if (gb.checkForWin(spot.row, spot.column, temp, board, tempName) == true)
                    {
                        try
                        {
                            if (board[spot.row - 1, spot.column].spotID != 0)
                            {
                                gameboard.board[spot.row, spot.column].spotID = id;
                                gameboard.board[spot.row, spot.column].boardImg.Source = currentPlayer.playerImg;
                                return true;
                            }
                            else if (board[spot.row - 1, spot.column].spotID == 0)
                            {
                                return false;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            gameboard.board[spot.row, spot.column].spotID = id;
                            gameboard.board[spot.row, spot.column].boardImg.Source = currentPlayer.playerImg;
                            return true;
                        }
                    }
                    spot.spotID = 0;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

       //Displays the about us information
        private void aboutUsBtn_Click(object sender, RoutedEventArgs e)
        {
            aboutUs.ShowDialog();
        }

        private void top10RankingBtn_Click(object sender, RoutedEventArgs e)
        {
            // Another form created for getting top 10 players' score and can access controls in another windows.  
            Top10ScoreWindow ScoreWindow = new Top10ScoreWindow(this);

            // Brings up the other window  
            ScoreWindow.ShowDialog();
        }

        private void column1Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 5;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 5;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 9;
            }
            int row_to_search = 0;

            // for loop to check for the lowest empty spot in the clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                // main if-else block to check for an empty Spot object in the clicked column
                if (gameboard.board[row_to_search,column_num].spotID == 0 && gameboard.board[4,column_num].spotID == 0)
                {
                    // sets the spotId of the now-filled spot to 1 and fills it with the appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search,column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search,column_num];
                    aiIsBlocking = false;

                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search,column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;
                }
            }

            // After the player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        private void column2Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 5;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 4;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 8;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search,column_num].spotID == 0 && gameboard.board[4,column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search,column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search,column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search,column_num];
                    aiIsBlocking = false;

                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search,column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        private void column3Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 4;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 3;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 7;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;

                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        private void column4Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 3;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 2;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 6;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;

                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }

                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        private void column5Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 2;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 1;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 5;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;

                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }


                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        private void column6Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 1;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 0;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 4;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
            {
                //main if-else block to check for an empty Spot object in clicked column
                if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                {

                    //sets the spotId of the now filled spot to 1 and fills with appropriate image
                    gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                    gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                    lastSpotPlaced = gameboard.board[row_to_search, column_num];
                    aiIsBlocking = false;

                    //check to make sure we only check for winning after the 7th turn
                    if (numOfMoves >= 6)
                    {
                        gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                        //checks for a draw
                        if (gameboard.CheckForDraw() == true)
                        {
                            MessageBox.Show("There is a draw!");
                        }
                    }


                    //Increments the number of moves and switches the current players turn
                    numOfMoves++;
                    SwitchPlayerTurn();
                    break;
                }
                else
                {
                    row_to_search++;

                }


            }
            // After player makes a move, reset and start the timer for the current player
            ResetTimer();
            StartTimer();
        }

        private void column7Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 5;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 4;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 3;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
                {
                    //main if-else block to check for an empty Spot object in clicked column
                    if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                    {

                        //sets the spotId of the now filled spot to 1 and fills with appropriate image
                            gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                        gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                        lastSpotPlaced = gameboard.board[row_to_search, column_num];
                        aiIsBlocking = false;

                        //check to make sure we only check for winning after the 7th turn
                        if (numOfMoves >= 6)
                        {
                            gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                            //checks for a draw
                            if (gameboard.CheckForDraw() == true)
                            {
                                MessageBox.Show("There is a draw!");
                            }
                        }


                        //Increments the number of moves and switches the current players turn
                        numOfMoves++;
                        SwitchPlayerTurn();
                        break;
                    }
                    else
                    {
                        row_to_search++;

                    }
                }

            }

        private void column8Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 5;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 4;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 2;
            }
            int row_to_search = 0;

                //for loop to check for lowest empty spot in clicked column
                for (int i = 0; i <= row_to_search; i++)
                {
                    //main if-else block to check for an empty Spot object in clicked column
                    if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                    {

                        //sets the spotId of the now filled spot to 1 and fills with appropriate image
                        gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                        gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                        lastSpotPlaced = gameboard.board[row_to_search, column_num];
                        aiIsBlocking = false;

                        //check to make sure we only check for winning after the 7th turn
                        if (numOfMoves >= 6)
                        {
                            gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                            //checks for a draw
                            if (gameboard.CheckForDraw() == true)
                            {
                                MessageBox.Show("There is a draw!");
                            }
                        }

                        //Increments the number of moves and switches the current players turn
                        numOfMoves++;
                        SwitchPlayerTurn();
                        break;
                    }
                    else
                    {
                        row_to_search++;

                    }
                }
            
        }


        private void column9Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            if (chooseSizeComboBox.SelectedIndex == 2)
            {
                //variables for checking the column and row for each click
                if (chooseSizeComboBox.SelectedIndex == 0)
                {
                    column9Button.IsEnabled = false;
                }
                else if (chooseSizeComboBox.SelectedIndex == 1)
                {
                    column9Button.IsEnabled = false;
                    return;
                }
                else if (chooseSizeComboBox.SelectedIndex == 2)
                {
                    column_num = 1;
                }
                int row_to_search = 0;

                //for loop to check for lowest empty spot in clicked column
                for (int i = 0; i <= row_to_search; i++)
                {
                    //main if-else block to check for an empty Spot object in clicked column
                    if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                    {

                        //sets the spotId of the now filled spot to 1 and fills with appropriate image
                        gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                        gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                        lastSpotPlaced = gameboard.board[row_to_search, column_num];
                        aiIsBlocking = false;

                        //check to make sure we only check for winning after the 7th turn
                        if (numOfMoves >= 6)
                        {
                            gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                            //checks for a draw
                            if (gameboard.CheckForDraw() == true)
                            {
                                MessageBox.Show("There is a draw!");
                            }
                        }

                        //Increments the number of moves and switches the current players turn
                        numOfMoves++;
                        SwitchPlayerTurn();
                        break;
                    }
                    else
                    {
                        row_to_search++;

                    }
                }
            }
        }

        private void column10Button_Click(object sender, RoutedEventArgs e)
        {
            int column_num = 0;
            //variables for checking the column and row for each click
            if (chooseSizeComboBox.SelectedIndex == 0)
            {
                column_num = 5;
            }
            else if (chooseSizeComboBox.SelectedIndex == 1)
            {
                column_num = 4;
            }
            else if (chooseSizeComboBox.SelectedIndex == 2)
            {
                column_num = 0;
            }
            int row_to_search = 0;

            //for loop to check for lowest empty spot in clicked column
            for (int i = 0; i <= row_to_search; i++)
                {
                    //main if-else block to check for an empty Spot object in clicked column
                    if (gameboard.board[row_to_search, column_num].spotID == 0 && gameboard.board[4, column_num].spotID == 0)
                    {

                        //sets the spotId of the now filled spot to 1 and fills with appropriate image
                        gameboard.board[row_to_search, column_num].spotID = currentPlayer.PlayerID;
                        gameboard.board[row_to_search, column_num].boardImg.Source = currentPlayer.playerImg;
                        lastSpotPlaced = gameboard.board[row_to_search, column_num];
                        aiIsBlocking = false;

                        //check to make sure we only check for winning after the 7th turn
                        if (numOfMoves >= 6)
                        {
                            gameboard.checkForWin(row_to_search, column_num, currentPlayer.PlayerID, gameboard.board, currentPlayer.PlayerName);
                            //checks for a draw
                            if (gameboard.CheckForDraw() == true)
                            {
                                MessageBox.Show("There is a draw!");
                            }
                        }

                        //Increments the number of moves and switches the current players turn
                        numOfMoves++;
                        SwitchPlayerTurn();
                        break;
                    }
                    else
                    {
                        row_to_search++;

                    }
                }
            }

        //On the window loaded it sets the application's main window to this Main Window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
        }
    }

}



