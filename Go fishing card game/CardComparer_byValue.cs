using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class CardComparer_byValue : IComparer<Card>
    {
        public int Compare(Card? x, Card? y)
        {
            if (x.Value == CardValues.Ace)
            {
                if (y.Value == CardValues.Ace)
                    return 0;
                return 1;
            }

            if (y.Value == CardValues.Ace)
            {
                return -1;
            }

            if (x.Value < y.Value) return -1;
            if (x.Value > y.Value) return 1;
            return 0;
        }
    }
}
