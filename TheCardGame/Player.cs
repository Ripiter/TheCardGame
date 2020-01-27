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
        public virtual string TakeCard(Player player, int cardPlace)
        {
            Card card = player.PlayersCards[cardPlace];

            player.PlayersCards.Remove(card);
            int rndPos = new Random(DateTime.Now.Millisecond).Next(0, PlayersCards.Count);
            this.PlayersCards.Insert(rndPos, card);

            return RemovePairs(card);
        }


        public string RemovePairs(Card cardFromPlayer)
        {
            string temp = string.Empty;
            foreach (Card card in PlayersCards.ToList())
            {
                if (card.cardNumber == cardFromPlayer.cardNumber && card.cardType != cardFromPlayer.cardType)
                {
                    temp += "Match " + card.cardNumber + " " + card.cardType + "\n" +
                            "with " + cardFromPlayer.cardNumber + " " + cardFromPlayer.cardType;
                    
                    PlayersCards.Remove(card);
                    PlayersCards.Remove(cardFromPlayer);
                }
            }
            return temp;
        }


        public virtual string RemoveAllPairs()
        {
            string temp = string.Empty;
            foreach (Card nCard in PlayersCards.ToList())
            {
                foreach (Card card in PlayersCards.ToList())
                {
                    if (card.cardNumber == nCard.cardNumber && card.cardType != nCard.cardType)
                    {
                        temp += "Card from " + this.Name + " removed " + card.cardNumber + " " + card.cardType + "\n" +
                                "Card from " + this.Name + " removed " + nCard.cardNumber + " " + nCard.cardType + "\n\n";
                     
                        //removes pair
                        PlayersCards.Remove(nCard);
                        PlayersCards.Remove(card);
                    }
                }
            }
            return temp;
        }
    }
}

