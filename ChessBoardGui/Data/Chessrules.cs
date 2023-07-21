using ChessBoardGui.Models;
using MethodTimer;
using System.Reflection;

namespace ChessBoardGui.Data
{
    public class Chessrules
    {
        public Dictionary<(int, int), (PieceClass, Colour)> StartingPositions { get; private set; } = new Dictionary<(int, int), (PieceClass, Colour)>
        {
            { (0,0), (PieceClass.Rook, Colour.White) },
            { (1,0), (PieceClass.Knight, Colour.White) },
            { (2,0), (PieceClass.Bishop, Colour.White) },
            { (3,0), (PieceClass.Queen, Colour.White) },
            { (4,0), (PieceClass.King, Colour.White) },
            { (5,0), (PieceClass.Bishop, Colour.White) },
            { (6,0), (PieceClass.Knight, Colour.White) },
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
            { (1,7), (PieceClass.Knight, Colour.Black) },
            { (2,7), (PieceClass.Bishop, Colour.Black) },
            { (3,7), (PieceClass.Queen, Colour.Black) },
            { (4,7), (PieceClass.King, Colour.Black) },
            { (5,7), (PieceClass.Bishop, Colour.Black) },
            { (6,7), (PieceClass.Knight, Colour.Black) },
            { (7,7), (PieceClass.Rook, Colour.Black) },
        };

        [Time]
        public List<(int x, int y)> GetLegalMoves(PieceModel model, List<PieceModel> allPieces, bool allowSelfCheck)
        {
            IEnumerable<(int, int)> moves;
            if (model is null)
                return new List<(int x, int y)>();

            switch (model.PieceClass)
            {
                case PieceClass.King:
                    moves = GetKingMoves(model, allPieces);
                    break;
                case PieceClass.Queen:
                    moves = GetQueenMoves(model, allPieces);
                    break;
                case PieceClass.Pawn:
                    moves = GetPawnMoves(model, allPieces);
                    break;
                case PieceClass.Rook:
                    moves = GetRookMoves(model, allPieces);
                    break;
                case PieceClass.Bishop:
                    moves = GetBishopMoves(model, allPieces);
                    break;
                case PieceClass.Knight:
                    moves = GetHorseyMoves(model, allPieces);
                    break;
                default:
                    return new List<(int x, int y)>();
            }
            var legalMoves = allowSelfCheck
                ? moves.ToList()
                : moves.Where(pos => !MoveIsCheck(pos, model, allPieces)).ToList();

            return legalMoves;

        }
        private List<(int, int)> GetHorseyMoves(PieceModel model, List<PieceModel> allPieces)
        {
            var moves = new List<(int, int)>();
            var p = model.Position;

            var positions = new[] {
                (p.x + 1, p.y - 2),
                (p.x + 1, p.y + 2 ),
                (p.x + 2, p.y + 1),
                (p.x + 2, p.y - 1),

                (p.x - 1, p.y - 2),
                (p.x - 1, p.y + 2 ),
                (p.x - 2, p.y + 1),
                (p.x - 2, p.y - 1),
            };

            moves.AddRange(positions.Where(p => !SquareIsIllegal(model, allPieces, p)));
            return moves;
        }

        private List<(int, int)> GetQueenMoves(PieceModel model, List<PieceModel> allPieces)
        {
            var moves = GetBishopMoves(model, allPieces);
            moves.AddRange(GetRookMoves(model, allPieces));
            return moves;
        }

        private IEnumerable<(int, int)> GetRookMoves(PieceModel model, List<PieceModel> allPieces)
        {
            //x-right
            var rookXright = model.Position.x;
            while (rookXright < 7)
            {

                if (allPieces.FirstOrDefault(p => p.Position == (rookXright + 1, model.Position.y)) is PieceModel xRightModel)
                {
                    //same colour
                    if (xRightModel.Colour == model.Colour)
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

            var rangeX = Enumerable.Range(rookXLeft, rookXright - rookXLeft + 1).Where(r => r != model.Position.x);

            //y-up
            var rookYup = model.Position.y;
            while (rookYup < 7)
            {

                if (allPieces.FirstOrDefault(p => p.Position == (model.Position.x, rookYup + 1)) is PieceModel rookYupModel)
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

            var rangeY = Enumerable.Range(rookYdown, rookYup - rookYdown + 1).Where(r => r != model.Position.y);
            var legal = new List<(int x, int y)>();
            legal.AddRange(rangeX.Select(x => (x, model.Position.y)));
            legal.AddRange(rangeY.Select(y => (model.Position.x, y)));

            return legal;
        }

        private List<(int, int)> GetPawnMoves(PieceModel payload, List<PieceModel> allPieces)
        {
            var moveDirection = payload.Colour == Colour.White
                ? 1
                : -1;
            var moves = new List<(int, int)>();
            if (payload.Position.y < 7 && payload.Position.y > 0)
            {
                var newPos1 = (payload.Position.x, payload.Position.y + moveDirection);
                var newPos2 = (payload.Position.x, payload.Position.y + 2 * moveDirection);
                if (SquareIsFree(allPieces, newPos1))
                {
                    moves.Add(newPos1);

                    //first move
                    if (SquareIsFree(allPieces, newPos2))
                    {
                        if (payload.Colour == Colour.White && payload.Position.y == 1 ||
                            payload.Colour == Colour.Black && payload.Position.y == 6)
                        {
                            moves.Add(newPos2);
                        }
                    }
                }

                //attack moves:
                var attackPos1 = (payload.Position.x + 1, payload.Position.y + moveDirection);
                var attackPos2 = (payload.Position.x - 1, payload.Position.y + moveDirection);
                if (SquareIsCapture(payload, allPieces, attackPos1))
                {
                    moves.Add(attackPos1);
                }

                if (SquareIsCapture(payload, allPieces, attackPos2))
                {
                    moves.Add(attackPos2);
                }
            }
            return moves;
        }

        private List<(int, int)> GetBishopMoves(PieceModel payload, List<PieceModel> allPieces)
        {
            var moves = new List<(int, int)>();

            var upleftX = -1;
            var upleftY = -1;
            var allowUpLeft = true;

            var downleftX = -1;
            var downleftY = 1;
            var allowDownLeft = true;

            var downrightX = 1;
            var downrightY = 1;
            var allowDownRight = true;

            var uprightX = 1;
            var uprightY = -1;
            var allowUpRight = true;

            for (int i = 1; i < 7; i++)
            {
                allowUpLeft = AddBishopMoveIfLegal(i, upleftX, upleftY, allowUpLeft, moves, payload, allPieces);
                allowDownLeft = AddBishopMoveIfLegal(i, downleftX, downleftY, allowDownLeft, moves, payload, allPieces);
                allowDownRight = AddBishopMoveIfLegal(i, downrightX, downrightY, allowDownRight, moves, payload, allPieces);
                allowUpRight = AddBishopMoveIfLegal(i, uprightX, uprightY, allowUpRight, moves, payload, allPieces);
            }
            return moves;
            bool AddBishopMoveIfLegal(int moveLength, int directionX, int directionY, bool allowDirection, List<(int, int)> moves, PieceModel payload, List<PieceModel> allPieces)
            {
                var pos = (x: payload.Position.x + moveLength * directionX, y: payload.Position.y + moveLength * directionY);
                if (allowDirection)
                {
                    if (moveLength == 1)
                    {
                        // allowDirection = MoveIsCheck(pos, payload, allPieces);
                    }
                    if (SquareIsIllegal(payload, allPieces, pos))
                    {
                        allowDirection = false;
                    }
                    else
                    {
                        moves.Add(pos);
                        allowDirection = !SquareIsCapture(payload, allPieces, pos);
                    }
                }
                return allowDirection;
            }
        }

        private bool MoveIsCheck((int, int) pos, PieceModel payload, List<PieceModel> allPieces)
        {

            //todo: verify... maybe buggy. Checking all pieces not nescessary
            var kingPos = payload.PieceClass == PieceClass.King
                ? pos
                : allPieces.First(p => p.Colour == payload.Colour && p.PieceClass == PieceClass.King).Position;

            var newBoardPositions = allPieces.Where(p => p.Position != payload.Position).ToList();
            newBoardPositions.Add(payload.Clone(pos));
            var opponentPices = allPieces.Where(p => p.Colour != payload.Colour && p.PieceClass != PieceClass.King);
            var isCheck = opponentPices.Any(piece => GetLegalMoves(piece, newBoardPositions, true).Any(p => p == kingPos));
            // var causingCheck = opponentPices.Select(piece => (piece, GetLegalMoves(piece, newBoardPositions, true).Any(p => p == kingPos))).Where(i => i.Item2);

            return isCheck;
        }

        private List<(int, int)> GetKingMoves(PieceModel payload, List<PieceModel> allPieces)
        {
            var moves = new List<(int, int)>();
            var p = payload.Position;

            var positions = new[] {
                (p.x + 1, p.y - 1),
                (p.x + 1, p.y ),
                (p.x + 1, p.y + 1),

                (p.x, p.y + 1),
                (p.x, p.y - 1),

                (p.x - 1, p.y - 1),
                (p.x - 1, p.y),
                (p.x - 1, p.y + 1)
            };

            moves.AddRange(positions.Where(p => !SquareIsIllegal(payload, allPieces, p)));
            return moves;
        }

        private bool SquareIsFree(List<PieceModel> allPieces, (int x, int y) p)
        {
            //check if piece of same colour is located at square p 
            return !allPieces.Any(a => a.Position == p);
        }

        private bool SquareIsIllegal(PieceModel piece, List<PieceModel> allPieces, (int x, int y) p)
        {
            //check if piece of same colour is located at square p 
            if (p.x < 0 || p.x > 7 || p.y < 0 || p.y > 7) return false;
            return allPieces.Any(a => a.Colour == piece.Colour && a.Position == p);
        }
        private bool SquareIsCapture(PieceModel piece, List<PieceModel> allPieces, (int x, int y) p)
        {
            //check if piece of same colour is located at square p 
            return allPieces.Any(a => a.Colour != piece.Colour && a.Position == p);
        }
    }
}
