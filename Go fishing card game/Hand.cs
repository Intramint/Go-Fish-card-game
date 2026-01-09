using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Go_fishing_card_game
{
    public class Hand : IEnumerable<Card>
    {
        public int Count { get { return cards.Count; } }
        public List<Card> cards = new();

        public void Add(Card card)
        {
            cards.Add(card);
        }
        public void Add(IEnumerable<Card> cardsToAdd)
        {
            foreach (Card card in cardsToAdd) 
                cards.Add(card);
        }

        public void Remove(IEnumerable<Card> cardsToRemove)
        {
            foreach (Card card in cardsToRemove)
                cards.Remove(card);
        }

        public bool IsEmpty()
        {
            if (cards.Count == 0)
                return true;
            return false;
        }

        public bool TryGetBook(CardValues cardValue)
        {
            int NumberOfCards = 0;
            foreach (Card card in cards)
            {
                if (card.Value == cardValue)
                {
                    NumberOfCards++;
                    if (NumberOfCards == 4)
                        return true;
                }
                else
                    NumberOfCards = 0;     
            }
            return false;
        }



        private IEnumerable PullOutValues(CardValues value)
        {
            Deck deckToReturn = new();
            for (int i = cards.Count - 1; i >= 0; i--)
                if (cards[i].Value == value)
                    deckToReturn.Add(Deal(i));
            return deckToReturn;
        }

        public IEnumerable<string> GetCardNames()
        {
            string[] CardNames = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
                CardNames[i] = cards[i].Name;
            return CardNames;
        }

        public bool HasValue(CardValues value)
        {
            foreach (var card in this)
                if (card.Value == value)
                    return true;
            return false;
        }

        public void SortByValue()
        {
            cards.Sort(new CardComparer_byValue());
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
