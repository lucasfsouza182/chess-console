using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)  { }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] AvailableMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position positionBoard = new Position(0, 0);

            //north position
            positionBoard.SetValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //northeast position
            positionBoard.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //right position
            positionBoard.SetValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //southeast position
            positionBoard.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //south position
            positionBoard.SetValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //southwest position
            positionBoard.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //left position
            positionBoard.SetValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            //northwest position
            positionBoard.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }

            return mat;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.piece(position);
            return piece == null || piece.Color != Color;
        }
    }
}
