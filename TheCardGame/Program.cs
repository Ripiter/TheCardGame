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
                //new Human("Peter"),
                //new Human("Rene")
                //new Human("Kenneth"),
                //new Human("Emil"),
                new Computer("Computer"),
                new Computer("Computer2")
            };


            game.Start();
            game.Shufle(players);

            RemoveDuplicates();

            //PrintAllPlayersCards();
            GameMenu();





            Console.ReadKey();
        }

        /// <summary>
        /// Removes all duplicates from all the players at the start of the game
        /// </summary>
        static void RemoveDuplicates()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].RemoveAllPairs();
            }
        }

        static void GameMenu()
        {
            bool gameOver = false;
            int nextPlayer = 1;
            while (gameOver == false)
            {
                Console.Clear();
                for (int i = 0; i < players.Count; i++)
                {
                    if (CheckForLooser() == true)
                    {
                        gameOver = true;
                        continue;
                    }

                    ShowPlayerCardAmount();

                    if (nextPlayer >= players.Count)
                        nextPlayer = 0;
                    
                    if (players[i].GetType() == typeof(Human))
                    {
                        int cardPlace = UserInput(players[i], players[nextPlayer]);
                        Console.WriteLine(players[i].TakeCard(players[nextPlayer], cardPlace));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(players[i].Name + " picked card from " + players[nextPlayer].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        int testRnd = new Random().Next(1, players[nextPlayer].PlayersCards.Count);
                        Console.WriteLine(players[i].TakeCard(players[nextPlayer], testRnd));
                    }
                }
                nextPlayer++;
            }
        }

        static int UserInput(Player curPlayer, Player nextPlayer)
        {
            int retValue = 0;
            try
            {
                Console.WriteLine(curPlayer + " pick card from " + nextPlayer.Name);
                Console.WriteLine("Card from 1 to " + nextPlayer.PlayersCards.Count);
                Console.Write("I pick: ");
                retValue = int.Parse(Console.ReadLine());
            }
            catch
            {
                UserInput(curPlayer, nextPlayer);
            }

            return retValue;
        }

        static bool CheckForLooser()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].PlayersCards.Count == 0)
                {
                    players.Remove(players[i]);
                }
            }

            if (players.Count == 1)
                return true;

            return false;
        }

        static void ShowPlayerCardAmount()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int j = 0; j < players.Count; j++)
            {
                Console.WriteLine(players[j].Name + " has " + players[j].PlayersCards.Count + " card(s)");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        // Debug purposes
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
