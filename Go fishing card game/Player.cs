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
            Draw(drawPile, 5);      
        }

        public string Name { get; }
        public int CardCount { get { return hand.Count; } }
        public bool HasEmptyHand { get { return hand.IsEmpty(); } }
        public event EventHandler<MessageCreatedEventArgs>? MessageCreated;

        protected Hand hand { get; }
        public bool Draw(Deck sourceDeck, int cardCount = 1)
        {
            for (int i = 0; i < cardCount; i++) {
                hand.Add(sourceDeck.DealTop());
                if (sourceDeck.IsEmpty())
                    return false;
            }
            return true;
        }

        public void ReceiveCards(IEnumerable<Card> cards)
        {
            hand.Add(cards);
        }

        public IEnumerable<Card> GiveCardsWithValue(CardValues cardValue)
        {
            var cardsToGive = new List<Card>();
            foreach (Card card in hand)
            {
                if (card.Value == cardValue)
                    cardsToGive.Add(card);
            }
            return cardsToGive;
        }

        public bool HasValue(CardValues cardValue)
        {
            return hand.HasValue(cardValue);
        }
        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<string> GetCardNames()
        {
            return hand.GetCardNames();
        }

        public bool HasBook(CardValues cardValue) 
        {
            return hand.HasBook(cardValue);  //game.cs should register the new book, tell player to discard it and to draw 5 if hand is empty
        }

  
        public bool DiscardBook(CardValues cardValue)
        {
            List<Card> cardsToDiscard = new();
            foreach (Card card in hand)
            {
                if (card.Value == cardValue)
                    cardsToDiscard.Add(card);
            }
            if (cardsToDiscard.Count != 4)
                throw new InvalidOperationException($"Trying to score a book that has {cardsToDiscard.Count}: {cardsToDiscard}");
            hand.Remove(cardsToDiscard);
            if (hand.IsEmpty())
                return false;
            return true;
        }

        public bool AskForACard(List<Player> players, CardValues cardValue)
        {
            OnMessageCreated(new MessageCreatedEventArgs($"{Name} pyta, czy ktoś ma {Card.Plural(cardValue, 1)}"));
            bool cardAdded = false;

            foreach (Player opponent in players) //move logic to game.cs. leave the brainless actions here
            {
                if (opponent == this)
                    continue;
                if (opponent.HasValue(cardValue))
                {
                    cardAdded = true;
                    ReceiveCards(opponent.GiveCardsWithValue(cardValue));
                }
            }
            return cardAdded;
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
