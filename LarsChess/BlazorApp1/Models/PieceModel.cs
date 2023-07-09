namespace BlazorApp1.Models
{
    public enum PieceClass { King, Queen, Bishop, Knight, Rook, Pawn}
    public class PieceModel
    {
        public PieceClass Class { get; set; }
        public int Id { get; set; }
        public int[] Position {  get; set; }
    }
}
