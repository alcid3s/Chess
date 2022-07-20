using System.Numerics;

public class Player
{
    private static Random random = new Random();

    private List<Piece> pieces = new();
    public bool White { get; }
    public Player(bool white)
    {
        Console.WriteLine("Enemy has color: " + (white ? "White" : "Black"));
        this.White = white;
        foreach (Tile tile in ScreenManager.board.GetTiles())
        {
            if (tile.ContainsPiece() && tile.GetPieceOnTile().GetColor().Equals(white))
            {
                pieces.Add(tile.GetPieceOnTile()); ;
            }
        }
    }

    public void GenerateMove()
    {
        int piece = random.Next(pieces.Count);
        List<Tile> legalMoves = pieces.ElementAt(piece).CalculateLegalMoves(pieces.ElementAt(piece).GetAssignedTile(), false);

        while(legalMoves.Count == 0)
        {
            piece = random.Next(pieces.Count);
            legalMoves = pieces.ElementAt(piece).CalculateLegalMoves(pieces.ElementAt(piece).GetAssignedTile(), false);
        }

        Console.WriteLine(piece + " so " + pieces.ElementAt(piece).GetType() + " is selected and pieceCount is " + pieces.Count);

        int x = pieces.ElementAt(piece).GetAssignedTile().x;
        int y = pieces.ElementAt(piece).GetAssignedTile().y;
        ScreenManager.board.OnClick(new Vector2(x, y));

        pieces.ElementAt(piece).GetAssignedTile().SetTexture(Board.tile6);

        int move = random.Next(legalMoves.Count);

        Console.WriteLine("Move is " + move + " while legalMoveCount is " + legalMoves.Count);
        while(move == 0)
        {
            move = random.Next(legalMoves.Count);
            if (legalMoves.Count == 1)
            {
                move = 1;
            }
        }

        Console.WriteLine(move + " while legalMovesCount: " + legalMoves.Count);
        x = legalMoves.ElementAt(move).x;
        y = legalMoves.ElementAt(move).y;
        ScreenManager.board.OnClick(new Vector2(x, y));
    }
}