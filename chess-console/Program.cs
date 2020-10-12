using board;
using chess;
using System;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.SetPieceInPosition(new Tower(board, Color.Black), new Position(0, 0));
                board.SetPieceInPosition(new Tower(board, Color.Black), new Position(1, 3));
                board.SetPieceInPosition(new King(board, Color.Black), new Position(0, 2));

                board.SetPieceInPosition(new Tower(board, Color.White), new Position(3, 5));

                Screen.PrintBoard(board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
