using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

public class MultiPlayerScreen
{
    private Button host;
    private Button join;
    public MultiPlayerScreen()
    {
        Texture2D texture = LoadTexture("../../../res/buttons/Host.png");
        this.host = new(texture, 2, new Vector2(GetScreenWidth() / 4, GetScreenHeight() / 4));
        texture = LoadTexture("../../../res/buttons/Join.png");
        this.join = new(texture, 2, new Vector2(GetScreenWidth() / 4, GetScreenHeight() / 2));
    }

    public void Update()
    {
        this.host.Update(GetMousePosition());
        this.join.Update(GetMousePosition());

    }
    public void Draw()
    {
        this.host.Draw();
        this.join.Draw();
    }
    public void Unload()
    {
        this.host.Unload();
        this.join.Unload();
    }
}