namespace BlazorApp1.Models
{
    public enum PieceClass { King, Queen, Bishop, Knight, Rook, Pawn}
    public enum Colour { White, Black}
    public class PieceModel
    {
        public Colour Colour { get; set; }
        public PieceClass PieceClass { get; set; }
        public string Id { get; set; }
        public int[] Position {  get; set; }

        public int[] OrgPosition { get; set; }

        public PieceModel(int[] position, string id, PieceClass pieceClass, Colour colour)
        {
            Position = position;
            OrgPosition = position;
            Id = id;
            PieceClass = pieceClass;
            Colour = colour;
        }
    }
    
}
