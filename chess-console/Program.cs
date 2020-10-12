using board;
using System;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.printBoard(board);

            Console.ReadLine();
        }
    }
}
