using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class GameEndedEventArgs : EventArgs
    {
        public GameEndedEventArgs(Player winner) 
        {
            Winner = winner;
        }
        Player Winner;
    }
}
