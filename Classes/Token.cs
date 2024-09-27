using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;


namespace Connect4Game
{
    public class Token
    {
        //variables for the row and column
        public int row; 
        public int column;

        //variable for the current Id of each Token
        //this will help us determine whether this spot is currently empty or not
        public int spotID;

        //image for each Token
        public Image boardImg;

        //images for the different colored pieces
        public BitmapImage RedChip = new BitmapImage(new Uri("pack://application:,,,/Images/ChipRed.png"));
        public BitmapImage YellowChip = new BitmapImage(new Uri("pack://application:,,,/Images/ChipYellow.png"));
        public BitmapImage GreenChip = new BitmapImage(new Uri("pack://application:,,,/Images/ChipGreen.png"));
        public BitmapImage BlueChip = new BitmapImage(new Uri("pack://application:,,,/Images/ChipBlue.png"));
        public BitmapImage BlackChip = new BitmapImage(new Uri("pack://application:,,,/Images/ChipBlack.png"));

        // Constructor that assigns the rows and columns of board and token image. 
        public void FillTheTokens(int row_spot, int column_spot,Image img)
        {
            row = row_spot;
            column = column_spot;
            boardImg = img;
        }

        ////function to return the red token image
        //public BitmapImage MakeTokenRed()
        //{
        //    return RedChip;
        //}

        ////function to return the yellow token image
        //public BitmapImage MakeTokenYellow()
        //{
        //    return YellowChip; 
        //}

        public BitmapImage MakeTokenColor(int spotID)
        {
            if (spotID == 1)
            {
                return RedChip;
            }
            else if (spotID == 2)
            {
                return YellowChip;
            }
            else
            {
                return null;
            }
        }
    }
}
