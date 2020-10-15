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

        public ChessMatch() 
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            FinishedMatch = false;
            PutPieces();
        }

        public void ExecuteMoviment(Position origin, Position destination)
        {
            Piece piece = Board.removePieceFromPosition(origin);
            piece.IncrementMovementQuantity();
            Piece capturedPiece = Board.removePieceFromPosition(destination);
            Board.SetPieceInPosition(piece, destination);

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

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        private void PutPieces()
        {
            Board.SetPieceInPosition(new Tower(Board, Color.White), new PositionChess('c',1).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.White), new PositionChess('c', 2).ToBoardPosition());
            Board.SetPieceInPosition(new King(Board, Color.White), new PositionChess('d', 1).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.White), new PositionChess('d', 2).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.White), new PositionChess('e', 1).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.White), new PositionChess('e', 2).ToBoardPosition());
            

            Board.SetPieceInPosition(new Tower(Board, Color.Black), new PositionChess('c', 7).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.Black), new PositionChess('c', 8).ToBoardPosition());
            Board.SetPieceInPosition(new King(Board, Color.Black), new PositionChess('d', 8).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.Black), new PositionChess('d', 7).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.Black), new PositionChess('e', 8).ToBoardPosition());
            Board.SetPieceInPosition(new Tower(Board, Color.Black), new PositionChess('e', 7).ToBoardPosition());


        }
    }
}
