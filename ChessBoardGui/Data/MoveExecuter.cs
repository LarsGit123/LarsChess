using ChessBoardGui.Models;
using ChessBoardGui.Shared;
using System.Reflection;

namespace ChessBoardGui.Data
{
    public class MoveExecuter
    {
        private Chessrules _rules { get; }
        public Stack<Move> Moves { get; set; } = new Stack<Move>();
        private int moveCounter = 0;
        public int BoardPositionIndex { get; set; } = 0;


        public MoveExecuter(Chessrules rules)
        {
            _rules = rules;
        }

        public void RevertMove(List<PieceModel> allPieces, Stack<PieceModel> capturedPieces)
        {

            BoardPositionIndex--;
            var move = Moves.Pop();
            var player =  move.Player;

            allPieces.First(piece => piece.Id == move.PieceId).Position = move.OriginSquare;

            if (move.IsCapture)
            {
                var capturedPiece = capturedPieces.Pop();
                capturedPiece.Position = move.DestinationSquare;
                allPieces.Add(capturedPiece);
                allPieces.First(piece => piece.Id == capturedPiece.Id).Position = move.DestinationSquare;
            }
         
        }

        public void UndoRevertMove(List<PieceModel> allPieces)
        {
            BoardPositionIndex++;
            var revertMove = Moves.ElementAt(BoardPositionIndex);
        }
        public void PerformMove(List<Piece> allPieces, List<Piece> capturedPieces, string move)
        {

        }
        public void UpdateMovesState(Move move)
        {
            move.UpdateOriginPosition();
            Moves.Push(move);            
            BoardPositionIndex = Moves.Count();
        }
    }
}
