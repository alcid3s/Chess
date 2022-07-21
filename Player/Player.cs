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

    public void GenerateMove()
    {
        List<Piece> pieces = GetPieces();

        int selectPiece = random.Next(0, pieces.Count);
        List<Tile> legalMoves = pieces.ElementAt(selectPiece).CalculateLegalMoves(pieces.ElementAt(selectPiece).GetAssignedTile(), false);

        while (!legalMoves.Any() || legalMoves.Count == 0)
        { 
            selectPiece = random.Next(0, pieces.Count);
            legalMoves = pieces.ElementAt(selectPiece).CalculateLegalMoves(pieces.ElementAt(selectPiece).GetAssignedTile(), false);
        }

        ScreenManager.board.OnClick(new Vector2(pieces.ElementAt(selectPiece).GetAssignedTile().x + 10, pieces.ElementAt(selectPiece).GetAssignedTile().y + 10));
        Console.WriteLine("Selected " + pieces.ElementAt(selectPiece).GetType() + " at position " + pieces.ElementAt(selectPiece).GetAssignedTile().GetPositionOnBoard());

        

        int randomMove = random.Next(0, legalMoves.Count);
        ScreenManager.board.OnClick(new Vector2(legalMoves.ElementAt(randomMove).x + 10, legalMoves.ElementAt(randomMove).y + 10));
        Console.WriteLine("randomMove: " + randomMove + " selected tile: " + legalMoves.ElementAt(randomMove).GetPositionOnBoard());
    }

    private List<Piece> GetPieces()
    {
        List<Piece> pieces = new();
        foreach (Tile tile in ScreenManager.board.GetTiles())
        {
            if (tile.ContainsPiece() && tile.GetPieceOnTile().GetColor().Equals(this.White))
            {
                pieces.Add(tile.GetPieceOnTile()); ;
            }
        }
        return pieces;
    }
}