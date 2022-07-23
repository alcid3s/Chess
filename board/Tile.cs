using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;

public class Tile
{
    private Texture2D texture;
    private Vector2 positionOnBoard;
    public int x, y;

    private Piece? pieceOnTile = null;

    public bool Even;
    public Tile(Texture2D texture, int x, int y, bool even)
    {
        this.texture = texture;
        this.positionOnBoard = new(x / texture.width, y / texture.height);

        this.Even = even;
        this.x = x;
        this.y = y;
    }

    public void Draw()
    {
        DrawTexture(texture, x, y, WHITE);
        if (this.pieceOnTile != null)
        {
            DrawTexture(this.pieceOnTile.GetTexture(), x, y, WHITE);
        }
    }
    public void SetTexture(Texture2D texture)
    {
        this.texture = texture;
    }

    public void Assign(Piece piece)
    {
        this.pieceOnTile = piece;
    }
    public void Detach()
    {
        this.pieceOnTile = null;
    }
    public bool ContainsPiece()
    {
        return this.pieceOnTile != null;
    }
    public Piece? GetPieceOnTile()
    {
        return this.pieceOnTile;
    }

    public int GetPositionOnBoard()
    {
        return (int) (positionOnBoard.X + (positionOnBoard.Y * 8));
    }

    public bool OnClick(Vector2 mousePosition)
    {
        if (y < mousePosition.Y && mousePosition.Y < y + texture.height && x < mousePosition.X && mousePosition.X < x + texture.width)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Tile? GetNorth(Board board)
    {
        int bounds = GetPositionOnBoard() - 8;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetNorthEast(Board board)
    {
        int bounds = GetPositionOnBoard() - 7;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetEast(Board board)
    {
        int bounds = GetPositionOnBoard() + 1;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetSouthEast(Board board)
    {
        int bounds = GetPositionOnBoard() + 9;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetSouth(Board board)
    {
        int bounds = GetPositionOnBoard() + 8;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetSouthWest(Board board)
    {
        int bounds = GetPositionOnBoard() + 7;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetWest(Board board)
    {
        int bounds = GetPositionOnBoard() - 1;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
    public Tile? GetNorthWest(Board board)
    {
        int bounds = GetPositionOnBoard() - 9;
        return bounds >= 0 && bounds <= 63 ? board.GetTiles()[bounds] : null;
    }
}