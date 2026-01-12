using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class BookScoredEventArgs
    {
        public BookScoredEventArgs (Player player, CardValues cardValue)
        {
            Player = player;
            CardValue = cardValue;
        }

        Player Player;
        CardValues CardValue;
    }
}
