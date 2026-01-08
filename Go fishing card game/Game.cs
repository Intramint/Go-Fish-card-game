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
        public int PlayerCount { get; private set; }
        public int StockCardCount { get { return stock.Count; } }
        public event EventHandler<MessageCreatedEventArgs> MessageCreated;


        public Game(IEnumerable<string> playerNames)
        {
            Random random = new Random();
            players = new List<Player>();
            foreach (string playerName in playerNames)
            {
                Player player = new Player(playerName, random);
                players.Add(player);
                player.MessageCreated += Player_MessageCreated;
            }
            PlayerCount = players.Count;
            HumanPlayer = players[0];
            Books = new Dictionary<Values, Player>();
            stock = Deck.CreateFullDeck();
            Deal();
            HumanPlayer.SortHand();
            
        }

        protected virtual void OnMessageCreated(MessageCreatedEventArgs e)
        {
            MessageCreated?.Invoke(this, e);
        }
        private void Player_MessageCreated(object? sender, MessageCreatedEventArgs e)
        {
            OnMessageCreated(e);
        }

        public Player HumanPlayer { get; private set; }

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
           
            for (int i = 0; i < players.Count; i++)
            {
                if (i == 0) {
                    players[i].AskForACard(players, i, stock, HumanPlayer.Peek(selectedPlayerCard).Value);
                }
                else
                    players[i].AskForACard(players, i, stock);

                if (PullOutBooks(players[i]))
                {
                    int drawThatMany = Math.Min(5, stock.Count);
                    for (int _ = 0; _ < drawThatMany; _++)
                        players[i].TakeCard(stock.Deal());
                }
            }

            HumanPlayer.SortHand();
            if (stock.Count == 0)
            {
                OnMessageCreated(new MessageCreatedEventArgs("Talia jest pusta. Koniec gry!\r\n"));
                return true;
            }
            return false;
        }

        private bool PullOutBooks(Player player)
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
