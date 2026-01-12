using System;
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
            //todo: need to check for any books in starting hands

            HumanPlayer.SortHand();
        }

        public List<Player> players;
        public int PlayerCount { get; }
        public HumanPlayer HumanPlayer { get; }
        public Dictionary<CardValues, Player> Books { get; } //might be able to change later to just storing how many books each player has instead of which books they have
        public int StockCardCount { get { return drawPile.Count; } }
        public event EventHandler<MessageCreatedEventArgs> MessageCreated;

        private Deck drawPile;

        public bool PlayOneRound(int selectedPlayerCard)
        {
            HumanPlayer.SetChosenCardValue(selectedPlayerCard);
            foreach (Player player in players)
            {
                CardValues cardValue = player.GetChosenCardValue();

                if (AskEveryoneForACard(player, cardValue))
                {
                    if (player.HasBook(cardValue)) //add BookScored event, so the books listbox doesnt need to get updated on each turn
                    {
                        player.DiscardBook(cardValue);
                        Books.Add(cardValue, player);
                        //BookScored event
                        if (player.HasEmptyHand)
                            player.Draw(drawPile, 5);
                    }
                }
                else
                {
                    player.Draw(drawPile);
                    if (drawPile.IsEmpty)
                    {
                        //game end event here
                        return true;
                    }
                }
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
                return winners[0].Name;
            }
            foreach (var winner in winners.SkipLast(1))
            {
                winnerNames += winner.Name + " i";
            }
            winnerNames += winners.Last().Name;
            return "Remis pomiędzy" + winnerNames;
        }

        protected virtual void OnMessageCreated(MessageCreatedEventArgs e)
        {
            MessageCreated?.Invoke(this, e);
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

        private void checkForGameEnd()
        {
            if (drawPile.IsEmpty)
            {
                //game end event
            }
                return;
        }

        private void Player_MessageCreated(object? sender, MessageCreatedEventArgs e)
        {
            OnMessageCreated(e);
        }
    }
}
