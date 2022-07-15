using Raylib_cs;
using static Raylib_cs.Raylib;

public class King : Piece
{
    // still shit, but it works fine :D
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
        List<Tile> legalMoves = new();
        Tile north = startTile.GetNorth(ScreenManager.board);
        if (checkTile(north, true))
        {
            legalMoves.Add(north);
        }

        Tile northEast = startTile.GetNorthEast(ScreenManager.board);
        if (checkTile(northEast, true))
        {
            legalMoves.Add(northEast);
        }

        Tile east = startTile.GetEast(ScreenManager.board);
        if (checkTile(east, true))
        {
            legalMoves.Add(east);
        }

        Tile southEast = startTile.GetSouthEast(ScreenManager.board);
        if (checkTile(southEast, true))
        {
            legalMoves.Add(southEast);
        }

        Tile south = startTile.GetSouth(ScreenManager.board);
        if (checkTile(south, false))
        {
            legalMoves.Add(south);
        }

        Tile southWest = startTile.GetSouthWest(ScreenManager.board);
        if (checkTile(southWest, false))
        {
            legalMoves.Add(southWest);
        }

        Tile west = startTile.GetWest(ScreenManager.board);
        if (checkTile(west, false))
        {
            legalMoves.Add(west);
        }

        Tile northWest = startTile.GetNorthWest(ScreenManager.board);
        if (checkTile(northWest, false))
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

    private bool checkTile(Tile tile, bool checkWestBounds)
    {
        if (checkWestBounds)
        {
            if (tile != null && !tile.ContainsPiece() && !boundsWest.Contains(tile.GetPositionOnBoard())
                || tile != null && tile.ContainsPiece() && !tile.GetPieceOnTile().GetColor().Equals(this.GetColor())
                && !boundsWest.Contains(tile.GetPositionOnBoard()))
            {
                return true;
            }
        }
        else
        {
            if (tile != null && !tile.ContainsPiece() && !boundsEast.Contains(tile.GetPositionOnBoard())
                || tile != null && tile.ContainsPiece() && !tile.GetPieceOnTile().GetColor().Equals(this.GetColor())
                && !boundsEast.Contains(tile.GetPositionOnBoard()))
            {
                return true;
            }
        }

        return false;
    }
}