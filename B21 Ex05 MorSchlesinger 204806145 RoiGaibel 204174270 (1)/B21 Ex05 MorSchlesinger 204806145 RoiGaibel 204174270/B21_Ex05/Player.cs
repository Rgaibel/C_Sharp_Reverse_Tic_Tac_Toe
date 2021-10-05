namespace B21_Ex05
{
    public class Player
    {
        private readonly string r_PlayerName;
        private readonly Cell.eCellContent r_CoinType;
        private int m_PlayerScore;
        private static bool s_FirstPlayer = true;

        public Player()
        {
            r_PlayerName = "Computer";
            m_PlayerScore = 0;
            r_CoinType = Cell.eCellContent.X;
        }
        public Player(string i_name)
        {
            r_PlayerName = i_name;
            m_PlayerScore = 0;
            if (s_FirstPlayer)
            {
                r_CoinType = Cell.eCellContent.O;
                s_FirstPlayer = false;
            }
            else
            {
                r_CoinType = Cell.eCellContent.X;
            }
        }

        public void increaseScore()
        {
            m_PlayerScore++;
        }
        public int getScore()
        {
            return m_PlayerScore;
        }

        public string getName()
        {
            return r_PlayerName;
        }

        public Cell.eCellContent GetCoinType()
        {
            return r_CoinType;
        }

        public string GetCoinTypeAsString()
        {
            string playersCoinType;
            if (GetCoinType() == Cell.eCellContent.O)
            {
                playersCoinType = "O";
            }
            else
            {
                playersCoinType = "X";
            }
            return playersCoinType;
        }
    }
}
