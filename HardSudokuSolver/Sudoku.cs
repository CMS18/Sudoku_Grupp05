using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{

    public class Sudoku
    {
        //tom sudokutabell
        int[,] board = new int[9, 9];

        //constructor
        public Sudoku(int[,] board)
        {
            //loop för att sätta värden i tom sudokutabell
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.board[i, j] = board[i, j];
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
        //utskrift av sudokutabell
        public void BoardAsText()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(" " + board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int[,] StartingBoard = {
                            { 0, 6, 0, 0, 9, 0, 0, 2, 0 },
                            { 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 7, 0, 0, 0, 1, 8, 0, 0 },
                            { 0, 1, 5, 3, 0, 0, 0, 0, 2 },
                            { 0, 0, 0, 6, 0, 4, 0, 0, 0 },
                            { 8, 0, 0, 0, 0, 5, 4, 3, 0 },
                            { 0, 0, 3, 4, 0, 0, 0, 5, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 9 },
                            { 0, 5, 0, 0, 7, 0, 0, 6, 0 }
                            };
            Sudoku game = new Sudoku(StartingBoard);
            Console.WriteLine(" Sudoku game to solve: ");
            game.BoardAsText();

            if (game.Solve())
            {
                Console.WriteLine("Sudoku Grid solved with simple BT");
                game.BoardAsText();

            }
            else
                Console.WriteLine("UNsolvable!");


            Console.ReadKey();


        }
    }

}

