using System.Windows.Markup;
using static System.Reflection.Metadata.BlobBuilder;

namespace Go_fishing_card_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gameLog = new GameLogger(gameProgressTextBox);
            booksLog = new GameLogger(completedBooksTextBox);
        }

        private GameLogger gameLog;
        private GameLogger booksLog;
        private Game game;

        private void updateForm()
        {
            handListBox.Items.Clear();
            foreach (var cardName in game.HumanPlayer.GetCardNames())
                handListBox.Items.Add(cardName);
            DescribeBooks();
            DescribePlayerHands();
            gameProgressTextBox.SelectionStart = gameProgressTextBox.Text.Length; //
            gameProgressTextBox.ScrollToCaret();                                  // scrolls text to the bottom in case there's too much text
        }


        private void DescribePlayerHands()
        {
            string description;
            for (int i = 0; i < game.PlayerCount; i++)
            {
                description = "";
                description += game.players[i].Name + " ma " + game.players[i].CardCount;
                if (game.players[i].CardCount == 1)
                    description += " kartê.";
                else if (game.players[i].CardCount == 2 || game.players[i].CardCount == 3 || game.players[i].CardCount == 4)
                    description += " karty.";
                else
                    description += " kart.";
                gameLog.Write(description);
            }
            gameLog.Write($"W talii pozosta³o kart: {game.StockCardCount}");
        }

        private void DescribeBooks()
        {
            booksLog.Clear();
            foreach (CardValues cardValue in game.Books.Keys) //change to react to a BookScored event
            {
                booksLog.Write($"{game.Books[cardValue].Name} ma grupê {Card.Plural(cardValue, 0)}");
            }
        }

        private void game_MessageCreated(object? sender, MessageCreatedEventArgs e)
        {
            gameLog.Write(e.Message);
        }

        private void askForCardButton_Click(object sender, EventArgs e)
        {
            if (handListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Wybierz kartê.");
                return;
            }
            gameLog.Clear();
            if (game.PlayOneRound(handListBox.SelectedIndex)) //change PlayOneRound to void and instead react to a game end event
            {
                gameLog.Write("Zwyciêzc¹ jest... " + game.GetWinnerName());
                DescribeBooks();
                askForCardButton.Enabled = false;
            }
            else
                updateForm();
        }

        private void game_GameEnded(object? sender, GameEndedEventArgs e)
        {

        }

        private void game_BookScored(object? sender, BookScoredEventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(playerNameTextBox.Text))
            {
                MessageBox.Show("Wpisz swoje imiê", "Nie mo¿na jeszcze rozpocz¹æ gry.");
                return;
            }
            
            string humanName = playerNameTextBox.Text;
            string[] opponentNames = { "Janek", "Bartek" };
            game = new Game(humanName, opponentNames);

            game.MessageCreated += game_MessageCreated;
            game.GameEnded += game_GameEnded;
            game.BookScored += game_BookScored;

            foreach (string name in opponentNames)
            {
                gameLog.Write($"{name} do³¹czy³ do gry");
            }
            startButton.Enabled = false;
            playerNameTextBox.Enabled = false;
            askForCardButton.Enabled = true;
            updateForm();
        }
    }
}
