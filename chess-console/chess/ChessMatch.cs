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
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch() 
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            FinishedMatch = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PutPieces();
        }

        public void ExecuteMoviment(Position origin, Position destination)
        {
            Piece piece = Board.removePieceFromPosition(origin);
            piece.IncrementMovementQuantity();
            Piece capturedPiece = Board.removePieceFromPosition(destination);
            Board.SetPieceInPosition(piece, destination);

            if(capturedPiece != null) 
            {
                capturedPieces.Add(capturedPiece);
            }
        }

        public void PerformeMove(Position origin, Position destination)
        {
            ExecuteMoviment(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position position)
        {
            if(Board.piece(position) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position!");
            }
            if(CurrentPlayer != Board.piece(position).Color)
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
            foreach (Piece p in capturedPieces)
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
            foreach(Piece p in capturedPieces)
            {
                if (p.Color == color)
                    aux.Add(p);
            }
            return aux;
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
