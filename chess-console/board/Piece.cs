using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            MovementQuantity = 0;
            Board = board;
        }

        public void IncrementMovementQuantity()
        {
            MovementQuantity++;
        }

        public bool CanMoveTo(Position position)
        {
            return AvailableMoviments()[position.Line, position.Column];
        }

        public bool ExistsPossibleMovements()
        {
            bool[,] mat = AvailableMoviments();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] AvailableMoviments();
    }
}
