using System;
using System.Drawing;
using System.Windows.Forms;

namespace B21_Ex05
{
    public partial class GameForm : Form
    {
        private const int k_Padding = 10;
        private const int k_ButtonSize = 40;
        private const int k_SpaceSize = 5;
        private readonly int r_BoardLength;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Label player1Label = new Label();
        private Label player2Label = new Label();
        //Bonus: I created a Delegate that handles the event of a button being pressed. Logic is handled in the TicTacToe Class
        public event Action<int, int> OnButtonSelection;
        public delegate bool MessageBoxDelegate();

        public GameForm(int i_BoardLength, Player i_Player1, Player i_Player2)
        {
            r_Player1 = i_Player1;
            r_Player2 = i_Player2;
            r_BoardLength = i_BoardLength;
            Size = new Size(r_BoardLength * (k_ButtonSize + k_SpaceSize) + 35 , (r_BoardLength + 2) * (k_ButtonSize + k_SpaceSize));
            FormBorderStyle = FormBorderStyle.Fixed3D;
            StartPosition = FormStartPosition.CenterScreen;
            Padding = new Padding(k_Padding, k_Padding, k_Padding, k_Padding);
            Text = "TicTacToeMisere";
            addButtons();
            addScoreLabel();
        }

        private void addButtons()
        {
            int index = 0;
            for (int row = 0; row < r_BoardLength; row++)
            {
                for (int col = 0; col < r_BoardLength; col++)
                {
                    Button currButton = new Button();
                    currButton.Size = new Size(k_ButtonSize, k_ButtonSize);
                    currButton.Location = new Point((k_SpaceSize + k_ButtonSize) * col + k_Padding, (k_SpaceSize + k_ButtonSize) * row + k_Padding);
                    currButton.Click += new EventHandler(ButtonClickedEvent);
                    currButton.TabStop = false;
                    currButton.Name = (index).ToString();
                    index++;
                    Controls.Add(currButton);
                }
            }
        }

        protected void ButtonClickedEvent(object i_Button, EventArgs i_Click)
        {
            int buttonIndex;

            (i_Button as Button).Text = TicTacToeGame.s_CurrentPlayer.GetCoinTypeAsString();
            (i_Button as Button).Enabled = false;
            int.TryParse((i_Button as Button).Name, out buttonIndex);
            int row = buttonIndex / r_BoardLength;
            int col = buttonIndex % r_BoardLength;
            OnButtonSelection?.Invoke(row, col);
            changeFontBoldness();
        }

        private void changeFontBoldness()
        {
            if (TicTacToeGame.s_CurrentPlayer.GetCoinType() == Cell.eCellContent.O)
            {
                player1Label.Font = new Font(player1Label.Font, FontStyle.Bold);
                player2Label.Font = new Font(player1Label.Font, FontStyle.Regular);
            }
            else
            {
                player2Label.Font = new Font(player1Label.Font, FontStyle.Bold);
                player1Label.Font = new Font(player1Label.Font, FontStyle.Regular);
            }
        }

        private void addScoreLabel()
        {
            player2Label.Text = r_Player2.getName() + ": " + r_Player2.getScore();
            player2Label.Size = new Size(k_ButtonSize, k_ButtonSize);
            player2Label.AutoSize = true;
            player2Label.Location = new Point((k_SpaceSize + k_ButtonSize) * r_BoardLength/2 + k_Padding, (k_SpaceSize + k_ButtonSize) * r_BoardLength + k_Padding);
            Controls.Add(player2Label);
            player1Label.Text = r_Player1.getName() + ": " + r_Player1.getScore();
            player2Label.Size = new Size(k_ButtonSize, k_ButtonSize);
            player2Label.AutoSize = true;
            player1Label.Width = player1Label.Text.Length*5+k_Padding;
            player1Label.Location = new Point(player2Label.Left - player1Label.Size.Width, player2Label.Top);
            player1Label.Font = new Font(player1Label.Font, FontStyle.Bold);
            Controls.Add(player1Label);
        }

        public bool TieMessageBox()
        {
            string message = "Tie!" + Environment.NewLine + "Would you like to play another round?";
            string caption = "A Tie!";

            return (MessageBox.Show(message, caption , MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        public bool WinMessageBox()
        {
            string message = "The winner is " + getOpponentsName() + "!" + Environment.NewLine + "Would you like to play another round?";
            string caption = "A Win!";

            return (MessageBox.Show(message, caption, MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        private string getOpponentsName()
        {
            string opponent;
            if (TicTacToeGame.s_CurrentPlayer == r_Player1)
            {
                opponent = r_Player2.getName();
            }
            else
            {
                opponent = r_Player1.getName();
            }

            return opponent;
        }

        public void SetButton(int i_Row, int i_Col)
        {
            int buttonIndex = i_Row * r_BoardLength + i_Col;
            Control button = Controls.Find(buttonIndex.ToString(), true)[0];
            button.Text = "X";
            button.Enabled = false;

            OnButtonSelection?.Invoke(i_Row, i_Col);
            changeFontBoldness();
        }
    }
}
