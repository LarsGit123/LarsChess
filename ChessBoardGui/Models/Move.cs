using ChessBoardGui.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ChessBoardGui.Models
{
    public class Move
    {
        private readonly Chessrules _rules;
        private readonly PieceModel? _capturePiece;

        public string PieceId { get; set; }
        public PieceClass Piece { get; set; }
        public (int x, int y) OriginSquare { get; set; }
        public (int x, int y) DestinationSquare { get; set; }
        public int? CapturePieceId { get; }
        public bool IsCapture { get; set; }
        public Colour Player { get; set; }
        private readonly PieceModel _model;

        public Move(Chessrules chessrules, PieceModel model, PieceModel? capturePiece)
        {
            _rules = chessrules;
            PieceId = model.Id;
            Piece = model.PieceClass;
            OriginSquare = model.OrgPosition;
            DestinationSquare = model.Position;
            IsCapture = capturePiece is not null;
            _capturePiece = capturePiece;
            Player = model.Colour;
            _model = model;
        }

        public string GetPgn(int moveCounter)
        {
            string prefix = string.Empty;
            if (Player == Colour.White)
            {
                prefix = moveCounter.ToString() + ". ";
            }
            var pgn = string.Empty;
            if (Piece == PieceClass.Pawn)
            {
                if (OriginSquare.x == DestinationSquare.x)
                {
                    pgn = _rules.Xcoords[DestinationSquare.x].ToLower() + (DestinationSquare);
                }
                else
                {
                    pgn = $"{_rules.Xcoords[OriginSquare.x].ToLower()}x{_rules.Xcoords[DestinationSquare.x].ToLower()}{DestinationSquare.y}";
                }
            }
            return prefix + pgn;
        }

        internal void UpdateOriginPosition()
        {
            _model.OrgPosition = _model.Position;
        }
    }
}
