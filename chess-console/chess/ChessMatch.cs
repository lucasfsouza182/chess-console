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
        public bool CheckMate { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            FinishedMatch = false;
            CheckMate = false;
            VulnerableEnPassant = null;
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

            // #Castle Kingside
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinationT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.removePieceFromPosition(originT);
                T.IncrementMovementQuantity();
                Board.SetPieceInPosition(T, destinationT);
            }

            // #Castle Queenside
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinationT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.removePieceFromPosition(originT);
                T.IncrementMovementQuantity();
                Board.SetPieceInPosition(T, destinationT);
            }

            // #jogadaespecial en passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position posP;
                    if (piece.Color == Color.White)
                    {
                        posP = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        posP = new Position(destination.Line - 1, destination.Column);
                    }
                    capturedPiece = Board.removePieceFromPosition(posP);
                    capturedPieces.Add(capturedPiece);
                }
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

            // #Castle Kingside
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinationT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.removePieceFromPosition(destinationT);
                T.DecrementMovementQuantity();
                Board.SetPieceInPosition(T, originT);
            }

            // #Castle Queenside
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinationT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.removePieceFromPosition(destinationT);
                T.DecrementMovementQuantity();
                Board.SetPieceInPosition(T, originT);
            }

            // #en passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.removePieceFromPosition(destination);
                    Position posP;
                    if (piece.Color == Color.White)
                    {
                        posP = new Position(3, destination.Column);
                    }
                    else
                    {
                        posP = new Position(4, destination.Column);
                    }
                    Board.SetPieceInPosition(pawn, posP);
                }
            }
        }

        public void PerformeMove(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMoviment(origin, destination);

            if(IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }

            CheckMate = IsInCheck(Opponent(CurrentPlayer));

            if (TestCheckMate(Opponent(CurrentPlayer)))
            {
                FinishedMatch = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            Piece p = Board.piece(destination);

            // #en passant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
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
            if (!Board.piece(origin).PossibleMovements(destination))
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

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.AvailableMoviments();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMoviment(origin, destination);
                            bool isCheckMate = IsInCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!isCheckMate)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            PutNewPiece('a', 1, new Tower(Board, Color.White));
            PutNewPiece('b', 1, new Knight(Board, Color.White));
            PutNewPiece('c', 1, new Bishop(Board, Color.White));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('e', 1, new King(Board, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.White));
            PutNewPiece('g', 1, new Knight(Board, Color.White));
            PutNewPiece('h', 1, new Tower(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PutNewPiece('a', 8, new Tower(Board, Color.Black));
            PutNewPiece('b', 8, new Knight(Board, Color.Black));
            PutNewPiece('c', 8, new Bishop(Board, Color.Black));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('e', 8, new King(Board, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.Black));
            PutNewPiece('g', 8, new Knight(Board, Color.Black));
            PutNewPiece('h', 8, new Tower(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
