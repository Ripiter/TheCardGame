using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCardGame
{
    class Human : Player
    {
        public Human(string name) : base(name)
        {

        }

        public override string TakeCard(Player player, int cardNumber)
        {
            return base.TakeCard(player, cardNumber);
        }

        //public override void RemovePairs(Card card)
        //{
        //    base.RemovePairs(card);
        //}


    }
}
