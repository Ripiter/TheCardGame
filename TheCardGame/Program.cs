using System;
using System.Collections.Generic;

namespace TheCardGame
{
    class Program
    {
        static List<Player> players;
        static void Main(string[] args)
        {
            Game game = new Game();

            players = new List<Player>
            {
                new Human("Peter"),
                new Human("Rene"),
                new Human("Marc"),
                //new Human("Kenneth")
                //new Human("Emil"),
                new Computer("Computer")
                //new Computer("Computer2")
            };


            game.Start();
            game.Shufle(players);

            Console.WriteLine(RemoveDuplicates());

            //PrintAllPlayersCards();
            GameMenu();
           

            Console.WriteLine("And tonights bigest loser is " + players[0].Name);
            Console.ReadKey();
        }

        /// <summary>
        /// Removes all duplicates from all the players at the start of the game
        /// </summary>
        static string RemoveDuplicates()
        {
            string temp = string.Empty;
            for (int i = 0; i < players.Count; i++)
            {
                temp += players[i].RemoveAllPairs();
            }
            return temp;
        }

        static void GameMenu()
        {
            bool gameOver = false;
            int nextPlayer = 1;
            while (gameOver == false)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (CheckForLooser() == true)
                    {
                        gameOver = true;
                        continue;
                    }

                    ShowPlayerCardAmount();

                    // Set next player in line
                    if (nextPlayer >= players.Count)
                        nextPlayer = 0;

                    if (players[i].GetType() == typeof(Human))
                    {
                        int cardPlace = UserInput(players[i], players[nextPlayer]);
                        PrintMatches(players[i].TakeCard(players[nextPlayer], cardPlace));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("");
                        Console.WriteLine(players[i].Name + " picked card from " + players[nextPlayer].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        
                        int testRnd = new Random().Next(0, players[nextPlayer].PlayersCards.Count);                        
                        PrintMatches(players[i].TakeCard(players[nextPlayer], testRnd));
                    }
                    nextPlayer++;
                }
            }
        }

        static void PrintMatches(string match)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (string.IsNullOrEmpty(match))
                Console.WriteLine("No matches \n");
            else
                Console.WriteLine(match + "\n");

            Console.ForegroundColor = ConsoleColor.White;
        }

        static int UserInput(Player curPlayer, Player nextPlayer)
        {
            int retValue = 0;
            try
            {
                Console.WriteLine("");
                Console.WriteLine(curPlayer.Name + " pick card from " + nextPlayer.Name);
                Console.WriteLine("Card from 1 to " + nextPlayer.PlayersCards.Count);
                Console.Write("I pick: ");
                retValue = int.Parse(Console.ReadLine());

                // Check for user error in input
                if (retValue <= nextPlayer.PlayersCards.Count && retValue > 0)
                    return retValue;
                else
                    return UserInput(curPlayer, nextPlayer);
            }
            catch
            {
                return UserInput(curPlayer, nextPlayer);
            }

            return retValue;
        }


        /// <summary>
        /// Removes finished players and check if there is 1 player left
        /// </summary>
        /// <returns></returns>
        static bool CheckForLooser()
        {
            RemoveFinishedPlayers();

            if (players.Count == 1)
                return true;

            return false;
        }

        static void RemoveFinishedPlayers()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].PlayersCards.Count == 0)
                {
                    players.Remove(players[i]);
                }
            }
        }

        static void ShowPlayerCardAmount()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int j = 0; j < players.Count; j++)
            {
                Console.WriteLine(players[j].Name + " has " + players[j].PlayersCards.Count + " card(s)");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints all players card
        /// <para>Debug Only</para>
        /// </summary>
        static void PrintAllPlayersCards()
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < players[i].PlayersCards.Count; j++)
                {
                    Console.WriteLine(players[i].Name + " " +
                                  players[i].PlayersCards[j].cardNumber + " " +
                                  players[i].PlayersCards[j].cardType + " " +
                                  players[i].PlayersCards[j].cardColor);
                }
            }
        }
    }
}
