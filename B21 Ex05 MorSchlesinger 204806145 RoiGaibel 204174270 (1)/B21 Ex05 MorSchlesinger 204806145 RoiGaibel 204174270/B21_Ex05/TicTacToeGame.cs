using System;

namespace B21_Ex05
{
    public class TicTacToeGame
    {
        private GameForm m_GameForm;
        public static Player s_CurrentPlayer;
        public static Cell.eCellContent s_OpponentCoinType;
        private int m_BoardDimensions;
        private Player m_Player1, m_Player2;
        private ProgressBoard m_ProgressBoard;
        private int m_SelectedRow, m_SelectedColumn;
        private int m_NumOfEmptyCells;
        private bool m_OpponentIsComputer;
        private int m_ComputersBestChoice;

        public TicTacToeGame()
        {
            initializeGameSettings();
            playGame();
        }

        private void initializeGameSettings()
        {
            SetUpForm setUpForm = new SetUpForm();
            setUpForm.ShowDialog();
            setUpForm.GetFormProperties(out m_Player1, out m_Player2, out m_BoardDimensions, out m_OpponentIsComputer);
        }

        private void playGame()
        {
            m_NumOfEmptyCells = m_BoardDimensions * m_BoardDimensions;
            s_CurrentPlayer = m_Player1;
            s_OpponentCoinType = m_Player2.GetCoinType();
            m_ProgressBoard = new ProgressBoard(m_BoardDimensions);
            m_GameForm = new GameForm(m_BoardDimensions, m_Player1, m_Player2);
            m_GameForm.OnButtonSelection += resultsOfTurn;
            m_GameForm.ShowDialog();
        }

        private void resultsOfTurn(int i_Row, int i_Col)
        {
            GameForm.MessageBoxDelegate winDelegate = new GameForm.MessageBoxDelegate(m_GameForm.WinMessageBox);
            GameForm.MessageBoxDelegate tieDelegate = new GameForm.MessageBoxDelegate(m_GameForm.TieMessageBox);
            m_NumOfEmptyCells--;
            if(m_ProgressBoard.IsThereASequence(i_Row, i_Col)) 
            {
                gameOver(winDelegate(), true);
            }
            else if(m_NumOfEmptyCells == 0)
            {
                gameOver(tieDelegate(), false);
            }
            else
            {
                changeTurns();
            }
        }

        private void gameOver(bool i_PlayAnotherRound, bool i_ChangeScore)
        {
            if (i_PlayAnotherRound)
            {
                if (i_ChangeScore)
                {
                    setScore();
                }
                m_GameForm.Dispose();
                playGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void changeTurns()
        {
            if (s_CurrentPlayer == m_Player1)
            {
                s_CurrentPlayer = m_Player2;
                s_OpponentCoinType = m_Player1.GetCoinType();
            }
            else
            {
                s_CurrentPlayer = m_Player1;
                s_OpponentCoinType = m_Player2.GetCoinType();
            }

            if (m_OpponentIsComputer && s_CurrentPlayer == m_Player2)
            {
                ComputerTurn();
            }
        }
        
        private void setScore()
        {
            if (s_CurrentPlayer == m_Player1)
            {
                m_Player2.increaseScore();
            }
            else
            {
                m_Player1.increaseScore();
            }
        }

        public void ComputerTurn()
        {
            //note that this can be beaten. unlike the minimax method, which we implemented at first, we opted for a computer that required low space and low runtime complexity
            //the Computer will choose his cell intelligently and by this order of availability:
            //1) a row or column that are mixed with both o and x, thus it doesn't endanger itself, and places more pressure on the opponent to choose one of his own typed cells
            //2) next a row or colum which is Empty, as there is very little danger in choosing it
            //3) next if the above arent available, then a row or column with his opponents type. which helps his opponent, but atleast doesn't endanger himself
            //4) lastly if the computer is forced, he will choose a row or column of his own type, which is the most dangerous, and will possibly make him lose.
            // a point system will dictate which Empty cell I will choose:
            /*
            1: column and row of type x
            2: column of type x and row of type o          or column of type o and row of type x
            3: column of type x and row of type Empty      or column of type Empty and row of type x
            4: column of type x and row of type Mixed      or column of type Mixed and row of type x
            5: column and row of type o
            6: column of type o and row of type Empty      or column of type Empty and row of type o
            7: column of type o and row of type Mixed      or column of type Mixed and row of type o
            8: column and row of type Empty
            9: column of type Empty and row of type Mixed  or column of type Mixed and row of type Empty
            10: column and row of type Mixed
             */
            m_ComputersBestChoice = 0;

            for (int i = 0; i < m_BoardDimensions; i++)
            {
                for (int j = 0; j < m_BoardDimensions; j++)
                {
                    if (m_ProgressBoard.IsCellFilled(i, j))
                    {
                        if (m_ProgressBoard.RowIsMixed(i) && m_ProgressBoard.ColumnIsMixed(j))
                        {
                            setComputersCellChoice(i, j, 10);
                        }
                        else if ((m_ProgressBoard.RowIsEmpty(i) && m_ProgressBoard.ColumnIsMixed(j)) || (m_ProgressBoard.RowIsMixed(i) && m_ProgressBoard.ColumnIsEmpty(j)))
                        {
                            setComputersCellChoice(i, j, 9);
                        }
                        else if (m_ProgressBoard.RowIsEmpty(i) && m_ProgressBoard.ColumnIsEmpty(j))
                        {
                            setComputersCellChoice(i, j, 8);
                        }
                        else if ((m_ProgressBoard.RowIsO(i) && m_ProgressBoard.ColumnIsMixed(j)) || (m_ProgressBoard.RowIsMixed(i) && m_ProgressBoard.ColumnIsO(j)))
                        {
                            setComputersCellChoice(i, j, 7);
                        }
                        else if ((m_ProgressBoard.RowIsO(i) && m_ProgressBoard.ColumnIsEmpty(j)) || (m_ProgressBoard.RowIsEmpty(i) && m_ProgressBoard.ColumnIsO(j)))
                        {
                            setComputersCellChoice(i, j, 6);
                        }
                        else if (m_ProgressBoard.RowIsO(i) && m_ProgressBoard.ColumnIsO(j))
                        {
                            setComputersCellChoice(i, j, 5);
                        }
                        else if ((m_ProgressBoard.RowIsMixed(i) && m_ProgressBoard.ColumnIsX(j)) || (m_ProgressBoard.RowIsX(i) && m_ProgressBoard.ColumnIsMixed(j)))
                        {
                            setComputersCellChoice(i, j, 4);
                        }
                        else if ((m_ProgressBoard.RowIsEmpty(i) && m_ProgressBoard.ColumnIsX(j)) || (m_ProgressBoard.RowIsX(i) && m_ProgressBoard.ColumnIsEmpty(j)))
                        {
                            setComputersCellChoice(i, j, 3);
                        }
                        else if ((m_ProgressBoard.RowIsO(i) && m_ProgressBoard.ColumnIsX(j)) || (m_ProgressBoard.RowIsX(i) && m_ProgressBoard.ColumnIsO(j)))
                        {
                            setComputersCellChoice(i, j, 2);
                        }
                        else if (m_ProgressBoard.RowIsX(i) && m_ProgressBoard.ColumnIsX(j))
                        {
                            setComputersCellChoice(i, j, 1);
                        }
                    }
                }
            }
            m_GameForm.SetButton(m_SelectedRow, m_SelectedColumn);
        }
        private void setComputersCellChoice(int i_Row, int i_Col , int i_ComputersBestChoice)
        {
            if (m_ComputersBestChoice < i_ComputersBestChoice)
            {
                m_SelectedRow = i_Row;
                m_SelectedColumn = i_Col;
                m_ComputersBestChoice = i_ComputersBestChoice;
            }
        }
    }
}