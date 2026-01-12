using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
//using System.Numerics;

namespace Go_fishing_card_game
{
    internal class Game
    {
        public Game(string humanName, IEnumerable<string> opponentNames)
        {
            Random random = new();
            players = new List<Player>();
            drawPile = new(random);

            HumanPlayer = new HumanPlayer(humanName, drawPile);
            players.Add(HumanPlayer);
            HumanPlayer.MessageCreated += Player_MessageCreated;

            foreach (string opponentName in opponentNames)
            {
                ComputerPlayer player = new(opponentName, drawPile, random);
                players.Add(player);
                player.MessageCreated += Player_MessageCreated;
            }

            PlayerCount = players.Count;

            Books = new Dictionary<CardValues, Player>();
            DealCards();

            HumanPlayer.SortHand();
        }

        public List<Player> players;
        public int PlayerCount { get; }
        public HumanPlayer HumanPlayer { get; }
        public Dictionary<CardValues, Player> Books { get; } //might be able to change later to just storing how many books each player has instead of which books they have
        public int StockCardCount { get { return drawPile.Count; } }
        public event EventHandler<MessageCreatedEventArgs> MessageCreated;
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public event EventHandler<BookScoredEventArgs> BookScored;

        private Deck drawPile;
        private readonly static int bookSize = 4;

        public bool PlayOneRound(int selectedPlayerCard)
        {
            HumanPlayer.SetChosenCardValue(selectedPlayerCard);
            foreach (Player player in players)
            {
                CardValues cardValue = player.GetChosenCardValue();

                if (AskEveryoneForACard(player, cardValue))
                {
                    if (HasThisBook(player, cardValue))
                    {
                        ScoreBook(player, cardValue);
                    }
                }
                else
                    DrawForPlayer(player);
            }

            HumanPlayer.SortHand();
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
            List<Player> winners = new();
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
                return winners[0].Name;//seems incorrect
            }
            foreach (var winner in winners.SkipLast(1))
            {
                winnerNames += winner.Name + " i";
            }
            winnerNames += winners.Last().Name;
            return "Remis pomiędzy" + winnerNames;
        }



        private void DrawForPlayer(Player player, int count = 1)
        {

            for (int i = 0; i < Math.Min(count, drawPile.Count); i++)
            {
                Card drawnCard = drawPile.DealTop();
                player.ReceiveCards(drawnCard);
                if (HasThisBook(player, drawnCard.Value))
                    ScoreBook(player, drawnCard.Value);
            }
            if (drawPile.IsEmpty)
            {
                OnGameEnded(new GameEndedEventArgs(GetWinnerName())); //to change
            }
        }

        protected virtual void OnBookScored (BookScoredEventArgs e)
        {
            BookScored?.Invoke(this, e);
        }
        protected virtual void OnGameEnded (GameEndedEventArgs e)
        {
            GameEnded?.Invoke(this, e);
        }

        protected virtual void OnMessageCreated(MessageCreatedEventArgs e)
        {
            MessageCreated?.Invoke(this, e);
        }
        private void DealCards()
        {
            foreach (Player player in players)
            {
                DrawForPlayer(player, 5);
            }
        }

        private bool HasThisBook(Player player, CardValues cardValue)
        {
            return player.FindMatchingValues(cardValue, bookSize);
        }
            

        private void ScoreBook (Player player, CardValues cardValue)
        {
            player.RemoveCardsOfValue(cardValue);
            Books.Add(cardValue, player);
            //add BookScored event, so the books listbox doesnt need to get updated on each turn
            if (player.HasEmptyHand)
                DrawForPlayer(player, 5);
            //event
        }

        private bool AskEveryoneForACard (Player asker, CardValues cardValue)
        {
            bool anyCardReceived = false;
            foreach (Player opponent in players)
            {
                if (opponent == asker)
                    continue;
                if (asker.AskForACard(opponent, cardValue))
                    anyCardReceived = true;
            }
            return anyCardReceived;
        }

        private void Player_MessageCreated(object? sender, MessageCreatedEventArgs e)
        {
            OnMessageCreated(e);
        }
    }
}
