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
        public bool IsEmpty
        {
            get
            {
                if (cards.Count == 0)
                    return true;
                return false;
            }
        }
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
       

        public IEnumerable<string> GetCardNames()
        {
            string[] CardNames = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
                CardNames[i] = cards[i].Name;
            return CardNames;
        }

        public bool FindMatchingValues(CardValues value, int cardsNeeded)
        {
            int counter = 0;
            foreach (var card in this)
                if (card.Value == value)
                {
                    counter++;
                    if (counter == cardsNeeded)
                        return true;
                }
            return false;
        }

        public CardValues? FindMatchingValues(int cardsNeeded)
        {
            if (Count < cardsNeeded)
                return null;

            List<Card> copiedHand = new();
            copiedHand.AddRange(cards);
            while (copiedHand.Any())
            {
                int counter = 0;
                var searchedValue = copiedHand[0].Value;
                copiedHand.RemoveAt(0);
                foreach (var card in copiedHand)
                {
                    if (card.Value == searchedValue)
                    {
                        counter++;
                        copiedHand.Remove(card);
                    }
                    if (counter == cardsNeeded)
                        return card.Value;
                }
            }
            return null;
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
