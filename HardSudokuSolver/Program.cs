using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class Program
    {
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
        static string unSolvableSudoku = "666090020100000000070001800015300002000604000800005430003400050000000009050070060";

        /*
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(mediumSodoku);
        }
        */
       // class Program
       // {
            static void Main(string[] args)
            {
            Sudoku game = new Sudoku(expertSodoku);
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
       // }
    }
}
