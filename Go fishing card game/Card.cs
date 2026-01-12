using System;
using System.Collections.Generic;
using System.Text;
//using System.Security.Policy;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Go_fishing_card_game
{
    public class Card
    {
        public Card(Suits suit, CardValues value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public Suits Suit;
        public CardValues Value;
        public string Name { get { return names[(int)Value] + " " + suits[(int)Suit]; }} //try adding numbers and symbols for suits

        private static string[] suits = new string[] { "pik", "trefl", "karo", "kier" };

        private static string[] names = new string[]
        {
            "", "As", "Dwójka", "Trójka", "Czwórka", "Piątka", "Szóstka", "Siódemka", "Ósemka", "Dziewiątka", "Dziesiątka", "Walet", "Dama", "Król" 
        };
        private static string[] names0 = new string[]
        {
            "", "asów", "dwójek", "trójek", "czwórek", "piątek", "szóstek", "siódemek", "ósemek", "dziewiątek", "dziesiątek", "waletów", "dam", "królów"
        };
        private static string[] names1 = new string[]
        {
            "", "asa", "dwójkę", "trójkę", "czwórkę", "piątkę", "szóstkę", "siódemkę", "ósemkę", "dziewiątkę", "dziesiątkę", "waleta", "damę", "króla"
        };
        private static string[] names2andMore = new string[]
        {
            "", "asy", "dwójki", "trójki", "czwórki", "piątki", "szóstki", "siódemki", "ósemki", "dziewiątki", "dziesiątki", "walety", "damy", "króle"
        };

        public override string ToString()
        {
            return Name;
        }

        public static string Plural(CardValues value, int count)
        {
            if (count == 0)
                return names0[(int)value];
            if (count == 1)
                return names1[(int)value];
            return names2andMore[(int)value];
        }
    }
}
