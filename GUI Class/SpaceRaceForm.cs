using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();
            Board.SetUpBoard();
            ResizeGUIGameBoard();
            SetUpGUIGameBoard();
            SetupPlayersDataGridView();
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            PrepareToPlay();
            resetButton.Enabled = false;
            rollDiceButton.Enabled = false;
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        //  ******************* Uncomment - Remove Block Comment below
        //                         once you've added the TableLayoutPanel to your form.
        //
        //       You will have to replace "tableLayoutPanel" by whatever (Name) you used.
        //
        //        Likewise with "playerDataGridView" by your DataGridView (Name)
        //  ******************************************************************************************


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard

        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {
            screenCol = 0;
            screenRow = 0;

            if (squareNum == 0 || squareNum == 15 || squareNum == 16 || squareNum == 31 || squareNum == 32 || squareNum == 47 || squareNum == 48)
            {
                screenCol = 0;
            }
            if (squareNum == 1 || squareNum == 14 || squareNum == 17 || squareNum == 30 || squareNum == 33 || squareNum == 46 || squareNum == 49)
            {
                screenCol = 1;
            }
            if (squareNum == 2 || squareNum == 13 || squareNum == 18 || squareNum == 29 || squareNum == 34 || squareNum == 45 || squareNum == 50)
            {
                screenCol = 2;
            }
            if (squareNum == 3 || squareNum == 12 || squareNum == 19 || squareNum == 28 || squareNum == 35 || squareNum == 44 || squareNum == 51)
            {
                screenCol = 3;
            }
            if (squareNum == 4 || squareNum == 11 || squareNum == 20 || squareNum == 27 || squareNum == 36 || squareNum == 43 || squareNum == 52)
            {
                screenCol = 4;
            }
            if (squareNum == 5 || squareNum == 10 || squareNum == 21 || squareNum == 26 || squareNum == 37 || squareNum == 42 || squareNum == 53)
            {
                screenCol = 5;
            }
            if (squareNum == 6 || squareNum == 9 || squareNum == 22 || squareNum == 25 || squareNum == 38 || squareNum == 41 || squareNum == 54)
            {
                screenCol = 6;
            }
            if (squareNum == 7 || squareNum == 8 || squareNum == 23 || squareNum == 24 || squareNum == 39 || squareNum == 40 || squareNum == 55)
            {
                screenCol = 7;
            }
            //Rows
            if (squareNum == 0 || squareNum == 1 || squareNum == 2 || squareNum == 3 || squareNum == 4 || squareNum == 5 || squareNum == 6 || squareNum == 7)
            {
                screenRow = 6;
            }
            if (squareNum == 15 || squareNum == 14 || squareNum == 13 || squareNum == 12 || squareNum == 11 || squareNum == 10 || squareNum == 9 || squareNum == 8)
            {
                screenRow = 5;
            }
            if (squareNum == 16 || squareNum == 17 || squareNum == 18 || squareNum == 19 || squareNum == 20 || squareNum == 21 || squareNum == 22 || squareNum == 23)
            {
                screenRow = 4;
            }
            if (squareNum == 31 || squareNum == 30 || squareNum == 29 || squareNum == 28 || squareNum == 27 || squareNum == 26 || squareNum == 25 || squareNum == 24)
            {
                screenRow = 3;
            }
            if (squareNum == 32 || squareNum == 33 || squareNum == 34 || squareNum == 35 || squareNum == 36 || squareNum == 37 || squareNum == 38 || squareNum == 39)
            {
                screenRow = 2;
            }
            if (squareNum == 47 || squareNum == 46 || squareNum == 45 || squareNum == 44 || squareNum == 43 || squareNum == 42 || squareNum == 41 || squareNum == 40)
            {
                screenRow = 1;
            }
            if (squareNum == 48 || squareNum == 49 || squareNum == 50 || squareNum == 51 || squareNum == 52 || squareNum == 53 || squareNum == 54 || squareNum == 55)
            {
                screenRow = 0;
            }

        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            int num = 0;
            //If something other than an int is entered it throws a Message Box
            num = int.Parse(numPlayersComboBox.SelectedItem.ToString());

            // Set the NumberOfPlayers in the SpaceRaceGame class to that number
            SpaceRaceGame.NumberOfPlayers = num;

        }//end DetermineNumberOfPlayers

        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlay()
        {
            // More code will be needed here to deal with restarting 
            // a game after the Reset button has been clicked. 
            //
            // Leave this method with the single statement 
            // until you can play a game through to the finish square
            // and you want to implement the Reset button event handler.
            //
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);

        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)
        {
            int screenRow;
            int screenCol;

            // Uncomment the following lines once you've added the tableLayoutPanel to your form. 
            //     and delete the "return null;" 
            //
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            // Code needs to be added here.
            int num = SpaceRaceGame.Players[playerNumber].Position;
            return num;

        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
            // Uncomment the following line once you've added the tableLayoutPanel to your form.
            tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            //       determine the square number of the player
            //       retrieve the SquareControl object with that square number
            //       using the typeOfGuiUpdate, update the appropriate element of 
            //          the ContainsPlayers array of the SquareControl object.
            //
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                int playerSquareNumber = GetSquareNumberOfPlayer(i);
                SquareControl squareNumber = SquareControlAt(playerSquareNumber);

                if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
                {
                    if (i <= SpaceRaceGame.NumberOfPlayers)
                    {
                        squareNumber.ContainsPlayers[i] = true;
                    }

                }
                else if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer)
                {
                    if (i <= SpaceRaceGame.NumberOfPlayers)
                    {
                        squareNumber.ContainsPlayers[i] = false;
                    }
                }
            }

            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        private void numPlayersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            numPlayersComboBox.Enabled = false;
            DetermineNumberOfPlayers();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            UpdatesPlayersDataGridView();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
        }
        bool singleStepMode;
        int playerNum = 0;

        private void yesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            singleStepMode = true;
            rollDiceButton.Enabled = true;
        }

        private void noRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            singleStepMode = false;
            rollDiceButton.Enabled = true;
        }

        private void rollDiceButton_Click(object sender, EventArgs e)
        {
            //Disable the groupbox
            singleStepGroupBox.Enabled = false;
            //String to store the winners, if any, after the Dice is clicked
            string winners = "The following player(s) finished the game\n\n\t";
            //Is there any winners in the game?
            bool hasWinners = false;
            //Disables exit button during a round
            exitButton.Enabled = false;
            //Sets the name column to ReadOnly so the user can't edit it once they've started the game
            playersDataGridView.Columns[1].ReadOnly = true;
            //Disable the reset button during a round
            resetButton.Enabled = false;
            //Disable the combobox
            numPlayersComboBox.Enabled = false;
            //Removes the players tokens from their current positions
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            //If single step mode is disabled it runs all players at once
            if (singleStepMode == false)
            {
                //Plays one round of the game
                SpaceRaceGame.PlayOneRound();
            }
            else if (singleStepMode == true)
            {
                singleStepMethod();
            }
            //Places the player's tokens back on screen on the respective squares
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            //Updates the datagrid with the new info gathered from the round played
            UpdatesPlayersDataGridView();
            //a loop to determine whether or not anyone has won the game
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                if (SpaceRaceGame.Players[i].AtFinish)
                {
                    //Disables the rollDice if winners are present
                    rollDiceButton.Enabled = false;
                    //Add the players name to the winners string
                    winners += SpaceRaceGame.Players[i].Name;
                    //Add a space inbetween each winner
                    winners += " ";
                    //Sets the hasWinners bool to true
                    hasWinners = true;
                }
            }
            //If the hasWinners bool is true is shows the messagebox with the winners
            if (hasWinners)
            {
                MessageBox.Show(winners);
            }
            //If all the players have no fuel a messagebox is prompted
            if (SpaceRaceGame.allPlayersNoFuel == true)
            {
                //Disable the rollDice button as game is over
                rollDiceButton.Enabled = false;
                //Show messagebox
                MessageBox.Show("Game Over. All players have ran out of fuel.");
                //Sets the bool back to it's default false incase the player restarts the game
                SpaceRaceGame.allPlayersNoFuel = false;
            }
            //Re-enables these buttons at the end of the round
            resetButton.Enabled = true;
            exitButton.Enabled = true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            SpaceRaceGame.allPlayersNoFuel = false;
            //Sets the name column so that it's once againe editable after reset
            playersDataGridView.Columns[1].ReadOnly = false;
            //Re-enable the combo box so player can select the amound of players they want
            numPlayersComboBox.Enabled = true;
            //Re-enable the groupbox for single step
            singleStepGroupBox.Enabled = true;
            //Unchecks the radio buttons
            yesRadioButton.Checked = false;
            noRadioButton.Checked = false;
            //Disables the roll dice button till a radio button is checked
            rollDiceButton.Enabled = false;
            //Remove tokens
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            //Set all the players back to the start of the game defaults
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                SpaceRaceGame.Players[i].RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                SpaceRaceGame.Players[i].Position = Board.START_SQUARE_NUMBER;
                SpaceRaceGame.Players[i].Location = Board.Squares[0];
            }
            //Update the datagrid to match the starting values
            UpdatesPlayersDataGridView();
            //Place the players tokens back on the spawn
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
        }

        private void singleStepMethod()
        {
            int counter = 0;
            //If the player has fuel then they play
            if (SpaceRaceGame.Players[playerNum].RocketFuel > 0)
            {
                SpaceRaceGame.Players[playerNum].Play(SpaceRaceGame.Die1, SpaceRaceGame.Die2);
            }
            //Checks to see if playerNum +1 would exceed the number of players in the game, if so it resets the index
            if (playerNum + 1 > SpaceRaceGame.NumberOfPlayers - 1)
            {
                playerNum = 0;
            }
            //if it doesn't exceed the index it is incremented
            else
            {
                playerNum++;
            }
            //If the next player in the index doesn't have fuel
            if(SpaceRaceGame.Players[playerNum].RocketFuel <= 0)
            {
                //Continues to loop till the next index of a player with fuel is found
                for (;;)
                {
                    if (playerNum + 1 > SpaceRaceGame.NumberOfPlayers - 1)
                    {
                        playerNum = 0;
                    }
                    else
                    {
                        playerNum++;
                    }
                    if (SpaceRaceGame.Players[playerNum].RocketFuel > 0)
                    { 
                        break;
                    }
                    //If the counter has iterated over the Player array twice and couldn't find a player with fuel it's assumed that
                    //no one has fuel left and the infinite loop breaks
                    else if (counter > SpaceRaceGame.NumberOfPlayers * 2)
                    {
                        counter = 0;
                        SpaceRaceGame.allPlayersNoFuel = true;
                        break;
                    }
                    counter++;
                }
            }
        }

    }
}// end class

