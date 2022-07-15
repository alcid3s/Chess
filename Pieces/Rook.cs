using Raylib_cs;
using static Raylib_cs.Raylib;


public class Rook : Piece
{
    public Rook(bool white) : base(white)
    {
        if (white)
        {
            ImageCrop(ref this.image, new Rectangle(384, 0, 480, 96));
        }
        else
        {
            ImageCrop(ref this.image, new Rectangle(384, 96, 480, 192));
        }
        this.texture = LoadTextureFromImage(this.image);
        UnloadImage(this.image);
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMoves = new();
        Tile north = startTile.GetNorth(ScreenManager.board);
        while (north != null && !north.ContainsPiece())
        {
            legalMoves.Add(north);
            north = north.GetNorth(ScreenManager.board);
        }
        if (north != null && north.ContainsPiece())
        {
            if(ownPawns && north.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(north);
            }
            else if (!north.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(north);
            }
            
        }

        Tile east = startTile.GetEast(ScreenManager.board);
        while(east != null && !east.ContainsPiece() && (east.GetPositionOnBoard()) % 8 != 0 )
        {
            legalMoves.Add(east);
            east = east.GetEast(ScreenManager.board);
        }
        if(east != null && east.ContainsPiece() && (east.GetPositionOnBoard()) % 8 != 0)
        {
            if(ownPawns && east.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(east);
            }
            else if (!east.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(east);
            }
            
        }

        Tile south = startTile.GetSouth(ScreenManager.board);
        while(south != null && !south.ContainsPiece())
        {
            legalMoves.Add(south);
            south = south.GetSouth(ScreenManager.board);
        }
        if(south != null && south.ContainsPiece())
        {
            if(ownPawns && south.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(south);
            }
            else if (!south.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(south);
            }
            
        }

        Tile west = startTile.GetWest(ScreenManager.board);
        while(west != null && !west.ContainsPiece() && (west.GetPositionOnBoard() + 1) % 8 != 0)
        {
            legalMoves.Add(west);
            west = west.GetWest(ScreenManager.board);
        }
        if(west != null && west.ContainsPiece() && (west.GetPositionOnBoard() + 1) % 8 != 0)
        {
            if(ownPawns & west.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(west);
            }
            else if (!west.GetPieceOnTile().GetColor().Equals(this.GetColor()))
            {
                legalMoves.Add(west);
            }
        }
        return legalMoves;
    }
}