using System.Drawing;
using System.ComponentModel;
using Object_Classes;


namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;
   
        private static int numberOfPlayers = 6;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values
        
        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();

        //I added these get methods so I can access the die in my SpaceRaceForm.cs
        public static Die Die1
        {
            get
            {
                return die1;
            }
        }
        public static Die Die2
        {
            get
            {
                return die2;
            }
        }


        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers() 
        {
            Players.Clear();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player player = new Player(names[i]);
                player.RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                player.Position = Board.START_SQUARE_NUMBER;
                player.Location = Board.Squares[0];
                player.PlayerTokenColour = playerTokenColours[i];
                Players.Add(player);
            }
        }

        /// <summary>
        ///  Plays one round of a game
        /// </summary>
        /// 

        public static int tempNumPlayersWithNoFuel = 0;
        public static int numPlayersWithFuel;
        public static int actualNumPlayersWithNoFuel;
        public static bool allPlayersNoFuel = false;

        public static void PlayOneRound() 
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                //Onlys players with fuel can play
                if (Players[i].RocketFuel > 0)
                {
                    Players[i].Play(die1, die2);
                }
                //if the player has no fuel this int is incremented
                else
                {
                    //Adds one for every player in that round who didn't have fuel
                    tempNumPlayersWithNoFuel++;
                }
            }
            //Get the number of players with fuel from that round
            numPlayersWithFuel = numberOfPlayers - tempNumPlayersWithNoFuel;
            //Get the number of players without fuel that round
            actualNumPlayersWithNoFuel = numberOfPlayers - numPlayersWithFuel;
            //Reset this variable back to 0 to ensure it's ready for the next round to count again
            tempNumPlayersWithNoFuel = 0;

            //If the numPlayersWithNoFuel is the same as the number of players in the game the bool is set to true
            if (actualNumPlayersWithNoFuel == NumberOfPlayers)
            {
                allPlayersNoFuel = true;
            }
        }
    }//end SnakesAndLadders
}