using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class Deck
    {
        public Deck(Random random) 
        {
            FillDeck();
            Shuffle(random);
        }
        public int Count { get { return cards.Count; } }

        private List<Card> cards = new();


        public override string ToString()
        {
            return $"Cards in deck: {Count}";
        }

        public Card DealTop()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public bool IsEmpty()
        {
            if (cards.Count == 0)
                return true;
            return false;
        }

        private void FillDeck()
        {
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    cards.Add(new Card((Suits)suit, (CardValues)value));
        }
        private void Shuffle(Random random)
        {
            List<Card> NewCards = new();
            while (cards.Count > 0)
            {
                int CardToMove = random.Next(cards.Count);
                NewCards.Add(cards[CardToMove]);
                cards.RemoveAt(CardToMove);
            }
            cards = NewCards;
        }
    }
}
