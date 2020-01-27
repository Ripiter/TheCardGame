﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCardGame
{
    class Game
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        List<Card> cards = new List<Card>();
        private List<Player> players;

        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public Game()
        {
            Players = new List<Player>
            {
                //new Human("Peter"),
                //new Human("Rene"),
                //new Human("Marc")
                new Computer("C1"),
                new Computer("C2"),
                new Computer("C3"),
                new Computer("C4"),
                new Computer("C5"),
                new Computer("C6"),
                new Computer("C7"),
                new Computer("C8"),
                new Computer("C9"),
                new Computer("C10"),
                new Computer("C11"),
                new Computer("C12"),
                new Computer("C13")
            };
        }

        public void Start()
        {
            int amountOfUniqueCards = Enum.GetNames(typeof(CardNumber)).Length;
            
            bool blackPerExists = false;
            for (int i = 0; i < amountOfUniqueCards; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    // i is Value
                    // j is Type
                    Card card = new Card(i, j, GetColor(j));

                    if (card.cardNumber == CardNumber.BlackPer)
                    {
                        if(blackPerExists == false)
                            cards.Add(card);
                        blackPerExists = true;
                    }
                    else
                        cards.Add(card);
                }
            }
        }

        /// <summary>
        /// Deals all the cards to all players
        /// </summary>
        /// <param name="players"></param>
        public void Shufle()
        {
            RandomizeCards();
            int playerCount = Players.Count;
            foreach (Card card in cards)
            {
                if (playerCount == players.Count)
                    playerCount = 0;

                int rndPlace = rnd.Next(0, Players[playerCount].PlayersCards.Count);
                Players[playerCount].PlayersCards.Insert(rndPlace, card);
                playerCount++;
            }
        }

        /// <summary>
        /// Randomizes cards in the card list
        /// </summary>
        void RandomizeCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card temp = cards[i];
                int randomIndex = rnd.Next(i, cards.Count);
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Get color that fits with the type
        /// <para>Diamonds are red spades are black and so on</para>
        /// </summary>
        /// <param name="colorId"></param>
        /// <returns></returns>
        private int GetColor(int colorId)
        {
            int colorValue = 0;

            switch ((CardType)colorId)
            {
                case CardType.Diamonds:
                    colorValue = 0;
                    break;
                case CardType.Hearts:
                    colorValue = 0;
                    break;
                case CardType.Spades:
                    colorValue = 1;
                    break;
                case CardType.Clubs:
                    colorValue = 1;
                    break;
                default:
                    break;
            }
            return colorValue;
        }

        /// <summary>
        /// Removes all duplicates from all the players at the start of the game
        /// </summary>
        public string RemoveDuplicates()
        {
            string temp = string.Empty;
            for (int i = 0; i < Players.Count; i++)
            {
                temp += Players[i].RemoveAllPairs();
            }
            return temp;
        }
    }
}
