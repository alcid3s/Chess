using Raylib_cs;
using static Raylib_cs.Raylib;

public class Pawn : Piece
{
    public Pawn(bool white) : base(white)
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
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMoves = new();
        if (this.white)
        {
            if ((startTile.GetPositionOnBoard() + 1) % 8 != 0)
            {
                Tile northEast = startTile.GetNorthEast(ScreenManager.board);
                if (northEast != null && northEast.ContainsPiece())
                {
                    if (ownPawns && northEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(northEast);
                    }
                    else if (!northEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(northEast);
                    }

                }
            }

            if (startTile.GetPositionOnBoard() % 8 != 0)
            {
                Tile northWest = startTile.GetNorthWest(ScreenManager.board);
                if (northWest != null && northWest.ContainsPiece())
                {
                    if(ownPawns && northWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(northWest);
                    }else if (!northWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(northWest);
                    }
                    
                }
            }

            Tile north = startTile.GetNorth(ScreenManager.board);
            if (north == null || north.ContainsPiece())
            {
                return legalMoves;
            }
            else
            {
                legalMoves.Add(north);
                if (startTile.GetPositionOnBoard() >= 48 && startTile.GetPositionOnBoard() <= 55)
                {
                    north = north.GetNorth(ScreenManager.board);
                    if (north != null && !north.ContainsPiece())
                    {
                        legalMoves.Add(north);
                    }
                }
            }
        }
        else
        {
            if ((startTile.GetPositionOnBoard() + 1) % 8 != 0)
            {
                Tile southEast = startTile.GetSouthEast(ScreenManager.board);
                if (southEast != null && southEast.ContainsPiece() && !southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                {
                    if(ownPawns && southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(southEast);
                    }
                    else if (!southEast.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(southEast);
                    }
                }
            }

            if (startTile.GetPositionOnBoard() % 8 != 0)
            {
                Tile southWest = startTile.GetSouthWest(ScreenManager.board);
                if (southWest != null && southWest.ContainsPiece())
                {
                    if(ownPawns && southWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(southWest);
                    }
                    else if (!southWest.GetPieceOnTile().GetColor().Equals(this.GetColor()))
                    {
                        legalMoves.Add(southWest);
                    }
                    
                }
            }

            Tile south = startTile.GetSouth(ScreenManager.board);
            if (south == null || south.ContainsPiece())
            {
                return legalMoves;
            }
            else
            {
                legalMoves.Add(south);
                if (startTile.GetPositionOnBoard() >= 8 && startTile.GetPositionOnBoard() <= 15)
                {
                    south = south.GetSouth(ScreenManager.board);
                    if (south != null && !south.ContainsPiece())
                    {
                        legalMoves.Add(south);
                    }
                }
            }
        }
        return legalMoves;
    }
}
