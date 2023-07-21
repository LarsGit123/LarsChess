namespace ChessBoardGui.Models
{
    public class SquareModel
    {
        public SquareModel((int x, int y) position)
        {
            Position = position;
        }
        public (int x, int y) Position { get; set; }
        public bool IsLegalSquare { get; set; } = false;
    }
}
