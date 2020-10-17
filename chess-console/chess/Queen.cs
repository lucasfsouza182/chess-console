using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "D";
        }

        public override bool[,] AvailableMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position positionBoard = new Position(0, 0);

            //left position
            positionBoard.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line, positionBoard.Column - 1);
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
                positionBoard.SetValues(positionBoard.Line, positionBoard.Column + 1);
            }

            //north position
            positionBoard.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column);
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
                positionBoard.SetValues(positionBoard.Line + 1, positionBoard.Column);
            }

            //northwest position
            positionBoard.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column - 1);
            }

            //northeast position
            positionBoard.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column + 1);
            }

            //southeast position
            positionBoard.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line + 1, positionBoard.Column + 1);
            }

            //southwest position
            positionBoard.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(positionBoard) && CanMove(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line + 1, positionBoard.Column - 1);
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
