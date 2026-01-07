using System.Windows.Markup;
using static System.Reflection.Metadata.BlobBuilder;

namespace Go_fishing_card_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Game game;
        //public Action<string> LostCards(object sender, EventArgs e) dont use Action
        //{
        //    gameProgressTextBox += ;
        //}
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(playerNameTextBox.Text))
            {
                MessageBox.Show("Wpisz swoje imiê", "Nie mo¿na jeszcze rozpocz¹æ gry.");
                return;
            }
            string[] names = { playerNameTextBox.Text, "Janek", "Bartek" };
            game = new Game(names);
            game.LostCards += game_LostCards;
            foreach (string name in names)
            {
                gameProgressTextBox.Text += $"{name} do³¹czy³ do gry\r\n";
            }
            startButton.Enabled = false;
            playerNameTextBox.Enabled = false;
            askForCardButton.Enabled = true;
            updateForm();
        }

        private void game_LostCards(object? sender, LostCardsEventArgs e)
        {
            gameProgressTextBox.Text += describeNumberOfThisCardInHand(e.Name, e.NumberOfThisCardInHand, e.CardValue);
        }

        private string describeNumberOfThisCardInHand(string name, int numberOfThisCardInHand, Values cardValue)
        {
            return $"{name} ma {numberOfThisCardInHand} {Card.Plural(cardValue, numberOfThisCardInHand)}\r\n";
        }

        private void updateForm()
        {
            handListBox.Items.Clear();
            foreach (var cardName in game.HumanPlayer.GetCardNames())
                handListBox.Items.Add(cardName);
            completedBooksTextBox.Text += describeBooks();
            gameProgressTextBox.Text += describePlayerHands();
            gameProgressTextBox.SelectionStart = gameProgressTextBox.Text.Length;
            gameProgressTextBox.ScrollToCaret();
        }

        private string describePlayerHands()
        {
            string description = "";
            for (int i = 0; i < game.PlayerCount; i++)
            {
                description += game.players[i].Name + " ma " + game.players[i].CardCount;
                if (game.players[i].CardCount == 1)
                    description += " kartê.\r\n";
                else if (game.players[i].CardCount == 2 || game.players[i].CardCount == 3 || game.players[i].CardCount == 4)
                    description += " karty.\r\n";
                else
                    description += " kart.\r\n";
            }
            description += $"W talii pozosta³o kart: {game.StockCardCount}\r\n";
            return description;
        }

        private string describeBooks()
        {
            string description = "";
            foreach (Values cardValue in game.Books.Keys)
            {
                description += $"{game.Books[cardValue].Name} ma grupê {Card.Plural(cardValue, 0)},\r\n ";
            }
            description += ".";
            return description;
        }

        private void buttonAsk_Click(object sender, EventArgs e)
        {
            gameProgressTextBox.Text = "";
            if (handListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Wybierz kartê.");
                return;
                
            }
            if (game.PlayOneRound(handListBox.SelectedIndex))
            {
                gameProgressTextBox.Text += "Zwyciêzc¹ jest... " + game.GetWinnerName();
                completedBooksTextBox.Text = describeBooks();
                askForCardButton.Enabled = false;
            }
            else
                updateForm();
        }
    }
}
