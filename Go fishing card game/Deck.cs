using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    internal class Deck
    {
        private List<Card> cards = new();
        private Random random = new Random();
        private sealed class FactoryToken { }
        private static readonly FactoryToken token = new();

        private Deck(FactoryToken _) { }

        public static Deck CreateFullDeck()
        {
            var deck = new Deck(token);
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    deck.Add(new Card((Suits)suit, (Values)value));
            return deck;
        }

        public static Deck CreateEmptyDeck()
        {
            return new Deck(token); 
        }

        public int Count { get { return cards.Count; } }
        public void Add(Card cardToAdd) { cards.Add(cardToAdd); }
        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            return CardToDeal;
        }

        public Card Deal()
        {
            return Deal(0);
        }

        public void Shuffle()
        {
            List<Card> NewCards = new List<Card>();
            while (cards.Count > 0)
            {
                int CardToMove = random.Next(cards.Count);
                NewCards.Add(cards[CardToMove]);
                cards.RemoveAt(CardToMove);
            }
            cards = NewCards;
        }

        public IEnumerable<string> GetCardNames()
        {
            string[] CardNames = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
                CardNames[i] = cards[i].Name;
            return CardNames;
        }

        public Card Peek(int cardNumber)
        {
            return cards[cardNumber];
        }



        public bool ContainsValue(Values value)
        {
            foreach (Card card in cards)
                if (card.Value == value)
                    return true;
            return false;
        }

        public Deck PullOutValues(Values value)
        {
            Deck deckToReturn = CreateEmptyDeck();
            for (int i = cards.Count - 1; i >= 0; i--)
                if (cards[i].Value == value)
                    deckToReturn.Add(Deal(i));
            return deckToReturn;
        }

        public bool HasBook(Values value)
        {
            int NumberOfCards = 0;
            foreach (Card card in cards)
                if (card.Value == value)
                    NumberOfCards++;
            if (NumberOfCards == 4)
                return true;
            else
                return false;
        }
        public void SortByValue()
        {
            cards.Sort(new CardComparer_byValue());
        }
    }
}
