using System;
using System.Collections.Generic;
using System.Linq;

namespace TheCardGame
{
    abstract class Player
    {
        List<Card> playersCard = new List<Card>();
        string name;

        public List<Card> PlayersCards
        {
            get
            {
                return this.playersCard;
            }
            set
            {
                this.playersCard = value;
            }
        }
        public string Name { get => name; set => name = value; }

        public Player(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Takes card from another player
        /// </summary>
        public virtual void TakeCard(Player player, int cardPlace)
        {
            Card card = player.PlayersCards[cardPlace - 1];

            player.PlayersCards.Remove(card);
            int rndPos = new Random(DateTime.Now.Millisecond).Next(0, PlayersCards.Count);
            this.PlayersCards.Insert(rndPos, card);

            RemovePairs(card);
        }


        public virtual void RemovePairs(Card cardFromPlayer)
        {
            foreach (Card card in PlayersCards.ToList())
            {
                if (card.cardNumber == cardFromPlayer.cardNumber && card.cardType != cardFromPlayer.cardType)
                {
                    // Debug
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("match with " + card.cardNumber + " " + card.cardType);
                    Console.WriteLine("match with " + cardFromPlayer.cardNumber + " " + cardFromPlayer.cardType);
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.White;
                    // End Debug
                    PlayersCards.Remove(card);
                    PlayersCards.Remove(cardFromPlayer);
                }
            }
        }

        //public virtual void RemoveAllPairs()
        //{
        //    for (int i = 0; i < PlayersCards.ToList().Count; i++)
        //    {
        //        foreach (Card card in PlayersCards.ToList())
        //        {
        //            if (card.cardNumber == PlayersCards[i].cardNumber && card.cardType != PlayersCards[i].cardType)
        //            {
        //                // Debug
        //                Console.WriteLine("Card from " + this.Name + " removed " + card.cardNumber + " " + card.cardType);
        //                Console.WriteLine("Card from " + this.Name + " removed " + PlayersCards[i].cardNumber + " " + PlayersCards[i].cardType + " \n");
        //                // End Debug

        //                //remove pair
        //                PlayersCards.Remove(PlayersCards[i]);
        //                PlayersCards.Remove(card);
        //            }
        //        }
        //    }
        //}

        public virtual void RemoveAllPairs()
        {
            foreach (Card nCard in PlayersCards.ToList())
            {
                foreach (Card card in PlayersCards.ToList())
                {
                    if (card.cardNumber == nCard.cardNumber && card.cardType != nCard.cardType)
                    {
                        // Debug
                        Console.WriteLine("Card from " + this.Name + " removed " + card.cardNumber + " " + card.cardType);
                        Console.WriteLine("Card from " + this.Name + " removed " + nCard.cardNumber + " " + nCard.cardType + " \n");
                        // End Debug

                        //remove pair
                        PlayersCards.Remove(nCard);
                        PlayersCards.Remove(card);
                    }
                }
            }
        }
    }
}

