namespace Go_fishing_card_game
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            playerNameTextBox = new TextBox();
            startButton = new Button();
            gameProgressTextBox = new TextBox();
            completedBooksTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            askForCardButton = new Button();
            handListBox = new ListBox();
            gameProgressLabel = new Label();
            SuspendLayout();
            // 
            // playerNameTextBox
            // 
            playerNameTextBox.Location = new Point(24, 24);
            playerNameTextBox.MaxLength = 21;
            playerNameTextBox.Name = "playerNameTextBox";
            playerNameTextBox.Size = new Size(244, 23);
            playerNameTextBox.TabIndex = 0;
            // 
            // startButton
            // 
            startButton.Location = new Point(274, 24);
            startButton.Name = "startButton";
            startButton.Size = new Size(277, 23);
            startButton.TabIndex = 1;
            startButton.Text = "Rozpocznij grę";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += buttonStart_Click;
            // 
            // gameProgressTextBox
            // 
            gameProgressTextBox.BackColor = SystemColors.Control;
            gameProgressTextBox.Location = new Point(24, 77);
            gameProgressTextBox.Multiline = true;
            gameProgressTextBox.Name = "gameProgressTextBox";
            gameProgressTextBox.ReadOnly = true;
            gameProgressTextBox.Size = new Size(527, 294);
            gameProgressTextBox.TabIndex = 3;
            // 
            // completedBooksTextBox
            // 
            completedBooksTextBox.Location = new Point(24, 392);
            completedBooksTextBox.Multiline = true;
            completedBooksTextBox.Name = "completedBooksTextBox";
            completedBooksTextBox.ReadOnly = true;
            completedBooksTextBox.Size = new Size(527, 88);
            completedBooksTextBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 6);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 5;
            label1.Text = "Imię";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 374);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 7;
            label2.Text = "Grupy";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(557, 9);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 8;
            label3.Text = "Ręka";
            // 
            // askForCardButton
            // 
            askForCardButton.Enabled = false;
            askForCardButton.Location = new Point(557, 457);
            askForCardButton.Name = "askForCardButton";
            askForCardButton.Size = new Size(231, 23);
            askForCardButton.TabIndex = 9;
            askForCardButton.Text = "Zażądaj karty";
            askForCardButton.UseVisualStyleBackColor = true;
            askForCardButton.Click += askForCardButton_Click;
            // 
            // handListBox
            // 
            handListBox.FormattingEnabled = true;
            handListBox.Location = new Point(557, 27);
            handListBox.Name = "handListBox";
            handListBox.Size = new Size(231, 424);
            handListBox.TabIndex = 10;
            // 
            // gameProgressLabel
            // 
            gameProgressLabel.AutoSize = true;
            gameProgressLabel.Location = new Point(33, 66);
            gameProgressLabel.Name = "gameProgressLabel";
            gameProgressLabel.Size = new Size(69, 15);
            gameProgressLabel.TabIndex = 11;
            gameProgressLabel.Text = "Postępy gry";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 492);
            Controls.Add(gameProgressLabel);
            Controls.Add(handListBox);
            Controls.Add(askForCardButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(completedBooksTextBox);
            Controls.Add(gameProgressTextBox);
            Controls.Add(startButton);
            Controls.Add(playerNameTextBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox playerNameTextBox;
        private Button startButton;
        private TextBox gameProgressTextBox;
        private TextBox completedBooksTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button askForCardButton;
        private ListBox handListBox;
        private Label gameProgressLabel;
    }
}
