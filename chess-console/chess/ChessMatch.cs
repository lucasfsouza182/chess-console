using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool FinishedMatch { get; private set; }
        public bool Check { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            FinishedMatch = false;
            Check = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PutPieces();
        }

        public Piece ExecuteMoviment(Position origin, Position destination)
        {
            Piece piece = Board.removePieceFromPosition(origin);
            piece.IncrementMovementQuantity();
            Piece capturedPiece = Board.removePieceFromPosition(destination);
            Board.SetPieceInPosition(piece, destination);

            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = Board.removePieceFromPosition(destination);
            piece.DecrementMovementQuantity();

            if (capturedPiece != null)
            {
                Board.SetPieceInPosition(capturedPiece, destination);
                capturedPieces.Remove(capturedPiece);
            }
            Board.SetPieceInPosition(piece, origin);
        }

        public void PerformeMove(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMoviment(origin, destination);

            if(IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }

            Check = IsInCheck(Opponent(CurrentPlayer));

            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.piece(position) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position!");
            }
            if (CurrentPlayer != Board.piece(position).Color)
            {
                throw new BoardException("The chosen origin piece is not yours!");
            }
            if (!Board.piece(position).ExistsPossibleMovements())
            {
                throw new BoardException("There are not possible movements for the chosen piece!");
            }
        }

        public void ValidateTargetPosition(Position origin, Position destination)
        {
            if (!Board.piece(origin).CanMoveTo(destination))
            {
                throw new BoardException("The target position is not valid!");
            }
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.Color == color)
                    aux.Add(p);
            }
            aux.ExceptWith(CapturedPiecesByColor(color));
            return aux;
        }

        public HashSet<Piece> CapturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in capturedPieces)
            {
                if (p.Color == color)
                    aux.Add(p);
            }
            return aux;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
                throw new BoardException("There is not a king with color " + color + " on board!");

            foreach (Piece p in PiecesInGame(Opponent(color)))
            {
                bool[,] mat = p.AvailableMoviments();
                if (mat[king.Position.Line, king.Position.Column])
                    return true;
            }
            return false;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Board.SetPieceInPosition(piece, new PositionChess(column, line).ToBoardPosition());
            pieces.Add(piece);
        }
        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        private Color Opponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece p in PiecesInGame(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        private void PutPieces()
        {
            PutNewPiece('c', 1, new Tower(Board, Color.White));
            PutNewPiece('c', 2, new Tower(Board, Color.White));
            PutNewPiece('d', 1, new King(Board, Color.White));
            PutNewPiece('d', 2, new Tower(Board, Color.White));
            PutNewPiece('e', 1, new Tower(Board, Color.White));
            PutNewPiece('e', 2, new Tower(Board, Color.White));

            PutNewPiece('c', 7, new Tower(Board, Color.Black));
            PutNewPiece('c', 8, new Tower(Board, Color.Black));
            PutNewPiece('d', 8, new King(Board, Color.Black));
            PutNewPiece('d', 7, new Tower(Board, Color.Black));
            PutNewPiece('e', 8, new Tower(Board, Color.Black));
            PutNewPiece('e', 7, new Tower(Board, Color.Black));
        }
    }
}
