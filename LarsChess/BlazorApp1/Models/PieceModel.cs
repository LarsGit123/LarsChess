namespace BlazorApp1.Models
{
    public enum PieceClass { King, Queen, Bishop, Knight, Rook, Pawn}
    public class PieceModel
    {
        public PieceClass PieceClass { get; set; }
        public string Id { get; set; }
        public int[] Position {  get; set; }

        public int[] OrgPosition { get; set; }

        public PieceModel(int[] position, string id, PieceClass pieceClass)
        {
            Position = position;
            OrgPosition = position;
            Id = id;
            PieceClass = pieceClass;
        }
    }
    
}
