using Raylib_cs;
using static Raylib_cs.Raylib;
public class Pawn : Piece
{
    private bool boardReversed;
    private int[] startPositionNorth = { 8, 9, 10, 11, 12, 13, 14, 15 };
    private int[] startPositionSouth = { 48, 49, 50, 51, 52, 53, 54, 55 };
    public Pawn(bool white, bool boardReversed) : base(white)
    {
        if (white)
        {
            ImageCrop(ref this.image, new Rectangle(480, 0, 572, 96));
        }
        else
        {
            ImageCrop(ref this.image, new Rectangle(480, 96, 572, 192));
        }
        this.texture = LoadTextureFromImage(this.image);
        UnloadImage(this.image);

        this.boardReversed = boardReversed;
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMoves = new();
        if (this.white && !this.boardReversed)
        {
            legalMoves.AddRange(LegalMovesNorth(startTile, ownPawns));
        }
        else if(!this.white && !this.boardReversed)
        {
            legalMoves.AddRange(LegalMovesSouth(startTile, ownPawns));
        }
        else if(this.white && this.boardReversed)
        {
            legalMoves.AddRange(LegalMovesSouth(startTile, ownPawns));
        }
        else if(!this.white && this.boardReversed)
        {
            legalMoves.AddRange(LegalMovesNorth(startTile, ownPawns));
        }
        return legalMoves;
    }

    private List<Tile> LegalMovesNorth(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMovesNorth = new();
        // Check if pawn can take enemy piece on his east
        if (CheckNorthEast(startTile, ownPawns))
        {
            legalMovesNorth.Add(startTile.GetNorthEast(ScreenManager.board));
        }

        // check if pawn can take enemy piece on his west
        if (CheckNorthWest(startTile, ownPawns))
        {
            legalMovesNorth.Add(startTile.GetNorthWest(ScreenManager.board));
        }

        // Check north first move
        if (CheckNorthMovements(startTile))
        {
            Tile north = startTile.GetNorth(ScreenManager.board);
            legalMovesNorth.Add(north);

            // Check if the pawn is on the startPosition, if so it can move one more space north
            if (startPositionSouth.Contains(this.assignedTile.GetPositionOnBoard()) && CheckNorthMovements(north))
            {
                legalMovesNorth.Add(north.GetNorth(ScreenManager.board));
            }
        }
        return legalMovesNorth;
    }
    private List<Tile> LegalMovesSouth(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMovesSouth = new();
        // Check for south East
        if (CheckSouthEast(startTile, ownPawns))
        {
            legalMovesSouth.Add(startTile.GetSouthEast(ScreenManager.board));
        }


        // Check for southWest
        if (CheckSouthWest(startTile, ownPawns))
        {
            legalMovesSouth.Add(startTile.GetSouthWest(ScreenManager.board));
        }

        // Check if pawn can move south
        if (CheckSouthMovements(startTile))
        {
            Tile south = startTile.GetSouth(ScreenManager.board);
            legalMovesSouth.Add(south);

            // Check if the pawn is on the startPosition, if so it can move one more space south
            if (startPositionNorth.Contains(this.assignedTile.GetPositionOnBoard()) && CheckSouthMovements(south))
            {
                legalMovesSouth.Add(south.GetSouth(ScreenManager.board));
            }
        }
        return legalMovesSouth;
    }
    private bool CheckNorthMovements(Tile startTile)
    {
        Tile north = startTile.GetNorth(ScreenManager.board);
        if (north == null || north.ContainsPiece())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool CheckSouthMovements(Tile startTile)
    {
        Tile south = startTile.GetSouth(ScreenManager.board);
        if (south == null || south.ContainsPiece())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool CheckNorthEast(Tile startTile, bool ownPawns)
    {
        if ((startTile.GetPositionOnBoard() + 1) % 8 != 0)
        {
            Tile northEast = startTile.GetNorthEast(ScreenManager.board);
            if (northEast != null && northEast.ContainsPiece())
            {
                if (ownPawns && northEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
                else if (!northEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckNorthWest(Tile startTile, bool ownPawns)
    {
        if (startTile.GetPositionOnBoard() % 8 != 0)
        {
            Tile northWest = startTile.GetNorthWest(ScreenManager.board);
            if (northWest != null && northWest.ContainsPiece())
            {
                if (ownPawns && northWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
                else if (!northWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckSouthEast(Tile startTile, bool ownPawns)
    {
        if ((startTile.GetPositionOnBoard() + 1) % 8 != 0)
        {
            Tile southEast = startTile.GetSouthEast(ScreenManager.board);
            if (southEast != null && southEast.ContainsPiece() && !southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                if (ownPawns && southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
                else if (!southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CheckSouthWest(Tile startTile, bool ownPawns)
    {
        if (startTile.GetPositionOnBoard() % 8 != 0)
        {
            Tile southWest = startTile.GetSouthWest(ScreenManager.board);
            if (southWest != null && southWest.ContainsPiece())
            {
                if (ownPawns && southWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
                else if (!southWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }

            }
        }
        return false;
    }
}
