using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }
        public Pawn(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "P";
        }

        public override bool[,] AvailableMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position positionBoard = new Position(0, 0);


            if (Color == Color.White)
            {
                positionBoard.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(positionBoard) && Free(positionBoard))
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }
                positionBoard.SetValues(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(p2) && Free(p2) && Board.ValidPosition(positionBoard) && Free(positionBoard) && MovementQuantity == 0)
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }
                positionBoard.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(positionBoard) && ExistsOpponent(positionBoard))
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }
                positionBoard.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(positionBoard) && ExistsOpponent(positionBoard))
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }

                // #en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistsOpponent(left) && Board.piece(left) == match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistsOpponent(right) && Board.piece(right) == match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                positionBoard.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(positionBoard) && Free(positionBoard))
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }
                positionBoard.SetValues(Position.Line + 2, Position.Column);
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(p2) && Free(p2) && Board.ValidPosition(positionBoard) && Free(positionBoard) && MovementQuantity == 0)
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }
                positionBoard.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(positionBoard) && ExistsOpponent(positionBoard))
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }
                positionBoard.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(positionBoard) && ExistsOpponent(positionBoard))
                {
                    mat[positionBoard.Line, positionBoard.Column] = true;
                }

                // #en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistsOpponent(left) && Board.piece(left) == match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistsOpponent(right) && Board.piece(right) == match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }

        private bool ExistsOpponent(Position positionBoard)
        {
            Piece p = Board.piece(positionBoard);
            return p != null && p.Color != Color;
        }

        private bool Free(Position positionBoard)
        {
            return Board.piece(positionBoard) == null;
        }
    }
}
