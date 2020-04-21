using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        /// 

        //These 2 global variables will be used inside the PlayGame function
        public static int globalRoundCounter = 1;
        public static bool exitGame = false;

        static void Main(string[] args)
        {
            Console.WriteLine("\tWelcome to Space Race.\n");
            do
            {
                //Setup the board
                Board.SetUpBoard();
                //Displays the text "This game is for 2 to 6 players." and "How many players (2-6):"
                EnterPlayersText();
                //Tries to parse the players text input into the numPlayers variable
                int numPlayers = TestPlayerTextInput();
                //Sets the number of players to what the user inputted
                SpaceRaceGame.NumberOfPlayers = numPlayers;
                //Sets up the players giving them their names, positions, square and the amount of fuel they have
                SpaceRaceGame.SetUpPlayers();
                //Plays the game 
                PlayGame();
                //Code only gets to this line when someone has won the game
                Console.WriteLine("\n\n\tPress Enter key to continue ...\n\n\n\n\n");
                Console.ReadLine();
                PlayAgain();
            } while (exitGame == false);

            PressEnterToTerminate();

        }//end Main

        static void PlayGame()
        {
            //Setting up variables for this function
            bool gameOver = false;
            string finishedPlayers = "";
            bool gameOverBecauseFuel = false;


            //This do while loop will continue to run till a gameOver is triggered
            do
            {
                //Plays one round of the game
                SpaceRaceGame.PlayOneRound();

                //If all players run out of fuel
                if (SpaceRaceGame.allPlayersNoFuel == true)
                {
                    gameOverBecauseFuel = true;
                    SpaceRaceGame.allPlayersNoFuel = false;
                    break;
                }

                //Asks the user to press enter to play a around and waits for their input before proceeding
                Console.WriteLine("\n\nPress enter to play a round...");
                Console.ReadKey();

                //If it's round number one a string saying "First Round" will be displayed
                if (globalRoundCounter == 1)
                {
                    Console.WriteLine("\n\tFirst Round\n");
                }
                //If it isn't round number one a string saying "Next Round will appear"
                else
                {
                    Console.WriteLine("\n\tNext Round\n");
                }

                //loops through the amount of players in the game and prints their info to the console
                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                {
                    Console.WriteLine("\t{0} on square {1} with {2} yottawatt of power remaining", SpaceRaceGame.Players[i].Name, SpaceRaceGame.Players[i].Position, SpaceRaceGame.Players[i].RocketFuel);
                }

                //Runs a for loop to determine whether or noy anyone has won the game. If they have it places their names into the
                //finished players string
                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                {
                    if (SpaceRaceGame.Players[i].AtFinish == true)
                    {
                        finishedPlayers += SpaceRaceGame.Players[i].Name;
                    }
                }
                //If the finishedPlayers string had values added into it, the game is over because players have finished the game
                if (finishedPlayers != "")
                {
                    gameOver = true;
                }
                //Increments the globalRoundCounter to display the appropriate string next round
                globalRoundCounter++;
            } while (gameOver == false);

            //If the game is over because everyone ran out of fuel it prints this line
            if (gameOverBecauseFuel == true)
            {
                Console.WriteLine("\n\tGame Over. All players have ran out of fuel.");
                gameOverBecauseFuel = false;
            }
            else
            {
                //The code gets to this point when gameOver is true
                Console.WriteLine("\n\n\tThe following player(s) finished game");
                //Prints the names of the players who won the game
                Console.WriteLine("\n\t\t{0}\n\n", finishedPlayers);
                //Prints this string
                Console.WriteLine("\tIndividual players finished with the at the locations specified.");
                //Loops through all the players and prints their final positions for the specified winners won the game
                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                {
                    Console.WriteLine("\n\t\t{0} with {1} yattowatt of power at square {2}", SpaceRaceGame.Players[i].Name, SpaceRaceGame.Players[i].RocketFuel, SpaceRaceGame.Players[i].Position);
                }
            }
        }

        static void PlayAgain()
        {
            //Declaring variable for use in this function
            string input = "";
            //This while loop is infinate unless it breaks
            while (true)
            {
                //Asks the user if they want to play again
                Console.Write("\tPlay again? (Y or N): ");
                //Places their input into the input string variable
                input = Console.ReadLine();
                //If what they entered is n or N the while loop will exit
                if (input == "n" || input == "N")
                {
                    break;
                }
                //If what they entered is y or Y the while loop will exit
                else if (input == "y" || input == "Y")
                {
                    break;
                }
                //If what they entered didn't meet any of the if statement requirements it will declare their input as invalid
                //and restart the loop
                Console.WriteLine("\nError: invalid option entered.\n");
            }

            //If their input is n or N it will thank them for playing Space Race
            if (input == "n" || input == "N")
            {
                Console.WriteLine("\n\n\n\tThanks for playing Space Race");
                exitGame = true;
            }

            else if (input == "y" || input == "Y")
            {
                //Reset all the players variables back to default ready for a new game
                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                {
                    SpaceRaceGame.Players[i].RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                    SpaceRaceGame.Players[i].Position = Board.START_SQUARE_NUMBER;
                    SpaceRaceGame.Players[i].Location = Board.Squares[0];
                }
                SpaceRaceGame.Players.Clear();
                globalRoundCounter = 1;
            }
        }

        static void PressEnterToTerminate()
        {
            Console.WriteLine("\n\tPress Enter to terminate program ...");
            Console.ReadLine();
        }

        static void EnterPlayersText()
        {
            Console.WriteLine("\n\tThis game is for 2 to 6 players.");
            Console.Write("\tHow many players (2-6): ");
        }

        static int TestPlayerTextInput()
        {
            //Declaring variables to use inside the function
            string input;
            int numberOfPlayers;
            bool correctInput;

            do
            {
                //Places the user input into a string variable
                input = Console.ReadLine();
                //Tries to parse string variable into an int
                correctInput = int.TryParse(input, out numberOfPlayers);
                //If the parse failed or if it did parse and the players were more than the low and max thresholds this if statement runs
                if (!correctInput || numberOfPlayers > SpaceRaceGame.MAX_PLAYERS || numberOfPlayers < SpaceRaceGame.MIN_PLAYERS)
                {
                    //correctInput stays false so the do loop runs again 
                    correctInput = false;
                    //Tells the user that their input was invalid
                    Console.WriteLine("\nError: invalid number of players entered\n");
                    //Runs the function asking for them to enter a valid amount of players again
                    EnterPlayersText();
                }

            } while (!correctInput);
            //Only returns when correct input is true
            return numberOfPlayers;
        }
    }//end Console class
}
