using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        public override bool[,] AvailableMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position positionBoard = new Position(0, 0);

            positionBoard.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
            }
            positionBoard.SetValues(Position.Line + 1, Position.Column - 2);
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
