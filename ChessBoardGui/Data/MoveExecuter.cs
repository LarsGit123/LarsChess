using ChessBoardGui.Models;
using ChessBoardGui.Shared;
using System.Reflection;

namespace ChessBoardGui.Data
{
    public class MoveExecuter
    {
        private Chessrules _rules { get; }
        public List<string> Moves { get; set; } = new List<string>();
        private int moveCounter = 0;
        public int BoardPositionIndex { get; set; } = 0;


        public MoveExecuter(Chessrules rules)
        {
            _rules = rules;
        }

        public void RevertMove(List<PieceModel> allPieces, List<PieceModel> capturedPieces)
        {
            BoardPositionIndex--;
            var move = Moves.ElementAt(BoardPositionIndex);
            var player =  int.TryParse(move.Substring(0,1), out moveCounter)
                ? Colour.White 
                : Colour.Black;

            var moveTo = move.Substring(move.Length - 2);

            var cleanedMove = move.Replace("1. ", "").Replace("x", "");

            (int x, int y) toPosition = GetPosition(moveTo);
            (int x, int y) fromPosition;

            if (cleanedMove.Replace(moveTo, "").Length < 2)
            {
                //pawn move eg 1. d5
                //todo: how to determine single or double pawn move?
                fromPosition = (toPosition.x, toPosition.y - 2);
                throw new Exception();
            }
            else
            {
                var moveFrom = cleanedMove.Substring(0, move.Length - 3);
                fromPosition = GetPosition(moveFrom);
            }
            
            var isCapture = move.Contains("x");
            var PieceToRevert =  allPieces.First(p => p.Position == toPosition);
            PieceToRevert.Position = fromPosition;
            

            if(isCapture)
            {
                var capturedPiece = capturedPieces.Last();
                capturedPiece.Position = toPosition;
                allPieces.Add(capturedPiece);
                capturedPieces.Remove(capturedPiece);
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
        public void UpdateMovesState(PieceModel model)
        {
            string prefix = string.Empty;
            if (model.Colour == Colour.White)
            {
                moveCounter++;
                prefix = moveCounter.ToString() + ". ";
            }
            var move = string.Empty;
            if (model.PieceClass == PieceClass.Pawn)
            {
                if (model.OrgPosition.x == model.Position.x)
                {
                    move = _rules.Xcoords[model.Position.x].ToLower() + (model.Position.y + 1);
                }
                else
                {
                    move = $"{_rules.Xcoords[model.OrgPosition.x].ToLower()}x{_rules.Xcoords[model.Position.x].ToLower()}{model.Position.y + 1}";
                }
            }

            Moves.Add(prefix + move);
            model.OrgPosition = model.Position;
            BoardPositionIndex = Moves.Count();

        
        }
    }
}
