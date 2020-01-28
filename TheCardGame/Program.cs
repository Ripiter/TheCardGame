using System;

namespace TheCardGame
{
    class Program
    {
        static Game game;
        static bool gameOver = false;
        static void Main(string[] args)
        {
            game = new Game();
            StartMenu();

            game.Start();
            game.Shufle();

            Console.WriteLine(game.RemoveDuplicates());

            //PrintAllPlayersCards();

            while (gameOver == false)
                GameMenu();


            Console.WriteLine("And tonight's biggest loser is " + game.Players[0].Name);
            Console.ReadKey();
        }

        static void StartMenu()
        {
            bool isDone = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("On wrong input you will proccede to the game");
            Console.ForegroundColor = ConsoleColor.White;
            while (isDone == false)
            {
                Console.WriteLine("What you want to do \n" +
                                  "Add for adding player \n" +
                                  "See to see all players in the game \n" +
                                  "Remove to remove player \n" +
                                  "start or done when u are done");
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
                        game.RemovePlayer(toRemove);
                        break;
                    case "done":
                        isDone = true;
                        break;
                    case "start":
                        isDone = true;
                        break;
                    default:
                        isDone = true;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
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
            game.Players.Add(player);
            Console.WriteLine("Player Added");
        }

        static void GameMenu()
        {
            int nextPlayer = 1;

            for (int i = 0; i < game.Players.Count; i++)
            {
                //ShowPlayerCardAmount();

                // Set next player in line
                if (nextPlayer >= game.Players.Count)
                    nextPlayer = 0;

                if (game.Players[i].GetType() == typeof(Human))
                {
                    int cardPlace = UserInput(game.Players[i], game.Players[nextPlayer]);
                    PrintMatches(game.Players[i].TakeCard(game.Players[nextPlayer], cardPlace));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("");
                    //Console.WriteLine(game.Players[i].Name + " picked card from " + game.Players[nextPlayer].Name);
                    Console.ForegroundColor = ConsoleColor.White;

                    int testRnd = new Random().Next(0, game.Players[nextPlayer].PlayersCards.Count);
                    PrintMatches(game.Players[i].TakeCard(game.Players[nextPlayer], testRnd));
                }
                nextPlayer++;
                if (game.CheckForLooser() == true)
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
            while (isValid == false)
            {
                Console.WriteLine("");
                Console.WriteLine(curPlayer.Name + " pick card from " + nextPlayer.Name);
                Console.WriteLine("Card from 1 to " + nextPlayer.PlayersCards.Count);
                Console.Write("I pick: ");

                // Check for user error in input
                if (int.TryParse(Console.ReadLine(), out retValue))
                {
                    if (retValue > 0 && retValue <= nextPlayer.PlayersCards.Count)
                        isValid = true;
                }
            }
            return retValue;
        }

        static void ShowPlayerCardAmount()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(game.ShowPlayerCardAmount());
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints all players card
        /// <para>Debug Only</para>
        /// </summary>
        static void PrintAllPlayersCards()
        {
            for (int i = 0; i < game.Players.Count; i++)
            {
                for (int j = 0; j < game.Players[i].PlayersCards.Count; j++)
                {
                    Console.WriteLine(game.Players[i].Name + " " +
                                  game.Players[i].PlayersCards[j].Number + " " +
                                  game.Players[i].PlayersCards[j].Type + " " +
                                  game.Players[i].PlayersCards[j].Color);
                }
            }
        }

        static void PrintAllPlayers()
        {
            Console.WriteLine(game.PrintAllPlayers());
        }
    }
}
