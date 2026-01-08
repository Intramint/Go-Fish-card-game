using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace Go_fishing_card_game
{
    internal class Player
    {
        public string Name { get; private set; }
        private Random random;
        private Deck cardsInHand = Deck.CreateEmptyDeck();
        public int CardCount { get { return cardsInHand.Count; } }
        public event EventHandler<MessageCreatedEventArgs>? MessageCreated;
        public Player(string name, Random random)
        {
            
            this.Name = name;
            this.random = random;
        }
        
        public IEnumerable<string> GetCardNames() { return cardsInHand.GetCardNames(); }
        public void TakeCard(Card card) { cardsInHand.Add(card); }
        public Card Peek(int cardNumber) { return cardsInHand.Peek(cardNumber); }
        public void SortHand() { cardsInHand.SortByValue(); }

        protected virtual void OnMessageCreated(MessageCreatedEventArgs e) 
        {
            MessageCreated?.Invoke(this, e);
        }
        public IEnumerable<Values> PullOutBooks() {
            List<Values> books = new List<Values>();
            for (int i = 1; i <= 13; i++)
            {
                Values value = (Values)i;
                int howMany = 0;
                for (int card = 0; card < cardsInHand.Count; card++)
                    if (cardsInHand.Peek(card).Value == value)
                        howMany++;
                if (howMany == 4)
                {
                    books.Add(value);
                    for (int card = cardsInHand.Count - 1; card >= 0; card--)
                        cardsInHand.Deal(card);
                }
            }
            return books;
        }

        public Deck LoseCards(Values value)
        {
            Deck deckToReturn = cardsInHand.PullOutValues(value);
            int howMany = deckToReturn.Count;
            OnMessageCreated(new MessageCreatedEventArgs($"{Name} ma {howMany} {Card.Plural(value, howMany)}"));
            return deckToReturn;
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            int value = random.Next(players[myIndex].CardCount) + 1;
            AskForACard(players, myIndex, stock, (Values)value);
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock, Values value)
        {
            OnMessageCreated(new MessageCreatedEventArgs($"{Name} pyta, czy ktoś ma {Card.Plural(value, 1)}"));
            bool noCardsAdded = true;

            for (int i = 0; i < players.Count; i++)
            {
                if (i == myIndex)
                    continue;
                Deck cardsToAdd = players[i].LoseCards(value);
                int cardsAddedNum = cardsToAdd.Count;
                for (int j = 0; j < cardsAddedNum; j++)
                {
                    noCardsAdded = false;
                    TakeCard(cardsToAdd.Peek(j));
                }
            }
            if (noCardsAdded)
            {
                TakeCard(stock.Deal());
                OnMessageCreated(new MessageCreatedEventArgs($"{Name} pobrał kartę z talii"));
            }
            OnMessageCreated(new MessageCreatedEventArgs(""));//adds a new line
        }
    }
}
