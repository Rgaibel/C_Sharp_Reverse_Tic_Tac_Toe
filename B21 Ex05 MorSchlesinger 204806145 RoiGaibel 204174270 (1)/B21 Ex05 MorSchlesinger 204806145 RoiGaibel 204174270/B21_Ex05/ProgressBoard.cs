namespace B21_Ex05
{
    public class ProgressBoard
    {
        private Cell.eCellContent[] m_RowOfCellType;
        private Cell.eCellContent[] m_ColumnOfCellType;
        private int[] m_RowNumOfFilledCells;
        private int[] m_ColumnNumOfFilledCells;
        private readonly int r_BoardDimensions;
        private bool[,] m_ChosenCells;

        public ProgressBoard(int i_BoardDimensions)
        {
            m_RowOfCellType = new Cell.eCellContent[i_BoardDimensions];
            m_ColumnOfCellType = new Cell.eCellContent[i_BoardDimensions];
            m_RowNumOfFilledCells = new int[i_BoardDimensions];
            m_ColumnNumOfFilledCells = new int[i_BoardDimensions];
            r_BoardDimensions = i_BoardDimensions;
            m_ChosenCells = new bool[r_BoardDimensions, r_BoardDimensions];

            for (int i = 0; i < i_BoardDimensions; i++)
            {
                m_RowOfCellType[i] = Cell.eCellContent.Empty;
                m_ColumnOfCellType[i] = Cell.eCellContent.Empty;
                m_RowNumOfFilledCells[i] = 0;
                m_ColumnNumOfFilledCells[i] = 0;
            }
        }

        public bool IsThereASequence(int i_SelectedRow, int i_SelectedColumn)
        {
            bool thereIsAFullSequence = false;
            m_ChosenCells[i_SelectedRow, i_SelectedColumn] = true;
            m_RowNumOfFilledCells[i_SelectedRow]++;
            m_ColumnNumOfFilledCells[i_SelectedColumn]++;

            if (m_RowOfCellType[i_SelectedRow] == Cell.eCellContent.Empty)
            {
                m_RowOfCellType[i_SelectedRow] = TicTacToeGame.s_CurrentPlayer.GetCoinType();
            }
            if (m_ColumnOfCellType[i_SelectedColumn] == Cell.eCellContent.Empty)
            {
                m_ColumnOfCellType[i_SelectedColumn] = TicTacToeGame.s_CurrentPlayer.GetCoinType();
            }

            if (m_RowOfCellType[i_SelectedRow] == TicTacToeGame.s_OpponentCoinType)
            {
                m_RowOfCellType[i_SelectedRow] = Cell.eCellContent.inMixedRowOrColumn;
            }
            if (m_ColumnOfCellType[i_SelectedColumn] == TicTacToeGame.s_OpponentCoinType)
            {
                m_ColumnOfCellType[i_SelectedColumn] = Cell.eCellContent.inMixedRowOrColumn;
            }

            if (m_RowOfCellType[i_SelectedRow] == TicTacToeGame.s_CurrentPlayer.GetCoinType())
            {
                if (m_RowNumOfFilledCells[i_SelectedRow] == r_BoardDimensions)
                {
                    thereIsAFullSequence = true;
                }
            }
            if (m_ColumnOfCellType[i_SelectedColumn] == TicTacToeGame.s_CurrentPlayer.GetCoinType())
            {
                if (m_ColumnNumOfFilledCells[i_SelectedColumn] == r_BoardDimensions)
                {
                    thereIsAFullSequence = true;
                }
            }

            return thereIsAFullSequence;     
        }

        public bool IsCellFilled(int i_Row, int i_Col)
        {
            return !m_ChosenCells[i_Row, i_Col];
        }

        public bool RowIsEmpty(int row)
        {
            return m_RowOfCellType[row] == Cell.eCellContent.Empty;
        }
        public bool ColumnIsEmpty(int column)
        {
            return m_ColumnOfCellType[column] == Cell.eCellContent.Empty;
        }

        public bool RowIsMixed(int row)
        {
            return m_RowOfCellType[row] == Cell.eCellContent.inMixedRowOrColumn;
        }

        public bool ColumnIsMixed(int column)
        {
            return m_ColumnOfCellType[column] == Cell.eCellContent.inMixedRowOrColumn;
        }

        public bool RowIsX(int row)
        {
            return m_RowOfCellType[row] == Cell.eCellContent.X;
        }

        public bool ColumnIsX(int column)
        {
            return m_ColumnOfCellType[column] == Cell.eCellContent.X;
        }

        public bool RowIsO(int row)
        {
            return m_RowOfCellType[row] == Cell.eCellContent.O;
        }

        public bool ColumnIsO(int column)
        {
            return m_ColumnOfCellType[column] == Cell.eCellContent.O;
        }
    }
}