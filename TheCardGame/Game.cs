using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCardGame
{
    class Game
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        public List<Card> cards = new List<Card>();

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

        public void Shufle(List<Player> players)
        {
            RandomizeCards();
            int playerCount = players.Count;
            foreach (Card card in cards)
            {
                if (playerCount == players.Count)
                    playerCount = 0;


                int rndPlace = rnd.Next(0, players[playerCount].PlayersCards.Count);
                players[playerCount].PlayersCards.Insert(rndPlace, card);
                playerCount++;
            }
        }

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
    }
}
