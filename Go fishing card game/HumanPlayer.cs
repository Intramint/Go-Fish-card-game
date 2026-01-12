using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Go_fishing_card_game
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, Deck drawPile) : base(name, drawPile) { }

        private CardValues? chosenCardValue;

        public void SetChosenCardValue(int selectedIndex)
        {
            chosenCardValue = hand.cards[selectedIndex].Value;
        }
        public override CardValues GetChosenCardValue()
        {
            if (chosenCardValue == null)
                throw new InvalidOperationException("Card not chosen");
            return (CardValues)chosenCardValue;
        }

        public Card Peek (int index)
        {
            return hand.cards[index];
        }
        public void SortHand()
        {
            hand.SortByValue();
        }

    }
}
