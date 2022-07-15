using Raylib_cs;
using static Raylib_cs.Raylib;

public class Knight : Piece
{
    // It's shit but it works :)
    private int[] boundsWest = { 0, 8, 16, 24, 32, 40, 48, 56 };
    private int[] boundsEast = { 7, 15, 23, 31, 39, 47, 55, 63 };

    public Knight(bool white) : base(white)
    {
        if (white)
        {
            ImageCrop(ref this.image, new Rectangle(288, 0, 384, 96));
        }
        else
        {
            ImageCrop(ref this.image, new Rectangle(288, 96, 384, 192));
        }
        this.texture = LoadTextureFromImage(this.image);
        UnloadImage(this.image);
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMoves = new();
        Tile northNorthEast = startTile.GetNorth(ScreenManager.board);
        if (northNorthEast != null)
        {
            northNorthEast = northNorthEast.GetNorthEast(ScreenManager.board);
            if (CheckTile(northNorthEast, true, ownPawns))
            {
                legalMoves.Add(northNorthEast);
            }
        }

        Tile northEastEast = startTile.GetNorthEast(ScreenManager.board);
        if (northEastEast != null && !boundsWest.Contains(northEastEast.GetPositionOnBoard()))
        {
            northEastEast = northEastEast.GetEast(ScreenManager.board);
            if (CheckTile(northEastEast, true, ownPawns))
            {
                legalMoves.Add(northEastEast);
            }
        }

        Tile southEastEast = startTile.GetSouthEast(ScreenManager.board);
        if (southEastEast != null && !boundsWest.Contains(southEastEast.GetPositionOnBoard()))
        {
            southEastEast = southEastEast.GetEast(ScreenManager.board);
            if (CheckTile(southEastEast, true, ownPawns))
            {
                legalMoves.Add(southEastEast);
            }
        }

        Tile southSouthEast = startTile.GetSouth(ScreenManager.board);
        if (southSouthEast != null)
        {
            southSouthEast = southSouthEast.GetSouthEast(ScreenManager.board);
            if (CheckTile(southSouthEast, true, ownPawns))
            {
                legalMoves.Add(southSouthEast);
            }
        }

        Tile southSouthWest = startTile.GetSouth(ScreenManager.board);
        if (southSouthWest != null)
        {
            southSouthWest = southSouthWest.GetSouthWest(ScreenManager.board);
            if (CheckTile(southSouthWest, false, ownPawns))
            {
                legalMoves.Add(southSouthWest);
            }
        }

        Tile southWestWest = startTile.GetSouthWest(ScreenManager.board);
        if (southWestWest != null && !boundsEast.Contains(southWestWest.GetPositionOnBoard()))
        {
            southWestWest = southWestWest.GetWest(ScreenManager.board);
            if (CheckTile(southWestWest, false, ownPawns))
            {
                legalMoves.Add(southWestWest);
            }
        }

        Tile northWestWest = startTile.GetNorthWest(ScreenManager.board);
        if (northWestWest != null && !boundsEast.Contains(northWestWest.GetPositionOnBoard()))
        {
            northWestWest = northWestWest.GetWest(ScreenManager.board);
            if (CheckTile(northWestWest, false, ownPawns))
            {
                legalMoves.Add(northWestWest);
            }
        }

        Tile northNorthWest = startTile.GetNorth(ScreenManager.board);
        if (northNorthWest != null)
        {
            northNorthWest = northNorthWest.GetNorthWest(ScreenManager.board);
            if (CheckTile(northNorthWest, false, ownPawns))
            {
                legalMoves.Add(northNorthWest);
            }
        }
        return legalMoves;
    }

    private bool CheckTile(Tile tile, bool checkWestBound, bool ownPawns)
    {
        if (checkWestBound)
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
