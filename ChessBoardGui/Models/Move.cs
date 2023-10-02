using System.Reflection;
using System.Runtime.CompilerServices;

namespace ChessBoardGui.Models
{
    public class Move
    {
        public int PieceId { get; set; }
        public PieceClass Piece { get; set; }
        public (int x, int y) OriginSquare { get; set; }
        public (int x, int y) DestinationSquare { get; set; }
        public bool IsCapture { get; set; }
        public int CapturedPieceId { get; set; }
        public int MoveNumber { get; set; }
        public Colour Player { get; set; }

        
    }

    public static class MoveHelper
    {
        public static string GetPGN(this Move move)
        {
            string prefix = string.Empty;
            if (move.Player == Colour.White)
            {
                prefix = move.MoveNumber.ToString() + ". ";
            }
            var pgn= string.Empty;
            if (move.Piece == PieceClass.Pawn)
            {
                if (move.Piece. == model.Position.x)
                {
                    move = _rules.Xcoords[model.Position.x].ToLower() + (model.Position.y + 1);
                }
                else
                {
                    move = $"{_rules.Xcoords[model.OrgPosition.x].ToLower()}x{_rules.Xcoords[model.Position.x].ToLower()}{model.Position.y + 1}";
                }
            }
        }
    }

}
