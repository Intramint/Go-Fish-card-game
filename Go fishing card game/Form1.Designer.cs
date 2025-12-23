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
            textName = new TextBox();
            buttonStart = new Button();
            textProgress = new TextBox();
            textBooks = new TextBox();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            buttonAsk = new Button();
            listHand = new ListBox();
            SuspendLayout();
            // 
            // textName
            // 
            textName.Location = new Point(24, 24);
            textName.Name = "textName";
            textName.Size = new Size(244, 23);
            textName.TabIndex = 0;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(274, 24);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(277, 23);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "Rozpocznij grę";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // textProgress
            // 
            textProgress.Location = new Point(24, 77);
            textProgress.Multiline = true;
            textProgress.Name = "textProgress";
            textProgress.ReadOnly = true;
            textProgress.Size = new Size(527, 245);
            textProgress.TabIndex = 3;
            // 
            // textBooks
            // 
            textBooks.Location = new Point(24, 350);
            textBooks.Multiline = true;
            textBooks.Name = "textBooks";
            textBooks.ReadOnly = true;
            textBooks.Size = new Size(527, 88);
            textBooks.TabIndex = 4;
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
            // button1
            // 
            button1.Location = new Point(24, 62);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Postępy gry";
            button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 332);
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
            // buttonAsk
            // 
            buttonAsk.Enabled = false;
            buttonAsk.Location = new Point(557, 415);
            buttonAsk.Name = "buttonAsk";
            buttonAsk.Size = new Size(231, 23);
            buttonAsk.TabIndex = 9;
            buttonAsk.Text = "Zażądaj karty";
            buttonAsk.UseVisualStyleBackColor = true;
            buttonAsk.Click += buttonAsk_Click;
            // 
            // listHand
            // 
            listHand.FormattingEnabled = true;
            listHand.Location = new Point(557, 27);
            listHand.Name = "listHand";
            listHand.Size = new Size(231, 379);
            listHand.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listHand);
            Controls.Add(buttonAsk);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBooks);
            Controls.Add(textProgress);
            Controls.Add(buttonStart);
            Controls.Add(textName);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textName;
        private Button buttonStart;
        private TextBox textProgress;
        private TextBox textBooks;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Button buttonAsk;
        private ListBox listHand;
    }
}
