using System;
using System.Collections.Generic;
using System.Text;

namespace Go_fishing_card_game
{
    public class GameLogger
    {
        public GameLogger(TextBox textBox)
        {
            this.textBox = textBox;
        }
        
        private readonly TextBox textBox;

        public void Write(string text)
        {
            textBox.Text += text + Environment.NewLine;
        }

        public void Clear()
        {
            textBox.Text = "";
        }
    }
}
