using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class LostCardsEventArgs : EventArgs
    {
        public string Name { get; }
        public int NumberOfThisCardInHand { get; }
        public Values CardValue { get; }
        public LostCardsEventArgs(string name, int numberOfThisCardInHand, Values cardValue ) 
        {
            Name = name;
            numberOfThisCardInHand = NumberOfThisCardInHand;
            CardValue = cardValue;
        }
    }
}
