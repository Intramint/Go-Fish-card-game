using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, Deck drawPile, Random random) : base(name, drawPile) 
        {
            this.random = random;
        }

        private readonly Random random;

        public override CardValues GetChosenCardValue()
        {
            return hand.cards[random.Next(CardCount + 1)].Value;
        }
    }
}
