using Raylib_cs;
using static Raylib_cs.Raylib;

public class Bishop : Piece
{
    // I'm not proud of this bounds construction. But it's late, I want to sleep.
    private int[] boundsWest = { 0, 8, 16, 24, 32, 40, 48, 56 };
    private int[] boundsEast = { 7, 15, 23, 31, 39, 47, 55, 63 };

    public Bishop(bool white) : base(white)
    {
        if (white)
        {
            ImageCrop(ref this.image, new Rectangle(192, 0, 288, 96));
        }
        else
        {
            ImageCrop(ref this.image, new Rectangle(192, 96, 288, 192));
        }
        this.texture = LoadTextureFromImage(this.image);
        UnloadImage(this.image);
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMoves = new();
        Tile northEast = startTile.GetNorthEast(ScreenManager.board);
        while (northEast != null && !northEast.ContainsPiece() && !boundsWest.Contains(northEast.GetPositionOnBoard()))
        {
            legalMoves.Add(northEast);
            northEast = northEast.GetNorthEast(ScreenManager.board);
        }

        if (northEast != null && northEast.ContainsPiece() && !boundsWest.Contains(northEast.GetPositionOnBoard()))
        {
            // if own pieces need to be in legalMoves too
            if (ownPawns && northEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(northEast);
            }

            // if own pieces dont need to be in legalMoves.
            else if (!northEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(northEast);
            }

        }

        Tile southEast = startTile.GetSouthEast(ScreenManager.board);
        while (southEast != null && !southEast.ContainsPiece() && !boundsWest.Contains(southEast.GetPositionOnBoard()))
        {
            legalMoves.Add(southEast);
            southEast = southEast.GetSouthEast(ScreenManager.board);
        }
        if (southEast != null && southEast.ContainsPiece() && !boundsWest.Contains(southEast.GetPositionOnBoard()))
        {
            if (ownPawns && southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(southEast);
            }
            else if (!southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(southEast);
            }

        }

        Tile southWest = startTile.GetSouthWest(ScreenManager.board);
        while (southWest != null && !southWest.ContainsPiece() && !boundsEast.Contains(southWest.GetPositionOnBoard()))
        {
            legalMoves.Add(southWest);
            southWest = southWest.GetSouthWest(ScreenManager.board);
        }
        if (southWest != null && southWest.ContainsPiece() && !boundsEast.Contains(southWest.GetPositionOnBoard()))
        {
            if (ownPawns && southWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(southWest);
            }
            else if (!southWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(southWest);
            }

        }

        Tile northWest = startTile.GetNorthWest(ScreenManager.board);
        while (northWest != null && !northWest.ContainsPiece() && !boundsEast.Contains(northWest.GetPositionOnBoard()))
        {
            legalMoves.Add(northWest);
            northWest = northWest.GetNorthWest(ScreenManager.board);
        }
        if (northWest != null && northWest.ContainsPiece() && !boundsEast.Contains(northWest.GetPositionOnBoard()))
        {
            if (ownPawns && northWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(northWest);
            }
            else if (!northWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(northWest);
            }
        }

        return legalMoves;
    }
}
