using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color Color) : base(board, Color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool podeMover(Position positionBoard)
        {
            Piece piece = Board.piece(positionBoard);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] AvailableMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position positionBoard = new Position(0, 0);

            //northwest position
            positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column - 1);
            while (Board.ValidPosition(positionBoard) && podeMover(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column - 1);
            }

            //northeast position
            positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column + 1);
            while (Board.ValidPosition(positionBoard) && podeMover(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line - 1, positionBoard.Column + 1);
            }

            //southeast position
            positionBoard.SetValues(positionBoard.Line + 1, positionBoard.Column + 1);
            while (Board.ValidPosition(positionBoard) && podeMover(positionBoard))
            {
                mat[positionBoard.Line, positionBoard.Column] = true;
                if (Board.piece(positionBoard) != null && Board.piece(positionBoard).Color != Color)
                {
                    break;
                }
                positionBoard.SetValues(positionBoard.Line + 1, positionBoard.Column + 1);
            }

            //southwest position
            positionBoard.SetValues(positionBoard.Line + 1, positionBoard.Column - 1);
            while (Board.ValidPosition(positionBoard) && podeMover(positionBoard))
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
    }
}