using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines,columns];
        }

        public Piece piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece piece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public void SetPieceInPosition(Piece piece, Position position)
        {
            if(ExistPieceInPosition(position))
                throw new BoardException("There is already a piece in that position!");

            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece removePieceFromPosition(Position position)
        {
            if (piece(position) == null)
                return null;

            Piece aux = piece(position);
            aux.Position = null;
            Pieces[position.Line, position.Column] = null;
            return aux;
        }

        public bool ExistPieceInPosition(Position position)
        {
            ValidatePosition(position);
            return piece(position) != null;
        }

        public bool ValidPosition(Position position)
        {
            return !(position.Line < 0 || position.Line >= Lines ||
                   position.Column < 0 || position.Column >= Columns);
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
                throw new BoardException("Invalid position!");
        }
    }
}
