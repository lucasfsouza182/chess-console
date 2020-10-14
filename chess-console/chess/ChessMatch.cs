using board;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finishedMatch { get; private set; }

        public ChessMatch() 
        {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finishedMatch = false;
            PutPieces();
        }

        public void ExecuteMoviment(Position origin, Position destination)
        {
            Piece piece = Board.removePieceFromPosition(origin);
            piece.IncrementMovementQuantity();
            Piece capturedPiece = Board.removePieceFromPosition(destination);
            Board.SetPieceInPosition(piece, destination);

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
