using System;
using System.Collections.Generic;

namespace TheCardGame
{
    class Program
    {
        static List<Player> players;
        static bool gameOver = false;
        static void Main(string[] args)
        {
            Game game = new Game();

            players = new List<Player>
            {
                new Human("Peter"),
                new Human("Rene")
                //new Human("Marc"),
                //new Human("Kenneth")
                //new Human("Emil"),
                //new Computer("Computer"),
                //new Computer("Computer2")
            };

            StartMenu();

            game.Start();
            game.Shufle(players);

            Console.WriteLine(RemoveDuplicates());

            //PrintAllPlayersCards();

            while (gameOver == false)
                GameMenu();


            Console.WriteLine("And tonight's biggest loser is " + players[0].Name);
            Console.ReadKey();
        }

        static void StartMenu()
        {
            Console.Clear();
            bool isDone = false;
            while(isDone == false)
            {
                Console.WriteLine("What you want to do" + 
                                  "Add for adding player" +
                                  "See to see all players in the game"+
                                  "Remove to remove player");
                string temp = Console.ReadLine().ToLower();
                switch (temp)
                {
                    case "add":
                        AddPlayerMenu();
                        break;
                    case "see":
                        PrintAllPlayers();
                        break;
                    case "remove":
                        Console.WriteLine("Insert name to remove");
                        string toRemove = Console.ReadLine();
                        RemovePlayer(toRemove);
                        break;
                    default:
                        isDone = true;
                        break;
                }
            }
        }

        static void AddPlayerMenu()
        {
            Console.WriteLine("Do you want to add a new player");
            Console.WriteLine("Add Bot, Add Human or Default");
            string userInput = Console.ReadLine().ToLower();

            switch (userInput)
            {
                case "add bot":
                    Console.WriteLine("Insert name");
                    Player bot = new Computer(Console.ReadLine());
                    AddPlayerToList(bot);
                    break;
                case "add human":
                    Console.WriteLine("Insert name");
                    Player human = new Human(Console.ReadLine());
                    AddPlayerToList(human);
                    break;
                case "Default":
                    break;
                default:
                    break;
            }
        }
        static void AddPlayerToList(Player player)
        {
            players.Add(player);
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
            int nextPlayer = 1;

            for (int i = 0; i < players.Count; i++)
            {
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
                if (CheckForLooser() == true)
                {
                    gameOver = true;
                    continue;
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

        /// <summary>
        /// Yoink / Validate user input
        /// </summary>
        /// <param name="curPlayer"></param>
        /// <param name="nextPlayer"></param>
        /// <returns></returns>
        static int UserInput(Player curPlayer, Player nextPlayer)
        {
            int retValue = 0;
            bool isValid = false;
            while(isValid == false)
            {
                Console.WriteLine("");
                Console.WriteLine(curPlayer.Name + " pick card from " + nextPlayer.Name);
                Console.WriteLine("Card from 1 to " + nextPlayer.PlayersCards.Count);
                Console.Write("I pick: ");

                // Check for user error in input
                if(int.TryParse(Console.ReadLine(), out retValue))
                {
                    if(retValue > 0 && retValue <= nextPlayer.PlayersCards.Count)
                        isValid = true;
                }
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


        static void PrintAllPlayers()
        {
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine(players[i].GetType() +" with name " + players[i].Name );
            }
        }

        static void RemovePlayer(string playerName)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Name == playerName)
                    players.Remove(players[i]);
            }
        }
    }
}
