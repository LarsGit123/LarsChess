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
    
        public static List<(int,int)> GetLegalMoves(PieceModel model, List<PieceModel> allPieces)
        {
            if (model is null)
                return new List<(int, int)>();

            var p = model.Position;
            switch(model.PieceClass)
            {
                case PieceClass.King:
                    return GetKingMoves(model, allPieces);
                case PieceClass.Pawn:
                    return GetPawnMoves(model, allPieces);
                case PieceClass.Rook:
                    return GetRookMoves(model, allPieces);
                default:
                    return new List<(int, int)>();
            }
            
        }

        private static List<(int, int)> GetRookMoves(PieceModel model, List<PieceModel> allPieces)
        {
            //x-right
            var rookXright = model.Position.x;
            while(rookXright < 7)
            {

                if(allPieces.FirstOrDefault(p => p.Position == (rookXright +1, model.Position.y)) is PieceModel xRightModel)
                {
                    //same colour
                    if(xRightModel.Colour == model.Colour)
                    {
                        break;
                    }
                    else 
                    {
                        rookXright++;
                        break; 
                    }
                }
                rookXright++;
            }

            //x-left
            var rookXLeft = model.Position.x;
            while (rookXLeft > 0)
            {
                if (allPieces.FirstOrDefault(p => p.Position == (rookXright - 1, model.Position.y)) is PieceModel xLeftModel)
                {
                    //same colour
                    if (xLeftModel.Colour == model.Colour)
                    {
                        break;
                    }
                    else
                    {
                        rookXLeft--;
                        break;
                    }
                }
                rookXLeft--;
            }

            var rangeX = Enumerable.Range(rookXLeft,rookXright-rookXLeft+1).Where(r => r != model.Position.x);

            //y-up
            var rookYup = model.Position.y;
            while (rookYup < 7)
            {
                
                if (allPieces.FirstOrDefault(p => p.Position == (model.Position.x,rookYup + 1 )) is PieceModel rookYupModel)
                {
                    //same colour
                    if (rookYupModel.Colour == model.Colour)
                    {
                        break;
                    }
                    else
                    {
                        rookYup++;
                        break;
                    }
                }
                rookYup++;
            }

            //y-down
            var rookYdown = model.Position.y;
            while (rookYdown > 0)
            {
                
                if (allPieces.FirstOrDefault(p => p.Position == (model.Position.x, rookYdown - 1)) is PieceModel ydownModel)
                {
                    //same colour
                    if (ydownModel.Colour == model.Colour)
                    {
                        break;
                    }
                    else
                    {
                        rookYdown--;
                        break;
                    }
                }
                rookYdown--;
            }

            var rangeY = Enumerable.Range(rookYdown, rookYup-rookYdown+1).Where(r=> r != model.Position.y);
            var legal = new List<(int x , int y)>();
            legal.AddRange(rangeX.Select(x => (x, model.Position.y)));
            legal.AddRange(rangeY.Select(y=> (model.Position.x, y)));
            
            return legal;
        }

        private static List<(int, int)> GetPawnMoves(PieceModel payload, List<PieceModel> allPieces)
        {
            var moveDirection = (payload.Colour == Colour.White)
                ? 1
                : -1;
            var moves = new List<(int, int)>();
            if (payload.Position.y <7 && payload.Position.y  > 0)
            {
                if (!allPieces.Any(p => p.Position.y == payload.Position.y + moveDirection && p.Position.x == payload.Position.x))
                {
                    moves.Add((payload.Position.x, payload.Position.y + moveDirection));

                    //first move
                    if (!allPieces.Any(p => p.Position.y == payload.Position.y + moveDirection * 2))
                    {
                        if (payload.Colour == Colour.White && payload.Position.y == 1)
                        {
                            moves.Add((payload.Position.x, payload.Position.y + moveDirection * 2));

                        }
                        else if (payload.Colour == Colour.Black && payload.Position.y == 6)
                        {
                            moves.Add((payload.Position.x, payload.Position.y + moveDirection * 2));

                        }
                    }
                }

                //attack moves:
                if (allPieces.Where(p=> 
                    payload.Colour != p.Colour && 
                    p.Position.y == payload.Position.y + moveDirection &&
                    p.Position.x == payload.Position.x + 1).Any())
                {
                    moves.Add((payload.Position.x + 1,  payload.Position.y + moveDirection));
                }

                if (allPieces.Where(p =>
                    payload.Colour != p.Colour &&
                    p.Position.y == payload.Position.y + moveDirection &&
                    p.Position.x == payload.Position.x - 1).Any())
                {
                    moves.Add((payload.Position.x - 1, payload.Position.y + moveDirection));
                }
            }
            return moves;
        }

        private static List<(int, int)> GetKingMoves(PieceModel payload, List<PieceModel> allPieces)
        {
            var moves = new List<(int, int)>();
            var p = payload.Position;
            if (p.x < 7 && p.y > 1 && PositionIsFree(payload, allPieces, (p.x + 1, p.y - 1))) 
                moves.Add((p.x + 1, p.y - 1));
            if (p.x < 7 && PositionIsFree(payload, allPieces, (p.x + 1, p.y ))) 
                moves.Add((p.x + 1, p.y));
            if (p.x < 7 && p.y < 7 && PositionIsFree(payload, allPieces, (p.x + 1, p.y + 1))) 
                moves.Add((p.x + 1, p.y + 1));

            if (p.x > 0 && p.y > 1 && PositionIsFree(payload, allPieces, (p.x - 1, p.y - 1))) 
                moves.Add((p.x - 1, p.y - 1));
            if (p.x > 0 && PositionIsFree(payload, allPieces, (p.x - 1, p.y))) 
                moves.Add((p.x - 1, p.y));
            if (p.x > 0 && p.y < 7 && PositionIsFree(payload, allPieces, (p.x - 1, p.y + 1))) 
                moves.Add((p.x - 1, p.y + 1));

            if (p.y < 7 && PositionIsFree(payload, allPieces, (p.x, p.y + 1))) 
                moves.Add((p.x, p.y + 1));
            if (p.y > 1 && PositionIsFree(payload, allPieces, (p.x, p.y - 1))) 
                moves.Add((p.x, p.y - 1));
            return moves;
        }

        private static bool PositionIsFree(PieceModel piece, List<PieceModel> allPieces, (int x, int y) p )
        {
            return allPieces.Any(a => a.PieceClass == piece.PieceClass && a.Position == p);
        }
    }
}
