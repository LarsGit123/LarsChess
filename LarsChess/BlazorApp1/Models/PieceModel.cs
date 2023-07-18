namespace BlazorApp1.Models
{
    public enum PieceClass { King, Queen, Bishop, Knight, Rook, Pawn}
    public enum Colour { White, Black}
    public class PieceModel
    {
        public Colour Colour { get; set; }
        public PieceClass PieceClass { get; set; }
        public string Id { get; set; }
        public (int x,int y) Position {  get; set; }

        public (int x, int y) OrgPosition { get; set; }

        public PieceModel((int x, int y) position, string id, PieceClass pieceClass, Colour colour)
        {
            Position = position;
            OrgPosition = position;
            Id = id;
            PieceClass = pieceClass;
            Colour = colour;
        }

        public PieceModel Clone((int,int) newPosition)
        {
            return new PieceModel(newPosition, this.Id, this.PieceClass, this.Colour);
        }
    }
    
}
