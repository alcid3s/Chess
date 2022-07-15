using Raylib_cs;
using static Raylib_cs.Raylib;

public class Queen : Piece
{
    private Rook rook;
    private Bishop bishop;
    public Queen(bool white) : base(white)
    {
        if (white)
        {
            ImageCrop(ref this.image, new Rectangle(96, 0, 192, 96));
        }
        else
        {
            ImageCrop(ref this.image, new Rectangle(96, 96, 192, 192));
        }
        this.texture = LoadTextureFromImage(this.image);
        UnloadImage(this.image);

        this.rook = new Rook(white);
        this.bishop = new Bishop(white);
    }

    public override List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns)
    {
        List<Tile> legalMoves = new();
        legalMoves.AddRange(this.rook.CalculateLegalMoves(startTile, ownPawns));
        legalMoves.AddRange(this.bishop.CalculateLegalMoves(startTile, ownPawns));

        return legalMoves;
    }
}
