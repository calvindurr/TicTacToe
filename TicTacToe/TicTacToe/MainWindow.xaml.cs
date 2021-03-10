using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Defualt Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }



        #endregion

        #region Private Members 
        /// <summary>
        /// Holds the current results from the cells in a game
        /// </summary>
        private MarkType[] mResults;

        private bool mPlayer1Turn;
        private bool mGameEnded;
        #endregion
        /// <summary>
        /// Initialize a new game and clears all the values
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i=0; i<mResults.Length;i++)
            {
                mResults[i] = MarkType.Free;   
            }
            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            mGameEnded = false;
        }

        /// <summary>
        /// Handles the button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //Cast the sender to a button
            var button = (Button)sender;

            
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if(mResults[index] != MarkType.Free)
            {
                return;
            }
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";

            //Toggles the player turn instead of using multiple if/else statements
            mPlayer1Turn ^= true;

            if (mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            CheckForWinner();
 

        }

        private void CheckForWinner()
        {
   
            //Check for horizontal wins
            //Row 0
            if (mResults[0] != MarkType.Free && ((mResults[0] & mResults[1] & mResults[2]) == mResults[0]) )
            {
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //Row 1
            if (mResults[3] != MarkType.Free && ((mResults[3] & mResults[4] & mResults[5]) == mResults[3]))
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //Row 2
            if (mResults[6] != MarkType.Free && ((mResults[6] & mResults[7] & mResults[8]) == mResults[6]))
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //Vertical Wins
            //Col 0
            if (mResults[0] != MarkType.Free && ((mResults[0] & mResults[3] & mResults[6]) == mResults[0]))
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //Col 1
            if (mResults[1] != MarkType.Free && ((mResults[1] & mResults[4] & mResults[7]) == mResults[1]))
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //Col 2
            if (mResults[2] != MarkType.Free && ((mResults[2] & mResults[5] & mResults[8]) == mResults[2]))
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Diagonal Wins
            //Top Left
            if (mResults[0] != MarkType.Free && ((mResults[0] & mResults[4] & mResults[8]) == mResults[0]))
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //Top Right
            if (mResults[2] != MarkType.Free && ((mResults[2] & mResults[4] & mResults[6]) == mResults[2]))
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            //No winner check
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                { 
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
