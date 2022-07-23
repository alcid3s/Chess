using System.Numerics;

public class Player
{
    private static Random random = new Random();
    public bool White { get; }
    public Player(bool white)
    {
        Console.WriteLine("Enemy has color: " + (white ? "White" : "Black"));
        this.White = white;
    }

    public void GenerateMove(Tile[] tiles)
    {
        List<Piece> pieces = GetPieces(tiles);

        int selectPiece = random.Next(0, pieces.Count);
        List<Tile> legalMoves = pieces.ElementAt(selectPiece).CalculateLegalMoves(pieces.ElementAt(selectPiece).GetAssignedTile(), false);

        while (!legalMoves.Any() && pieces.ElementAt(selectPiece).PieceIsAlive() || legalMoves.Count == 0 && pieces.ElementAt(selectPiece).PieceIsAlive())
        { 
            selectPiece = random.Next(0, pieces.Count);
            legalMoves = pieces.ElementAt(selectPiece).CalculateLegalMoves(pieces.ElementAt(selectPiece).GetAssignedTile(), false);
        }

        ScreenManager.board.OnClick(new Vector2(pieces.ElementAt(selectPiece).GetAssignedTile().x + 10, pieces.ElementAt(selectPiece).GetAssignedTile().y + 10));

        int randomMove = random.Next(0, legalMoves.Count);

        Console.WriteLine("Player is moving " + pieces.ElementAt(selectPiece).GetType() + " to " + legalMoves.ElementAt(randomMove).GetPositionOnBoard());
        ScreenManager.board.OnClick(new Vector2(legalMoves.ElementAt(randomMove).x + 10, legalMoves.ElementAt(randomMove).y + 10));
    }

    private List<Piece> GetPieces(Tile[] tiles)
    {
        List<Piece> pieces = new();
        foreach (Tile tile in tiles)
        {
            if (tile.ContainsPiece() && tile.GetPieceOnTile().GetColor().Equals(this.White) && tile.GetPieceOnTile().PieceIsAlive())
            {
                pieces.Add(tile.GetPieceOnTile()); ;
            }
        }
        return pieces;
    }
}