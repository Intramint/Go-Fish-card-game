using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
//using System.Diagnostics.CodeAnalysis;

namespace Go_fishing_card_game
{
    internal class Player //make abstract and split to human and computer
    {
        public Player(string name, Deck drawPile)
        {
            this.Name = name;
            hand = new();
            TryDraw(drawPile, 5);
        }

        public string Name { get; private set; }
        public int CardCount { get { return hand.Count; } }
        public event EventHandler<MessageCreatedEventArgs>? MessageCreated;

        private Hand hand;
        public bool TryDraw(Deck sourceDeck, int cardCount = 1)
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

        public void SortHand()
        {
            hand.SortByValue(); 
        }

        public bool TryScoreBook(CardValues cardValue)
        {
            return hand.TryGetBook(cardValue);
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            int value = random.Next(players[myIndex].CardCount) + 1;
            AskForACard(players, myIndex, stock, (CardValues)value);
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock, CardValues value)
        {
            OnMessageCreated(new MessageCreatedEventArgs($"{Name} pyta, czy ktoś ma {Card.Plural(value, 1)}"));
            bool noCardsAdded = true;

            for (int i = 0; i < players.Count; i++)
            {
                if (i == myIndex)//change to use containValue
                    continue;
                Deck cardsToAdd = players[i].GiveCards(value);
                int cardsAddedNum = cardsToAdd.Count;
                for (int j = 0; j < cardsAddedNum; j++)
                {
                    noCardsAdded = false;
                    TakeCard(cardsToAdd.Peek(j));
                }
            }
            if (noCardsAdded)
            {
                ReceiveCard(stock.DealTop());
                OnMessageCreated(new MessageCreatedEventArgs($"{Name} pobrał kartę z talii"));
            }
            OnMessageCreated(new MessageCreatedEventArgs(""));//adds a new line
        }

        protected virtual void OnMessageCreated(MessageCreatedEventArgs e)
        {
            MessageCreated?.Invoke(this, e);
        }
    }
}
