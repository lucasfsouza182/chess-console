using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class King : Piece
    {
        private ChessMatch match;

        public King(Board board, Color color) : base(board, color)  { }

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

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

            // #Castle move
            if (MovementQuantity == 0 && !match.CheckMate)
            {
                // #Castle Kingside
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (TestTowerForCastleMove(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.piece(p1) == null && Board.piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                // #Castle Queenside
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (TestTowerForCastleMove(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.piece(p1) == null && Board.piece(p2) == null && Board.piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }

        private bool TestTowerForCastleMove(Position position)
        {
            Piece p = Board.piece(position);
            return p != null && p is Tower && p.Color == Color && p.MovementQuantity == 0;
        }


        private bool CanMove(Position position)
        {
            Piece piece = Board.piece(position);
            return piece == null || piece.Color != Color;
        }
    }
}
