using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCardGame
{
    public enum CardNumber
    {
        Two,
        Tree,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        BlackPer,
        Jack,
        Queen,
        King,
        Ace
    }
    public enum CardType
    {
        Spades,
        Diamonds,
        Hearts,
        Clubs
    }
    public enum CardColor
    {
        Red,
        Black
    }


    class Card
    {
        public CardNumber cardNumber;
        public CardType cardType;
        public CardColor cardColor;

        public Card(int number, int type, int color)
        {
            cardNumber = (CardNumber)number;
            cardType = (CardType)type;
            cardColor = (CardColor)color;
        }
        
    }
}
