namespace BlazorApp1.Models
{
    public enum PieceClass { King, Queen, Bishop, Knight, Rook, Pawn}
    public class PieceModel
    {
        public PieceClass Class { get; set; }
        public string Id { get; set; }
        public int[] Position {  get; set; }

        public int[] OrgPosition { get; set; }
    }
}
