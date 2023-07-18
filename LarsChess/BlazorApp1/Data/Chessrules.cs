using BlazorApp1.Models;

namespace BlazorApp1.Data
{
    public static class Chessrules
    {
        public static Dictionary<(int, int), (PieceClass, Colour)> StartingPositions { get; private set; } = new Dictionary<(int, int), (PieceClass, Colour)>
        {
            { (0,0), (PieceClass.Rook, Colour.White) },
            { (1,0), (PieceClass.Bishop, Colour.White) },
            { (2,0), (PieceClass.Knight, Colour.White) },
            { (3,0), (PieceClass.Queen, Colour.White) },
            { (4,0), (PieceClass.King, Colour.White) },
            { (5,0), (PieceClass.Knight, Colour.White) },
            { (6,0), (PieceClass.Bishop, Colour.White) },
            { (7,0), (PieceClass.Rook, Colour.White) },
            { (0,1), (PieceClass.Pawn, Colour.White) },
            { (1,1), (PieceClass.Pawn, Colour.White) },
            { (2,1), (PieceClass.Pawn, Colour.White) },
            { (3,1), (PieceClass.Pawn, Colour.White) },
            { (4,1), (PieceClass.Pawn, Colour.White) },
            { (5,1), (PieceClass.Pawn, Colour.White) },
            { (6,1), (PieceClass.Pawn, Colour.White) },
            { (7,1), (PieceClass.Pawn, Colour.White) },
            
            { (0,6), (PieceClass.Pawn, Colour.Black) },            
            { (1,6), (PieceClass.Pawn, Colour.Black) },
            { (2,6), (PieceClass.Pawn, Colour.Black) },
            { (3,6), (PieceClass.Pawn, Colour.Black) },
            { (4,6), (PieceClass.Pawn, Colour.Black) },
            { (5,6), (PieceClass.Pawn, Colour.Black) },
            { (6,6), (PieceClass.Pawn, Colour.Black) },
            { (7,6), (PieceClass.Pawn, Colour.Black) },
            { (0,7), (PieceClass.Rook, Colour.Black) },
            { (1,7), (PieceClass.Bishop, Colour.Black) },
            { (2,7), (PieceClass.Knight, Colour.Black) },
            { (3,7), (PieceClass.Queen, Colour.Black) },
            { (4,7), (PieceClass.King, Colour.Black) },
            { (5,7), (PieceClass.Knight, Colour.Black) },
            { (6,7), (PieceClass.Bishop, Colour.Black) },
            { (7,7), (PieceClass.Rook, Colour.Black) },
        };
    
        public static List<(int,int)> GetLegalMoves(PieceModel model)
        {
            if (model is null)
                return new List<(int, int)>();

            var moves = new List<(int,int)>();
            var p = model.Position;
            switch(model.PieceClass)
            {
                case PieceClass.King:
                    if (p.x < 7 && p.y>1)   moves.Add((p.x + 1, p.y - 1));
                    if (p.x < 7)            moves.Add((p.x + 1, p.y));
                    if (p.x < 7 && p.y <7)  moves.Add((p.x + 1, p.y + 1));

                    if (p.x > 1 && p.y > 1) moves.Add((p.x - 1, p.y - 1));
                    if (p.x > 1)            moves.Add((p.x - 1, p.y));
                    if (p.x > 1 && p.y < 7) moves.Add((p.x - 1, p.y + 1));

                    if (p.y < 7)            moves.Add((p.x, p.y + 1));
                    if (p.y > 1)            moves.Add((p.x, p.y - 1));
                    break;

            }
            return moves;
        }
        
    }
}
