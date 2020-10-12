using board;
using chess;
using System;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.setPieceInPosition(new Tower(board, Color.Black), new Position(0, 0));
            board.setPieceInPosition(new Tower(board, Color.Black), new Position(1, 3));
            board.setPieceInPosition(new King(board, Color.Black), new Position(2, 4));

            Screen.printBoard(board);

            Console.ReadLine();
        }
    }
}
