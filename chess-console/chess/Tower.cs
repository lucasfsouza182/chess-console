using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "T";
        }

        public override bool[,] AvailableMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position positionBoard = new Position(0, 0);

            //north position
            positionBoard.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if(Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.Line -= 1;
            }

            //right position
            positionBoard.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.Column += 1;
            }

            //south position
            positionBoard.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.Line += 1;
            }

            //left position
            positionBoard.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.Column -= 1;
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
