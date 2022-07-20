using Raylib_cs;
using static Raylib_cs.Raylib;

public class King : Piece
{
    private int[] boundsWest = { 0, 8, 16, 24, 32, 40, 48, 56 };
    private int[] boundsEast = { 7, 15, 23, 31, 39, 47, 55, 63 };
    public King(bool white) : base(white)
    {
        if (white)
        {
            ImageCrop(ref this.image, new Rectangle(0, 0, 96, 96));
        }
        else
        {
            ImageCrop(ref this.image, new Rectangle(0, 96, 96, 192));
        }
        this.texture = LoadTextureFromImage(this.image);
        UnloadImage(this.image);
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        if (!Board.mate)
        {

        }
        List<Tile> legalMoves = new();
        Tile? north = startTile.GetNorth(ScreenManager.board);
        if (CheckTile(north, true, ownPawns))
        {
            legalMoves.Add(north);
        }

        Tile? northEast = startTile.GetNorthEast(ScreenManager.board);
        if (CheckTile(northEast, true, ownPawns))
        {
            legalMoves.Add(northEast);
        }

        Tile? east = startTile.GetEast(ScreenManager.board);
        if (CheckTile(east, true, ownPawns))
        {
            legalMoves.Add(east);
        }

        Tile? southEast = startTile.GetSouthEast(ScreenManager.board);
        if (CheckTile(southEast, true, ownPawns))
        {
            legalMoves.Add(southEast);
        }

        Tile? south = startTile.GetSouth(ScreenManager.board);
        if (CheckTile(south, false, ownPawns))
        {
            legalMoves.Add(south);
        }

        Tile? southWest = startTile.GetSouthWest(ScreenManager.board);
        if (CheckTile(southWest, false, ownPawns))
        {
            legalMoves.Add(southWest);
        }

        Tile? west = startTile.GetWest(ScreenManager.board);
        if (CheckTile(west, false, ownPawns))
        {
            legalMoves.Add(west);
        }

        Tile? northWest = startTile.GetNorthWest(ScreenManager.board);
        if (CheckTile(northWest, false, ownPawns))
        {
            legalMoves.Add(northWest);
        }

        // Check if casteling on kingSide is possible
        if (!east.ContainsPiece())
        {
            Tile? possiblePosition = east.GetEast(ScreenManager.board);
            while (east != null && possiblePosition != null && !east.ContainsPiece())
            {
                east = east.GetEast(ScreenManager.board);
            }
            if (east != null && possiblePosition != null && east.ContainsPiece() && east.GetPieceOnTile().GetColor().Equals(this.GetColor())
                && typeof(Rook).Equals(east.GetPieceOnTile().GetType()))
            {
                legalMoves.Add(possiblePosition);
            }
        }

        // Check if casteling on queenSide is possible
        if (!west.ContainsPiece())
        {
            Tile? possiblePosition = west.GetWest(ScreenManager.board);
            while (west != null && possiblePosition != null && !west.ContainsPiece())
            {
                west = west.GetWest(ScreenManager.board);
            }
            if (west != null && possiblePosition != null && west.ContainsPiece() && west.GetPieceOnTile().GetColor().Equals(this.GetColor())
                && typeof(Rook).Equals(west.GetPieceOnTile().GetType()))
            {
                legalMoves.Add(possiblePosition);
            }
        }

        return legalMoves;
    }

    private bool CheckTile(Tile tile, bool checkWestBounds, bool ownPawns)
    {
        if (checkWestBounds)
        {
            if (tile != null && !boundsWest.Contains(tile.GetPositionOnBoard()))
            {
                if (!tile.ContainsPiece() || ownPawns && tile.ContainsPiece() && tile.GetPieceOnTile().GetColor().Equals(this.GetColor())
                    || tile.ContainsPiece() && !tile.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
            }
        }
        else
        {
            if (tile != null && !boundsEast.Contains(tile.GetPositionOnBoard()))
            {
                if (!tile.ContainsPiece() || ownPawns && tile.ContainsPiece() && tile.GetPieceOnTile().GetColor().Equals(this.GetColor())
                    || tile.ContainsPiece() && !tile.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    return true;
                }
            }
        }

        return false;
    }
}