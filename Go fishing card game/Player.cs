using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
//using System.Diagnostics.CodeAnalysis;

namespace Go_fishing_card_game
{
    public abstract class Player
    {
        public Player(string name, Deck drawPile)
        {
            Name = name;
            hand = new();
        }

        public string Name { get; }
        public int CardCount { get { return hand.Count; } }
        public bool HasEmptyHand { get { return hand.IsEmpty; } }
        public event EventHandler<MessageCreatedEventArgs>? MessageCreated;

        protected Hand hand { get; }

        public void ReceiveCards(IEnumerable<Card> cards)
        {
            hand.Add(cards);
        }

        public void ReceiveCards(Card card)
        {
            hand.Add(card);
        }

        public IEnumerable<Card> GiveCardsWithValue(CardValues cardValue)
        {
            var cardsToGive = new List<Card>();
            foreach (Card card in hand)
            {
                if (card.Value == cardValue)
                    cardsToGive.Add(card);
            }
            hand.Remove(cardsToGive);
            return cardsToGive;
        }

        public bool HasValue(CardValues cardValue)
        {
            return hand.FindMatchingValues(cardValue, 1);
        }
        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<string> GetCardNames()
        {
            return hand.GetCardNames();
        }
        public bool FindMatchingValues(CardValues cardValue, int count)
        {
            return hand.FindMatchingValues(cardValue, count);
        }

        public CardValues? FindMatchingValues(int count)
        {
            return hand.FindMatchingValues(count);
        }


        public void RemoveCardsOfValue(CardValues cardValue)
        {
            List<Card> cardsToDiscard = new();
            foreach (Card card in hand)
            {
                if (card.Value == cardValue)
                    cardsToDiscard.Add(card);
            }
            if (cardsToDiscard.Count != 4)
                throw new InvalidOperationException($"Trying to score a book that has {cardsToDiscard.Count} cards: {cardsToDiscard}");
            hand.Remove(cardsToDiscard);
        }

        public bool AskForACard(Player opponent, CardValues cardValue)
        {
            //OnMessageCreated(new MessageCreatedEventArgs($"{Name} pyta, czy ktoś ma {Card.Plural(cardValue, 1)}"));
            {
                if (opponent.HasValue(cardValue))
                {
                    ReceiveCards(opponent.GiveCardsWithValue(cardValue));
                    return true;
                }
                return false;
            }
            //if (noCardsAdded) //move all this to Game.cs
            //{               
            //    OnMessageCreated(new MessageCreatedEventArgs($"{Name} pobrał kartę z talii"));
            //}
            //OnMessageCreated(new MessageCreatedEventArgs(""));//adds a new line
        }

        public abstract CardValues GetChosenCardValue();
        protected virtual void OnMessageCreated(MessageCreatedEventArgs e)
        {
            MessageCreated?.Invoke(this, e);
        }
    }
}
