using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardSudokuSolver
{
    class Sudoku
    {
        private int[,] sudokuBoard = new int[9, 9]; // Sudokuboard
        List<int>[,] possibleValue = new List<int>[9, 9];

        public Sudoku(string board)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    sudokuBoard[row, col] = int.Parse(board[col + (9 * row)].ToString());
                }
            }
        }

        private void InitValues() // Initierar 1-9 för gissningslistan
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    possibleValue[row, col] = new List<int>();
                    for (int i = 1; i <= 9; i++)
                    {
                        possibleValue[row, col].Add(i);
                    }
                }
            }
        }

        private void DisplaysudokuBoard()
        {
            Console.WriteLine("-------------------------");
            for (int row = 0; row < 9; row++)
            {
                Console.Write("|");
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(" " + sudokuBoard[row, col]);
                    if ((col + 1) % 3 == 0)
                    {
                        Console.Write(" |");
                    }
                }
                Console.WriteLine();
                if ((row + 1) % 3 == 0)
                {
                    Console.WriteLine("-------------------------");
                }
            }
        }

        private List<int> GetRowValues(int row)
        {
            List<int> values = new List<int>();
            for (int col = 0; col < 9; col++)
            {
                if (sudokuBoard[row, col] != 0) values.Add(sudokuBoard[row, col]);
            }
            return values;
        }

        private List<int> GetColumnValues(int col)
        {
            List<int> values = new List<int>();
            for (int row = 0; row < 9; row++)
            {
                if (sudokuBoard[row, col] != 0) values.Add(sudokuBoard[row, col]);
            }
            return values;
        }

        private List<int> GetBoxValues(int row, int col)
        {
            List<int> values = new List<int>();

            int xStart = (row / 3) * 3;
            int yStart = (col / 3) * 3;
            for (int x = xStart; x < xStart + 3; x++)
            {
                for (int y = yStart; y < yStart + 3; y++)
                {
                    if (sudokuBoard[x, y] != 0) values.Add(sudokuBoard[x, y]);
                }
            }
            return values;
        }

        public void Solve()
        {
            Console.WriteLine("Pussel att lösa: ");
            DisplaysudokuBoard();

            InitValues();
            List<int> unavailableValues = new List<int>();

            bool wasUpdated = true;
            for (int iteration = 0; wasUpdated; iteration++)
            {
                wasUpdated = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        if (sudokuBoard[row, col] == 0)
                        {
                            unavailableValues = GetRowValues(row);
                            foreach (int value in GetColumnValues(col))
                            {
                                if (!unavailableValues.Contains(value))
                                {
                                    unavailableValues.Add(value);
                                }
                            }
                            foreach (int value in GetBoxValues(row, col))
                            {
                                if (!unavailableValues.Contains(value))
                                {
                                    unavailableValues.Add(value);
                                }
                            }

                            for (int i = 1; i <= 9; i++)
                            {
                                if (unavailableValues.Contains(i))
                                {
                                    possibleValue[row, col].Remove(i);
                                }
                            }
                            if (possibleValue[row, col].Count == 1)
                            {
                                sudokuBoard[row, col] = possibleValue[row, col][0];
                                possibleValue[row, col].Clear();
                                wasUpdated = true;
                            }
                            for (int i = 0; i < unavailableValues.Count; i++)
                            {
                                if (possibleValue[row, col].Contains(unavailableValues[i]))
                                {
                                    int getIndex = possibleValue[row, col].IndexOf(unavailableValues[i]);
                                    possibleValue[row, col].RemoveAt(getIndex);
                                }
                            }
                        }
                    }
                }
            }

            bool isSolved = SolveByTrying(0, 0, 0);
            if (isSolved) Console.WriteLine("Brädet är avklarat!");
            else Console.WriteLine("Du har failat!");


            Console.WriteLine("Efter lösning: ");
            DisplaysudokuBoard();
        }
            DisplaysudokuBoard(1);



        }
        
        private bool SolveByTrying(int row, int col, int idx)
        {
            bool withinBoard = false;
            // Kontrollera att positionen är innanför brädet - annars korrigera den
            if (row < 9 && col < 9) withinBoard = true;
            else if (col > 8 && row < 7)
            {
                col = 0;
                row++;
            }
            else withinBoard = false;
            if (withinBoard == false) return true; // alla rutor genomgådda
            

            // Om positionen är inom brädet:
            if (withinBoard)
            {

            }
            // Om positionen är 0:
            // GetPossibleValues(row,col); // från possibleValue
            // Om (i<possibleValue[row,col].length):
            // board[row,col] = possibleValue[row,col][i];
            // Om nästa lösning är true: if (...
            // Om positionen != 0:
            // return SolveByGuessing(row,col+1,0);
            // Om inte:
            // Om (col > 8 && row > 8):
            // return true; // brädet löst (förhoppningsvis)
            // Annars Om (col > 8):
            // SolveByGuessing(row+1,0,0)
            // return SolveByGuessing(row,col,i);
            return false; // 
        }





            private void PrintPossibleValues()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write($"possibleValue[{row}, {col}]=[");
                    foreach (int item in possibleValue[row, col])
                    {
                        Console.Write(item + ",");
                    }
                    Console.WriteLine("\b]");
                }
            }
        }
    }
}
