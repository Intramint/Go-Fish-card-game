using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows.Forms;

namespace Go_fishing_card_game
{
    internal class Player
    {
        public int CardCount { get { return cardsInHand.Count; }  }       
        private string name;
        public string Name { get { return name; } }
        private Random random;
        private Deck cardsInHand;
        private TextBox gameProgressTextBox;

        public Player(String name, Random random, TextBox gameProgressTextBox)
        {
            this.name = name;
            this.random = random;
            this.gameProgressTextBox = gameProgressTextBox;
            gameProgressTextBox.Text += name + " dołączył do gry\r\n";
        }

        public IEnumerable<string> GetCardNames() { return cardsInHand.GetCardNames(); }
        public void TakeCard(Card card) { cardsInHand.Add(card); }
        public Card Peek(int cardNumber) { return cardsInHand.Peek(cardNumber); }
        public void SortHand() { cardsInHand.SortByValue(); }

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
        public Values GetRandomCardValue()
        {
            Random random = new Random();
            return (Values)random.Next((int)Values.Ace, (int)Values.King + 1);
        }
        public Deck stolenCards(Values value)
        {
            Deck deckToReturn = cardsInHand.PullOutValues(value);
            int howMany = deckToReturn.Count;
            gameProgressTextBox.Text += $"{name} ma {howMany} {Card.Plural(value, howMany)}";
            return deckToReturn;
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            int value = random.Next(players[myIndex].CardCount);
            AskForACard(players, myIndex, stock, (Values)value);
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock, Values value)
        {
            gameProgressTextBox.Text += $"{name} pyta, czy ktoś ma {Card.Plural(value, 1)}";
            List<Player> playersWithoutMe = players;
            playersWithoutMe.RemoveAt(myIndex);
            bool anyCardsAdded = false;

            foreach (Player player in playersWithoutMe)
            {
                Deck cardsToAdd = stolenCards(value);
                int cardsAddedNum = cardsToAdd.Count;
                for (int i = 0; i < cardsAddedNum; i++)
                {
                    anyCardsAdded = true;
                    TakeCard(Peek(i));
                }
            }
            if (!anyCardsAdded)
            {
                TakeCard(stock.Deal());
                gameProgressTextBox.Text += $"{name} pobrał kartę z talii";
            }
        }
    }
}
