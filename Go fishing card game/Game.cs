using System;
using System.Collections.Generic;
using System.Text;
//using System.Numerics;

namespace Go_fishing_card_game
{
    internal class Game
    {
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
            Books = new Dictionary<CardValues, Player>();
            stock = Deck.CreateEmptyDeck();
            Deck.FillDeck(stock);
            StartingDeal();
            HumanPlayer.SortHand();
        }

        public List<Player> players;
        public int PlayerCount { get; private set; }
        public Player HumanPlayer { get; private set; }
        public Dictionary<CardValues, Player> Books { get; private set; }
        public int StockCardCount { get { return stock.Count; } }
        public event EventHandler<MessageCreatedEventArgs> MessageCreated;

        private Deck stock;

        public bool PlayOneRound(int selectedPlayerCard)
        {

            for (int i = 0; i < players.Count; i++)
            {
                if (i == 0)
                {
                    players[i].AskForACard(players, i, stock, HumanPlayer.Peek(selectedPlayerCard).Value);
                }
                else
                    players[i].AskForACard(players, i, stock);

                if (ScoreBooks(players[i]))
                {
                    int drawThatMany = Math.Min(5, stock.Count);//change to overloaded Draw() that draws multiple
                    for (int _ = 0; _ < drawThatMany; _++)
                        players[i].ReceiveCard(stock.DealTop());//change to Draw()
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
        public string GetWinnerName()
        {
            var playerScores = new Dictionary<Player, int>();
            foreach (var pair in Books)
            {
                playerScores.TryGetValue(pair.Value, out int score);
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
                else if (pair.Value == winningScore)
                {
                    winners.Add(pair.Key);
                    winnerCount++;
                }
            }
            string winnerNames = "";
            if (winnerCount == 1)
            {
                return winners[0].Name;
            }
            foreach (var winner in winners.SkipLast(1))
            {
                winnerNames += winner.Name + " i";
            }
            winnerNames += winners.Last().Name;
            return "Remis pomiędzy" + winnerNames;
        }

        private void StartingDeal()//should give 5 to each player
        {
            foreach (Player player in players)
            {
                for (int i = 0; i < 5; i++)
                    player.ReceiveCard(stock.DealTop());
                ScoreBooks(player);
            }
        }

        private bool ScoreBooks(Player player) //change to try all players
        {
            if (player.)
            foreach (CardValues value in player.ScoreBook())
            {
                Books.Add(value, player);
            }
            if (player.CardCount == 0)
                return true;
            return false;
        }

        protected virtual void OnMessageCreated(MessageCreatedEventArgs e)
        {
            MessageCreated?.Invoke(this, e);
        }

        private void Player_MessageCreated(object? sender, MessageCreatedEventArgs e)
        {
            OnMessageCreated(e);
        }
    }
}
