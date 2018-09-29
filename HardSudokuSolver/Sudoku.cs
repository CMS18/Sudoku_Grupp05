using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SudokuGame
{

    public class Sudoku
    {
        // Tom sudokutabell
        int[,] board = new int[9, 9];

        public Sudoku(string board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    this.board[row, col] = int.Parse(board[col + (9 * row)].ToString());
                }
            }
        }

        private bool isInRow(int row, int number)
        {
            for (int i = 0; i < 9; i++)
                if (board[row, i] == number)
                    return true;
            return false;
        }

        private bool isInCol(int col, int number)
        {
            for (int i = 0; i < 9; i++)
                if (board[i, col] == number)
                    return true;
            return false;
        }

        private bool isInBox(int row, int col, int number)
        {
            int r = row - row % 3;
            int c = col - col % 3;
            for (int i = r; i < r + 3; i++)
                for (int j = c; j < c + 3; j++)
                    if (board[i, j] == number)
                        return true;
            return false;
        }

        private bool isOk(int row, int col, int number)
        {
            return !isInRow(row, number) && !isInCol(col, number) && !isInBox(row, col, number);
        }

        public bool Solve()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        for (int number = 1; number <= 9; number++)
                        {
                            if (isOk(row, col, number))
                            {
                                board[row, col] = number;

                                if (Solve())
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row, col] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;

        }

        // Utskrift av sudokutabell
        public void BoardAsText()
        {
            for (int r = 0; r < 9; r++)
            {
                if (r % 3 == 0) Console.WriteLine(" -------------------------");
                for (int c = 0; c < 9; c++)
                {
                    if (c % 3 == 0) Console.Write(" |");
                    Console.Write(" " + board[r, c]);
                }
                Console.Write(" |");
                Console.WriteLine();
            }
            Console.WriteLine(" -------------------------");
        }
    }
}

