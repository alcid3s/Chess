using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;

public class Board
{
    private Tile[] tiles = new Tile[64];
    public static Texture2D tile1 = LoadTexture("../../../res/Tile1.png");
    public static Texture2D tile2 = LoadTexture("../../../res/Tile2.png");
    public static Texture2D tile3 = LoadTexture("../../../res/Tile3.png");
    public static Texture2D tile4 = LoadTexture("../../../res/Tile4.png");
    public static Texture2D tile5 = LoadTexture("../../../res/Tile5.png");

    private Piece? selectedPiece = null;

    private bool whiteHasTurn;
    public Board(bool side)
    {
        char[] name = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        if (side)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int i = x + (y * 8);
                    bool light = (x + y) % 2 == 0;
                    tiles[i] = new(light ? tile1 : tile2, x * tile1.width, y * tile1.height, light, i);
                }
            }
        }
        else
        {
            for(int y = 0; y < 8; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    int i = x + (y * 8);
                    bool light = (x + y) % 2 == 0;
                    tiles[i] = new(light ? tile1 : tile2, GetScreenWidth() - (tile1.width * (x + 1)), GetScreenHeight() - (tile1.height * (y + 1)), light, i);
                }
            }
        }

    }

    public void Draw()
    {
        foreach (Tile tile in tiles)
        {
            tile.Draw();
        }
    }

    public void OnClick(Vector2 mousePosition)
    {
        foreach (Tile tile in tiles)
        {
            bool clicked = tile.OnClick(mousePosition);

            if (clicked)
            {
                Console.WriteLine("clicked on: " + tile.GetPositionOnBoard());
            }
            // If nothing is selected yet
            if (clicked && this.selectedPiece == null && tile.ContainsPiece())
            {
                if (whiteHasTurn && tile.GetPieceOnTile() != null && tile.GetPieceOnTile().GetColor().Equals(true))
                {
                    this.selectedPiece = tile.GetPieceOnTile();
                    this.selectedPiece.setAssignedTile(tile);


                }
                else if (!whiteHasTurn && tile.GetPieceOnTile().GetColor().Equals(false))
                {
                    this.selectedPiece = tile.GetPieceOnTile();
                    this.selectedPiece.setAssignedTile(tile);
                }

                if (this.selectedPiece != null && this.selectedPiece.GetAssignedTile() != null)
                {
                    List<Tile> legalMoves = this.selectedPiece.CalculateLegalMoves(this.selectedPiece.GetAssignedTile(), false);

                    if (legalMoves.Any())
                    {
                        legalMoves.ForEach(tile =>
                        {
                            tile.SetTexture(tile.Even ? tile3 : tile4);
                        });
                    }
                }
            }

            // if a piece is selected and the user tries to replace the location of the piece to another tile.
            else if (clicked && this.selectedPiece != null && this.selectedPiece.GetAssignedTile() != null && !tile.ContainsPiece()
                || clicked && this.selectedPiece != null && this.selectedPiece.GetAssignedTile() != null && tile.ContainsPiece()
                && tile.GetPieceOnTile().GetColor() != this.selectedPiece.GetColor())
            {
                List<Tile> legalMoves = this.selectedPiece.CalculateLegalMoves(this.selectedPiece.GetAssignedTile(), false);

                // check if it is allowed to move piece to this tile
                if (legalMoves.Contains(tile))
                {
                    if (legalMoves.Any())
                    {
                        legalMoves.ForEach(tile =>
                        {
                            tile.SetTexture(tile.Even ? tile1 : tile2);
                        });
                    }

                    // check for casteling
                    if (typeof(King).Equals(this.selectedPiece.GetType()))
                    {
                        // if the king is a white king
                        if (this.selectedPiece.GetColor().Equals(true))
                        {
                            // Check if king is in starting position
                            if (this.selectedPiece.GetAssignedTile().GetPositionOnBoard() == 60)
                            {
                                // check if user wants to castle kingside
                                if (tile.GetPositionOnBoard() == 62)
                                {
                                    CastleKingSide(tile);
                                }

                                // Check if user wants to castle queenside
                                else if (tile.GetPositionOnBoard() == 58)
                                {
                                    CastleQueenSide(tile);
                                }
                            }
                        }

                        // if the king is a black king
                        else
                        {
                            // Check if king is in starting position
                            if (this.selectedPiece.GetAssignedTile().GetPositionOnBoard() == 4)
                            {
                                if (tile.GetPositionOnBoard() == 6)
                                {
                                    CastleKingSide(tile);
                                }
                                else if (tile.GetPositionOnBoard() == 2)
                                {
                                    CastleQueenSide(tile);
                                }
                            }
                        }
                    }

                    tile.Assign(this.selectedPiece);
                    this.selectedPiece.GetAssignedTile().Detach();
                    this.selectedPiece.setAssignedTile(tile);

                    // Looking for Check or CheckMate
                    this.selectedPiece.CalculateLegalMoves(this.selectedPiece.GetAssignedTile(), false).ForEach(tile =>
                    {
                        if (LookForCheckMate(tile))
                        {
                            Console.WriteLine("Check mate");
                        }
                    });

                    this.selectedPiece = null;
                }

                this.whiteHasTurn = !this.whiteHasTurn;
                if (this.whiteHasTurn)
                {
                    Console.WriteLine("White turn");
                }
                else
                {
                    Console.WriteLine("Black turn");
                }
            }
            else if (clicked && this.selectedPiece != null && this.selectedPiece.GetAssignedTile() != null && tile.ContainsPiece())
            {
                List<Tile> legalMoves = this.selectedPiece.CalculateLegalMoves(this.selectedPiece.GetAssignedTile(), false);

                if (legalMoves.Any())
                {
                    legalMoves.ForEach(tile =>
                    {
                        tile.SetTexture(tile.Even ? tile1 : tile2);
                    });
                }

                this.selectedPiece = tile.GetPieceOnTile();
                this.selectedPiece.setAssignedTile(tile);

                legalMoves = this.selectedPiece.CalculateLegalMoves(this.selectedPiece.GetAssignedTile(), false);

                if (legalMoves.Any())
                {
                    legalMoves.ForEach(tile =>
                    {
                        tile.SetTexture(tile.Even ? tile3 : tile4);
                    });
                }
            }
        }
    }

    private void CastleKingSide(Tile tile)
    {
        Tile? east = tile.GetEast(ScreenManager.board);
        if (east != null && east.ContainsPiece() && east.GetPieceOnTile().GetColor().Equals(this.selectedPiece.GetColor())
            && typeof(Rook).Equals(east.GetPieceOnTile().GetType()))
        {
            Tile? west = tile.GetWest(ScreenManager.board);
            if (west != null && !west.ContainsPiece())
            {
                west.Assign(east.GetPieceOnTile());
                east.Detach();
                west.GetPieceOnTile().setAssignedTile(west);
            }
        }
    }

    private void CastleQueenSide(Tile tile)
    {
        Tile west = tile.GetWest(ScreenManager.board);
        if (west != null && !west.ContainsPiece())
        {
            west = west.GetWest(ScreenManager.board);

            // check if there is a rook at the western end of the board
            if (west != null && west.ContainsPiece() && west.GetPieceOnTile().GetColor().Equals(this.selectedPiece.GetColor()
                && typeof(Rook).Equals(west.GetPieceOnTile().GetType())))
            {
                Tile newRookPosition = tile.GetEast(ScreenManager.board);
                if (newRookPosition != null && !newRookPosition.ContainsPiece())
                {
                    Console.WriteLine("Changing position for rook from: " + west.GetPositionOnBoard() + " to " + newRookPosition.GetPositionOnBoard());
                    newRookPosition.Assign(west.GetPieceOnTile());
                    west.Detach();
                    newRookPosition.GetPieceOnTile().setAssignedTile(newRookPosition);
                }
            }
        }
    }

    //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w
    public void LoadPieces(string fen)
    {
        var pieceType = new Dictionary<char, Piece>()
        {
            ['p'] = new Pawn(false),
            ['P'] = new Pawn(true),
            ['r'] = new Rook(false),
            ['R'] = new Rook(true),
            ['n'] = new Knight(false),
            ['N'] = new Knight(true),
            ['b'] = new Bishop(false),
            ['B'] = new Bishop(true),
            ['q'] = new Queen(false),
            ['Q'] = new Queen(true),
            ['k'] = new King(false),
            ['K'] = new King(true)
        };

        int x = 0, y = 0;
        bool whoMovesFirst = false;
        foreach (char c in fen)
        {
            if (whoMovesFirst && c == 'w')
            {
                Console.WriteLine("White starts");
                this.whiteHasTurn = true;
            }
            else if (whoMovesFirst && c == 'b')
            {
                Console.WriteLine("Black starts");
                this.whiteHasTurn = false;
            }
            else if (c == '/')
            {
                x = 0;
                y++;
            }
            else if (c == ' ')
            {
                whoMovesFirst = true;
            }
            else
            {
                if (char.IsDigit(c))
                {
                    x += (int)Char.GetNumericValue(c);
                }
                else
                {
                    Piece piece = pieceType[c];
                    Tile tile = GetTile(x, y);
                    Console.WriteLine("Placing " + piece.GetType() + " on position " + tile.GetPositionOnBoard());
                    tile.Assign(piece);
                    piece.setAssignedTile(tile);
                    x++;
                }
            }
        }
    }

    public void Update()
    {
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            OnClick(GetMousePosition());
        }
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x + (y * 8)];
    }

    public Tile[] GetTiles()
    {
        return tiles;
    }

    private bool LookForCheck(Tile tile)
    {
        if (this.selectedPiece != null && tile.ContainsPiece() && !tile.GetPieceOnTile().GetColor().Equals(this.selectedPiece.GetColor())
            && typeof(King).Equals(tile.GetPieceOnTile().GetType()))
        {
            tile.SetTexture(tile5);
            return true;
        }
        tile.SetTexture(tile.Even ? tile1 : tile2);
        return false;
    }

    private bool LookForCheckMate(Tile tile)
    {

        if (LookForCheck(tile))
        {
            Console.WriteLine("check");
            List<Tile> possibleKingMoves = tile.GetPieceOnTile().CalculateLegalMoves(tile, false);
            bool colorOfKing = tile.GetPieceOnTile().GetColor();

            HashSet<Tile> toRemove = new HashSet<Tile>();

            // Check if all moves the king can do are covered by the opposite side
            foreach (Tile t in tiles)
            {
                if (t.ContainsPiece() && t.GetPieceOnTile() != null)
                {

                    if (!t.GetPieceOnTile().GetColor().Equals(colorOfKing))
                    {
                        List<Tile> possiblePieceMoves = t.GetPieceOnTile().CalculateLegalMoves(t, true);

                        if (possiblePieceMoves.Any() && possibleKingMoves.Any())
                        {
                            possibleKingMoves.ForEach(kingMove =>
                            {
                                if (possiblePieceMoves.Contains(kingMove))
                                {
                                    Console.WriteLine(t.GetPieceOnTile().GetType() + " covers " + kingMove.GetPositionOnBoard());
                                    toRemove.Add(kingMove);
                                }
                            });
                        }
                    }
                    else if (t.GetPieceOnTile().GetColor().Equals(colorOfKing))
                    {
                        Tile tileOfInterest = this.selectedPiece.GetAssignedTile();
                        if (t.GetPieceOnTile().CalculateLegalMoves(t, false).Contains(tileOfInterest)
                            && !typeof(King).Equals(t.GetPieceOnTile().GetType()))
                        {
                            return false;
                        }
                    }
                }
            }

            possibleKingMoves.RemoveAll(toRemove.Contains);
            if (!possibleKingMoves.Any())
            {
                return true;
            }
        }
        return false;
    }
}