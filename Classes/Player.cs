using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Connect4Game
{
    public class Player
    {
        //fields for the player cl
        public int PlayerID;

        public string PlayerName;

        public int numOfWins = 0;

        public int numOfLosses = 0;

        public int numOfDraws = 0;

        public bool isBot;

        public BitmapImage playerImg;

        //method for ai players to find the best spot in certain situations
        public void findBestSpot(int row, int column,Token[,] board,GameBoard gb) 
        {
            board[row, column].spotID = PlayerID;

            if(gb.checkForWin(row,column,PlayerID,board,PlayerName) == true)
            {

                board[row,column].spotID = 0;
            }
            else
            {

                board[row,column].spotID = 0;
            }
        }
    }
}
