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

            
            

            if(move.IsCapture)
            {
                var capturedPiece = capturedPieces.Pop();
                capturedPiece.Position = move.DestinationSquare;
                allPieces.Add(capturedPiece);
                allPieces.First(piece => piece.Id == move.PieceId).Position = move.OriginSquare;
            }
         
        }

        private (int x, int y) GetPosition(string moveTo)
        {
            var x = Array.IndexOf(_rules.Xcoords, moveTo[..1].ToUpper());
            var y = int.Parse(moveTo.Substring(1, 1));
            return (x, y);
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
