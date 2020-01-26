using System;
using System.Collections.Generic;

namespace TheCardGame
{
    class Program
    {
        //static Player[] players;
        static List<Player> players;
        static void Main(string[] args)
        {
            Game game = new Game();

            //players = new Player[]
            //{
            //    new Human("Peter"),
            //    new Human("Rene"),
            //    new Human("Kenneth"),
            //    new Computer("Computer")
            //};
            players = new List<Player>
            {
                //new Human("Peter"),
                //new Human("Rene"),
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
                for (int i = 0; i < players.Count; i++)
                {
                    if(CheckForLooser() == true)
                    {
                        gameOver = true;
                        continue;
                    }
                    //Debug
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int j = 0; j < players.Count; j++)
                    {
                        Console.WriteLine(players[j].Name + " has " + players[j].PlayersCards.Count + " cards");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    // End debug

                    if (nextPlayer >= players.Count)
                        nextPlayer = 0;

                    if (players[i].GetType() == typeof(Human))
                    {
                        Console.WriteLine(players[i].Name + " pick card from " + players[nextPlayer].Name);
                        Console.WriteLine("Card from 1 to " + players[nextPlayer].PlayersCards.Count);
                        Console.Write("I pick: ");
                        int cardPlace = int.Parse(Console.ReadLine());
                        players[i].TakeCard(players[nextPlayer], cardPlace);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(players[i].Name + " picked card from " + players[nextPlayer].Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        int testRnd = new Random().Next(1, players[nextPlayer].PlayersCards.Count);
                        players[i].TakeCard(players[nextPlayer], testRnd);
                    }
                    nextPlayer++;
                }
            }
        }

        static bool CheckForLooser()
        {
            bool isLooser = false;
            for(int i = 0; i < players.Count; i++)
            {
                if(players.Count > 1)
                {
                    if (players[i].PlayersCards.Count == 0)
                        players.Remove(players[i]);
                    isLooser = false;
                }
                else
                {
                    Console.WriteLine("And tonights Big Looser is " + players[i].Name);
                    isLooser = true;
                }
            }
            return isLooser;
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
