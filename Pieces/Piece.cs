﻿using Raylib_cs;
using static Raylib_cs.Raylib;
public abstract class Piece
{
    protected Image image;
    protected Texture2D texture;
    protected bool white;
    protected Tile? assignedTile = null;

    private bool hasMoved;
    public Piece(bool white)
    {
        this.white = white;
        this.hasMoved = false;
        this.image = LoadImage("../../../res/pieces.png");
    }

    public abstract List<Tile> CalculateLegalMoves(Tile startTile, bool ownPawns);

    public bool GetColor()
    {
        return this.white;
    }

    public Texture2D GetTexture()
    {
        return this.texture;
    }

    public void setAssignedTile(Tile tile)
    {
        this.assignedTile = tile;
    }

    public Tile GetAssignedTile()
    {
        return this.assignedTile;
    }

    public void HasMoved()
    {
        this.hasMoved = true;
    }
}