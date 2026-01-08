using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class MessageCreatedEventArgs : EventArgs
    {
        public string Message;
        public MessageCreatedEventArgs(string message)
        {
            Message = message;
        }
    }
}
