using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCardGame
{
    class Computer : Player
    {
        public Computer(string name) : base(name)
        {

        }
        

        public override void RemovePairs(Card cardFromPlayer)
        {
            base.RemovePairs(cardFromPlayer);
        }

        public override void TakeCard(Player player, int cardNumber)
        {
            base.TakeCard(player, cardNumber);
        }

    }
}
