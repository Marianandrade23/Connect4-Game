using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Connect4Game
{
    public class GameBoard
    {
        //Fields needed for the GameBoard class
        Token token;
        public Token[,] board;
        private MainWindow mainWindow;
        private Top10ScoreWindow top10ScoreWindow;

        //Method for creating a two deminsional array for game board-/rows/column/.
        public Token[,] makeBoard(int numOfRows, int numOfColumns)
        {
            Token[,] newBoard = new Token[numOfRows, numOfColumns];
            mainWindow = (MainWindow)Application.Current.MainWindow;
            top10ScoreWindow = new Top10ScoreWindow(mainWindow);
            return newBoard;
        }

        //Reference to the game over window so we can display it when there is a win or a draw
        private GameOverWindow gameOverWindow = new GameOverWindow();

        //enum for the hard ai player to see if there is two in a row of a color
        public enum hasTwo
        {
            hasNone,
            hasTwoDown,
            hasTwoLeft,
            hasTwoRight,
            hasTwoUpRightDiagonal,
            hasTwoUpLeftDiagonal,
            hasTwoDownRightDiagonal,
            hasTwoDownLeftDiagnoal
        }


        // This method will fill the circles in the board with custom Token classes. 
        public void FillTheBoard(List<Image> imgs, Token[,] board)
        {

            //temporary int variable to assign images at the correct index
            int temp = 0;

            //nested for loop to set the board spots
            for (int i = board.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = board.GetLength(1) - 1; j >= 0; j--)
                {
                    token = new Token();
                    board[i, j] = token;
                    token.FillTheTokens(i, j, imgs[temp]);
                    temp++;
                }
            }
        }


        //method to check for winning
        public bool checkForWin(int row, int column, int currentPlayer, Token[,] board, string name)
        {
            //bool to track if a player has won.
            bool playerHasWon = false;

            //multiple try-catch blocks to check for all of the different winning conditions.
            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row - 1, column].spotID == currentPlayer &&
                    board[row - 2, column].spotID == currentPlayer &&
                    board[row - 3, column].spotID == currentPlayer)
                    {
                        if (this.mainWindow.currentPlayer.PlayerID == 1)
                        { 
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins");
                                mainWindow.currentPlayer.numOfWins++;
                                if(checkIfPlayerExists(mainWindow.currentPlayer.PlayerName,true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: "  + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt",mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                                else
                                {
                                    mainWindow.p1.numOfLosses++;
                                    top10ScoreWindow.playerLoseScore(mainWindow.p1.PlayerName, mainWindow.p1.numOfWins,mainWindow.p1.numOfLosses,mainWindow.p1.numOfDraws);
                                }
                                mainWindow.score1Lbl.Content = "Score: " + mainWindow.currentPlayer.numOfWins.ToString();
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins,mainWindow.currentPlayer.numOfLosses,mainWindow.currentPlayer.numOfDraws);
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    top10ScoreWindow.playerLoseScore(mainWindow.p2.PlayerName, mainWindow.p2.numOfWins,mainWindow.p2.numOfLosses,mainWindow.p2.numOfDraws);
                                }
                                else
                                {
                                    mainWindow.p1.numOfLosses++;
                                    top10ScoreWindow.playerLoseScore(mainWindow.p1.PlayerName, mainWindow.p1.numOfWins, mainWindow.p1.numOfLosses, mainWindow.p1.numOfDraws);
                                }
                                mainWindow.score1Lbl.Content = "Score: " + mainWindow.currentPlayer.numOfWins.ToString();
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                            }
                        }
                        playerHasWon = true;
                    }
                }

            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row, column + 1].spotID == currentPlayer &&
                    board[row, column + 2].spotID == currentPlayer &&
                    board[row, column + 3].spotID == currentPlayer)
                    {
                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }


            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row, column - 1].spotID == currentPlayer &&
                    board[row, column - 2].spotID == currentPlayer &&
                    board[row, column - 3].spotID == currentPlayer)
                    {
                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {

                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row, column + 1].spotID == currentPlayer &&
                    board[row, column - 1].spotID == currentPlayer &&
                    board[row, column - 2].spotID == currentPlayer)
                    {
                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }

                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row, column - 1].spotID == currentPlayer &&
                    board[row, column + 1].spotID == currentPlayer &&
                    board[row, column + 2].spotID == currentPlayer)
                    {
                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row - 1, column - 1].spotID == currentPlayer &&
                    board[row - 2, column - 2].spotID == currentPlayer &&
                    board[row - 3, column - 3].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row - 1, column + 1].spotID == currentPlayer &&
                    board[row - 2, column + 2].spotID == currentPlayer &&
                    board[row - 3, column + 3].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row + 1, column + 1].spotID == currentPlayer &&
                    board[row + 2, column + 2].spotID == currentPlayer &&
                    board[row + 3, column + 3].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row + 1, column - 1].spotID == currentPlayer &&
                    board[row + 2, column - 2].spotID == currentPlayer &&
                    board[row + 3, column - 3].spotID == currentPlayer)
                    {
                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row + 1, column + 1].spotID == currentPlayer &&
                    board[row - 1, column - 1].spotID == currentPlayer &&
                    board[row - 2, column - 2].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row - 1, column - 1].spotID == currentPlayer &&
                    board[row + 1, column + 1].spotID == currentPlayer &&
                    board[row + 2, column + 2].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row + 1, column - 1].spotID == currentPlayer &&
                    board[row - 1, column + 1].spotID == currentPlayer &&
                    board[row - 2, column + 2].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                if (playerHasWon == false)
                {
                    if (board[row, column].spotID == currentPlayer &&
                    board[row - 1, column + 1].spotID == currentPlayer &&
                    board[row + 1, column - 1].spotID == currentPlayer &&
                    board[row + 2, column - 2].spotID == currentPlayer)
                    {

                        if (mainWindow.currentPlayer.PlayerID == 1)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        else if (mainWindow.currentPlayer.PlayerID == 2)
                        {
                            if (mainWindow.aiIsBlocking == false)
                            {
                                gameOverWindow.playerwintxt.Text = (name + " wins!");
                                mainWindow.currentPlayer.numOfWins++;
                                mainWindow.DisableBoard();
                                gameOverWindow.ShowDialog();
                                if (checkIfPlayerExists(mainWindow.currentPlayer.PlayerName, true) == true)
                                {
                                    top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Winners.txt", mainWindow.currentPlayer.PlayerName);
                                }
                                else
                                {
                                    top10ScoreWindow.playerWinScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                }
                                if (mainWindow.currentPlayer == mainWindow.p1)
                                {
                                    mainWindow.p2.numOfLosses++;
                                    if (checkIfPlayerExists(mainWindow.p2.PlayerName, false) == true)
                                    {
                                        top10ScoreWindow.fileLineRemaker(mainWindow.currentPlayer.PlayerName + " losses: " + mainWindow.currentPlayer.numOfLosses.ToString() + " wins: " + mainWindow.currentPlayer.numOfWins.ToString() + " draws: " + mainWindow.currentPlayer.numOfDraws.ToString(), "Top10Losers.txt", mainWindow.currentPlayer.PlayerName);
                                    }
                                    else
                                    {
                                        top10ScoreWindow.playerLoseScore(mainWindow.currentPlayer.PlayerName, mainWindow.currentPlayer.numOfWins, mainWindow.currentPlayer.numOfLosses, mainWindow.currentPlayer.numOfDraws);
                                    }
                                }
                            }
                        }
                        playerHasWon = true;

                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            return playerHasWon;
        }


        //AI checks to see if two chips are in a row for both players
        public hasTwo checkForTwoInARow(Token spot, int currentPlayer, Token[,] board)
        {
            if (mainWindow.numOfMoves > 1)
            {

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row - 1, spot.column].spotID == currentPlayer)
                    {
                        return hasTwo.hasTwoDown;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row, spot.column + 1].spotID == currentPlayer)
                    {
                        return hasTwo.hasTwoLeft;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row, spot.column - 1].spotID == currentPlayer)
                    {
                        return hasTwo.hasTwoRight;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row + 1, spot.column - 1].spotID == currentPlayer)
                    {

                        return hasTwo.hasTwoUpRightDiagonal;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row + 1, spot.column + 1].spotID == currentPlayer)
                    {

                        return hasTwo.hasTwoUpLeftDiagonal;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row - 1, spot.column - 1].spotID == currentPlayer)
                    {

                        return hasTwo.hasTwoDownRightDiagonal;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                try
                {
                    if (board[spot.row, spot.column].spotID == currentPlayer &&
                        board[spot.row - 1, spot.column + 1].spotID == currentPlayer)
                    {

                        return hasTwo.hasTwoDownLeftDiagnoal;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

                return hasTwo.hasNone;
            }
            return hasTwo.hasNone;
        }

        //Method to check for a draw
        public bool CheckForDraw()
        {
            bool boardIsFull = false;

            foreach (Token spot in board)
            {
                if (spot.spotID == 0)
                {
                    boardIsFull = false;
                    break;
                }
                else
                {
                    boardIsFull = true;
                    mainWindow.p1.numOfDraws++;
                    mainWindow.p2.numOfDraws++;
                }
            }

            return boardIsFull;
        }

        private bool checkIfPlayerExists(string nameToSearch, bool ischeckingWin)
        {
            bool hasFound = false;
            StreamReader inputFile;

            if (ischeckingWin == true)
            {
                inputFile = File.OpenText("Top10Winners.txt");
            }
            else
            {
                inputFile = File.OpenText("Top10Losers.txt");
            }


            while(!inputFile.EndOfStream)
            {
               string lineToCheck = inputFile.ReadLine();

                if(lineToCheck.Contains(nameToSearch))
                {
                    hasFound = true;
                    inputFile.Close();
                    return hasFound;
                }
                else
                {
                    hasFound = false;
                    inputFile.Close();
                    return hasFound;
                }
            }
            inputFile.Close();
            return hasFound;
        }
    }
}

