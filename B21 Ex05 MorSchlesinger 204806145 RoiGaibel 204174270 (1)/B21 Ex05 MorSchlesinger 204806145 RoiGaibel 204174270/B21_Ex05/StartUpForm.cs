using System;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace B21_Ex05
{
    public partial class SetUpForm : Form
    {
        protected string m_Player1Name = "";
        protected string m_Player2Name = "Computer";
        protected int m_BoardLength = 4;
        protected bool m_OpponentIsComputer = true;

        public SetUpForm()
        {
            InitializeComponent();
        }

        private void computerPlayingCheckBox(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
            m_OpponentIsComputer = !textBox2.Enabled;
            if (!textBox2.Enabled)
            {
                textBox2.Text = "[Computer]";
                textBox2.BackColor = Color.WhiteSmoke;
            }
            else
            {
                textBox2.Text = "";
                textBox2.BackColor = Color.White;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            m_Player1Name = textBox1.Text;
        }

        private void numericUpDown1ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = numericUpDown1.Value;
            m_BoardLength = (int)numericUpDown1.Value;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            m_Player2Name = textBox2.Text;
        }

        private void numericUpDown2ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown2.Value;
            m_BoardLength = (int)numericUpDown2.Value;
        }

        private void startButtonIsClicked(object sender, EventArgs e)
        {
            Close();
        }

        public void GetFormProperties(out Player o_Player1, out Player o_Player2, out int o_BoardLength, out bool o_OppenentIsComputer)
        {
            o_BoardLength = m_BoardLength;
            o_OppenentIsComputer = m_OpponentIsComputer;
            o_Player1 = new Player(m_Player1Name);

            if (m_OpponentIsComputer)
            {
                o_Player2 = new Player();
            }
            else
            {
                o_Player2 = new Player(m_Player2Name);
            }
        }

        private void SetUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
