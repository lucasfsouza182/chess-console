using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess_console
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for(int i=0; i < board.Lines; i++)
            {
                for (int j=0; j < board.Columns; j++)
                {
                    if(board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}
