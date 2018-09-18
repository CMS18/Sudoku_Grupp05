using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuEliteSolver
{
    class Program
    {
        static List<int>[,] possibleValue = new List<int>[9, 9];
        static int[,] sodokuBoard = new int[9, 9]; // tmpBoard
        static string[] intermittentSodokuRowHolder = new string[9];
        static string[] easySodoku = new string[5];

        static void initSodokuStrings()
        {
            easySodoku[0] = "000000307048720010005014008250003876000000000671200039500840700030052980106000000";
            easySodoku[1] = "409120030050903100000040089910004007020010060600800051780030000004709010090065408";
            easySodoku[2] = "060001020970820400035004001604000018007000200820000605700900130002067094040500080";
            easySodoku[3] = "108069200062071300300800010000057069000000000510690000040003002009540130001980507";
            easySodoku[4] = "001290375020000961090300020000027100000805000002640000080009050453000090269053400";
        }

        static string mediumSodoku = "000300000001080070980007610007900040204000308060008900075800031020060700000004000";
        static string hardSodoku = "000004800590200004100800090001000508070000040309000100030001002200008067004900000";
        static string expertSodoku = "060090020100000000070001800015300002000604000800005430003400050000000009050070060";

        static void InitValues()
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

        static void InitIntermittentSodokuRowHolder(string sodoku)
        {
            int index = 0;
            const int length = 9;
            int i = 0;
            for (int row = 1; row <= 9; row++)
            {
                intermittentSodokuRowHolder[i] = sodoku.Substring(index, length);
                i++;
                index += length;
            }
        }

        static void InitSodokuBoard()
        {
            // static int[,] sodokuBoard = new int[9,9]; // tmpBoard
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sodokuBoard[row, col] = int.Parse(("" + intermittentSodokuRowHolder[row][col]));
                }
            }
        }

        static void DisplaySodokuBoard()
        {
            Console.WriteLine("-------------------------");
            for (int row = 0; row < 9; row++)
            {
                Console.Write("|");
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(" " + sodokuBoard[row, col]);
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

        static void UpdatePossibleValuesForBasedOnInitialValues()
        {
            // Om värderna har fastställts ska alla potentiella värden tas bort
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sodokuBoard[row, col] != 0)
                    {
                        possibleValue[row, col].Clear(); // Alla possibleValues tas bort för den fastställda rutan
                    }
                }
            }
        }

        // SodokuSolvern
        // possibleValue
        // sodokuBoard
        static List<int> GetRowValues(int row)
        {
            List<int> values = new List<int>();
            for (int col = 0; col < 9; col++)
            {
                if (sodokuBoard[row, col] != 0) values.Add(sodokuBoard[row, col]);
            }
            return values;
        }
        static List<int> GetColumnValues(int col)
        {
            List<int> values = new List<int>();
            for (int row = 0; row < 9; row++)
            {
                if (sodokuBoard[row, col] != 0) values.Add(sodokuBoard[row, col]);
            }
            return values;
        }
        static List<int> GetBoxValues(int row, int col)
        {
            List<int> values = new List<int>();

            int xStart = (row / 3) * 3;
            int yStart = (col / 3) * 3;
            for (int x = xStart; x < xStart + 3; x++)
            {
                for (int y = yStart; y < yStart + 3; y++)
                {
                    //if (sodokuBoard[rows[x], cols[y]] != 0) values.Add(sodokuBoard[rows[x], cols[y]]);
                    if (sodokuBoard[x, y] != 0) values.Add(sodokuBoard[x, y]);
                    //if (sodokuBoard[rows[x], cols[y]] != 0) values.Add(sodokuBoard[x, y]);
                }
            }
            return values;
        }
        static void Solve()
        {
            List<int> unavailableValues = new List<int>();

            bool wasUpdated = true;
            for (int iteration = 0; wasUpdated; iteration++)
            {
                wasUpdated = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
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


                        /*
                        unavailableValues.AddRange(GetRowValues(row));
                        unavailableValues.AddRange(GetColumnValues(col));
                        unavailableValues.AddRange(GetBoxValues(row, col));
                        */
                        // Uppdaterar brädet och potentiella värden



                        for (int i = 1; i <= 9; i++)
                        {
                            if (unavailableValues.Contains(i))
                            {
                                possibleValue[row, col].Remove(i);
                            }
                        }
                        if (possibleValue[row, col].Count == 1)
                        {
                            sodokuBoard[row, col] = possibleValue[row, col][0];
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

        static void Main(string[] args)
        {
            initSodokuStrings();
            for (int i = 0; i < 5; i++)
            {
                InitValues(); // Sätter alla möjliga värden för varje enskild ruta i sodokubrädet till en modifierbarlista med talen 1-9
                InitIntermittentSodokuRowHolder(easySodoku[i]);
                InitSodokuBoard();
                DisplaySodokuBoard();
                UpdatePossibleValuesForBasedOnInitialValues(); // Tar bort possibleValue-listan för varje ruta som != 0

                Solve();
                DisplaySodokuBoard();
                PrintPossibleValues();

                Console.ReadKey();
            }
        }

        private static void PrintPossibleValues()
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
