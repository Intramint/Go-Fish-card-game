using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Go_fishing_card_game
{
    internal class Game
    {
        public List<Player> players;
        public Dictionary<Values, Player> Books { get; private set; }
        private Deck stock;
        private TextBox gameProgressTextBox;//move to form1
        public int PlayerCount { get; private set; }
        public int StockCardCount { get { return stock.Count; } }
        public event EventHandler<LostCardsEventArgs> LostCards;

        protected virtual void OnLostCards(LostCardsEventArgs e)
        {
            LostCards?.Invoke(this, e);
        }
        public void player_LostCards(object? sender, LostCardsEventArgs e)
        {
            OnLostCards(e);
        } 

        public Game(IEnumerable<string> playerNames)
        {
            Random random = new Random();
            players = new List<Player>();
            foreach (string playerName in playerNames)
            {
                Player player = new Player(playerName, random);
                players.Add(player);
                player.LostCards += player_LostCards;
            }
            PlayerCount = players.Count;
            HumanPlayer = players[0];
            Books = new Dictionary<Values, Player>();
            stock = Deck.CreateFullDeck();
            Deal();
            HumanPlayer.SortHand();
            
        }

        public Player HumanPlayer { get; private set; }

        private void Deal() {
            stock.Shuffle();
            foreach (Player player in players)
            {
                for (int i = 0; i < 5; i++)
                    player.TakeCard(stock.Deal());
                pullOutBooks(player);
            }
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {
           
            for (int i = 0; i < players.Count; i++)
            {
                if (i == 0) {
                    players[i].AskForACard(players, i, stock, HumanPlayer.Peek(selectedPlayerCard).Value);
                }
                else
                    players[i].AskForACard(players, i, stock);

                if (pullOutBooks(players[i]))
                {
                    int drawThatMany = Math.Min(5, stock.Count);
                    for (int _ = 0; _ < drawThatMany; _++)
                        players[i].TakeCard(stock.Deal());
                }
            }

            HumanPlayer.SortHand();
            if (stock.Count == 0)
            {
                gameProgressTextBox.Text += "Talia jest pusta. Koniec gry!\r\n";//move to form1
                return true;
            }
            return false;
        }

        private bool pullOutBooks(Player player)
        {
            foreach (Values value in player.PullOutBooks()) {
                Books.Add(value, player);
            }
            if (player.CardCount == 0)
                return true;
            return false;
        }



        public string GetWinnerName()
        {
            var playerScores = new Dictionary<Player, int>();
            foreach (var pair in Books)
            {
               // playerScores.TryGetValue(pair.Value, out int score);
                playerScores[pair.Value]++;
            }
            List<Player> winners = new List<Player>();
            int winningScore = 0;
            int winnerCount = 0;
            foreach (var pair in playerScores)
            {
                if (pair.Value > winningScore)
                {
                    winningScore = pair.Value;
                    winners.Add(pair.Key);
                    winnerCount = 1;
                }
                else if (pair.Value == winningScore) {
                    winners.Add(pair.Key);
                    winnerCount++;
                }
            }
            string winnerNames = "";
            if (winnerCount == 1)
            {
                return winners[0].Name;
            }
            foreach (var winner in winners.SkipLast(1)) {
                winnerNames += winner.Name + " i";
            }
            winnerNames += winners.Last().Name;
            return "Remis pomiędzy" + winnerNames;

        }




    }
}
