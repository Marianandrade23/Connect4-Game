using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Top10ScoreWindow.xaml
    /// </summary>
    public partial class Top10ScoreWindow : Window
    {
        // private field instantiation, to have access to mainwindow controls. 
        private MainWindow mainWindow;

        public List<int> tempScores = new List<int>();

        public Top10ScoreWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            // Mainwindow instantiation.
            this.mainWindow = mainWindow;

        }

        //storage win and losses of players
        public void playerWinScore(string playerName, int numOfWins, int numOfLosses,int numOfDraws)
        {
            Random rand = new Random();

            StreamWriter outputFile;
            if (File.Exists("Top10Winners.txt"))
            {
                StreamWriter writer = File.AppendText("Top10Winners.txt");
                writer.WriteLine(playerName + " wins: " + numOfWins.ToString() + " losses: " + numOfLosses.ToString() + " draws: " + numOfDraws.ToString());
                writer.Close();
            }
            else
            {
                outputFile = File.CreateText("Top10Winners.txt");
                outputFile.WriteLine(playerName + " wins: " + numOfWins.ToString() + " losses: " + numOfLosses.ToString() + " draws: " + numOfDraws.ToString());
                outputFile.Close();
            }

        }

        public void playerLoseScore(string playerName, int numOfWins,int numOfLosses,int numOfDraws)
        {
            Random rand = new Random();

            StreamWriter outputFile;

            if (File.Exists("Top10Losers.txt"))
            {
                StreamWriter writer;
                writer = File.AppendText("Top10Losers.txt");
                writer.WriteLine(playerName + " wins: " + numOfWins.ToString() + " losses: " + numOfLosses.ToString() + " draws: " + numOfDraws.ToString());
                writer.Close();

            }
            else
            {
                outputFile = File.CreateText("Top10Losers.txt");
                outputFile.WriteLine(playerName + " wins: " + numOfWins.ToString() + " losses: " + numOfLosses.ToString() + " draws: " + numOfDraws.ToString());
                outputFile.Close();
            }

        }

        //Gets the wins and losses and displays them
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tempCount = 0;
            List<string> winningNames = new List<string>();

            StreamReader inputFile;


            inputFile = File.OpenText("Top10Winners.txt");

            while (!inputFile.EndOfStream)
            {
                winningNames.Add(inputFile.ReadLine());
            }

            var newList =
                from line in winningNames
                orderby int.Parse((Regex.Match(line, @"\d+").Value)) descending
                select line;


            scoreListBox.Items.Add("Top 10 Winners: ");

            foreach (string name in newList)
            {
                if (tempCount < 10)
                {
                    scoreListBox.Items.Add(name);
                    tempCount++;
                }
            }
            tempCount = 0;

            inputFile.Close();



            List<string> losingNames = new List<string>();

            StreamReader newInputFile;


            newInputFile = File.OpenText("Top10Losers.txt");

            while (!newInputFile.EndOfStream)
            {
                losingNames.Add(newInputFile.ReadLine());
            }

            var newList1 =
                from line in winningNames
                orderby int.Parse((Regex.Match(line, @"\d+").Value)) descending
                select line;


            scoreListBox.Items.Add("\nTop 10 Losers: ");

            foreach (string name in newList1)
            {
                if (tempCount < 10)
                {
                    scoreListBox.Items.Add(name);
                    tempCount++;
                }
            }


            newInputFile.Close();

        }

        public void fileLineRemaker(string newText, string fileName,string nameToSearch)
        {
            string[] fileLines = File.ReadAllLines(fileName);
            foreach(string line in fileLines)
            {
                if(line.Contains(nameToSearch))
                {
                    fileLines[line.IndexOf(nameToSearch)] = newText;
                }
            }
            File.WriteAllLines(fileName, fileLines);
        }

        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

