namespace ServerChessBoard2.Models
{
    public class PieceModel
    {
        public int Id { get; set; }
        public PieceStatuses Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public enum PieceStatuses
    {
        //Todo,
        //Started,
        //InProgress,
        //Completed
        //Done
        
        TODO,
        INPROGRESS,
        COMPLETED,
        DONE


    }
}
