using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace Go_fishing_card_game
{
    internal class Game
    {
        private List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox gameProgressTextBox;

        public Game(string playerName, IEnumerable<string> opponentNames, TextBox gameProgressTextBox)
        {
            Random random = new Random();
            this.gameProgressTextBox = gameProgressTextBox;
            players = new List<Player>();
            players.Add(new Player(playerName, random, gameProgressTextBox));
            foreach (string player in opponentNames)
                players.Add(new Player(player, random, gameProgressTextBox));
            books = new Dictionary<Values, Player>();
            stock = new Deck();
            Deal();
            players[0].SortHand();
        }

        private void Deal() {
            stock.Shuffle();
            foreach (Player player in players)
            {
                for (int i = 0; i < 5; i++)
                    player.TakeCard(stock.Deal());
                PullOutBooks(player);
            }
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {
           
            for (int i = 1; i < players.Count; i++)
            {
                if (i == 0) {
                    players[i].AskForACard(players, i, stock, (Values)selectedPlayerCard);
                }
                else
                    players[i].AskForACard(players, i, stock);

               players[i].AskForACard(players, i, stock);
                if (PullOutBooks(players[i]))
                {
                    int drawThatMany = Math.Min(5, stock.Count);
                    for (int _ = 0; _ < drawThatMany; _++)
                        players[i].TakeCard(stock.Deal());
                }
            }

            players[0].SortHand();
            if (stock.Count == 0)
            {
                gameProgressTextBox.Text += "Talia jest pusta. Koniec gry!\r\n";
                return true;
            }
            return false;


        }

        public bool PullOutBooks(Player player)
        {
            foreach (Values value in player.PullOutBooks()) {
                books.Add(value, player);
            }
            if (player.CardCount == 0)
                return true;
            return false;
        }

        public string DescribeBooks()
        {
            string description = "";
            foreach (Values cardValue in books.Keys)
            {
                description += $"{books[cardValue].Name} ma grupę {Card.Plural(cardValue, 0)},\r\n " ;
            }
            description += ".";
            return description;
        }

        public string GetWinnerName()
        {

        }

        public IEnumerable<string> GetPlayerCardNames()
        {
            return players[0].GetCardNames();
        }

        public string DescribePlayerHands()
        {
            string description = "";
            for (int i = 0; i < players.Count; i++)
            {
                description += players[i].Name + " ma " + players[i].CardCount;
                if (players[i].CardCount == 1)
                    description += " kartę.\r\n";
                else if (players[i].CardCount == 2 || players[i].CardCount == 3 || players[i].CardCount == 4)
                    description += " karty.\r\n";
                else
                    description += " kart.\r\n";
            }
            description += $"W talii pozostało kart: {stock.Count}\r\n";
            return description;
        }
    }
}
