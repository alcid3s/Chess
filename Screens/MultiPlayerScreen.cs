using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

public class MultiPlayerScreen : Screen
{
    private Button host;
    private Button join;
    public int State { get; set; }  // 1 hostScreen, 2 clientScreen
    public MultiPlayerScreen()
    {
        Texture2D texture = LoadTexture("../../../res/buttons/Host.png");
        this.host = new(texture, 2, new Vector2(GetScreenWidth() / 4, GetScreenHeight() / 4));
        texture = LoadTexture("../../../res/buttons/Join.png");
        this.join = new(texture, 2, new Vector2(GetScreenWidth() / 4, GetScreenHeight() / 2));
        this.State = 0;
    }

    public override void Update()
    {
        this.host.Update(GetMousePosition());
        this.join.Update(GetMousePosition());
        if (this.host.State == 2)
        {
            this.State = 1;
        }
        else if (this.join.State == 2)
        {
            this.State = 2;
        }

    }
    public override void Draw()
    {
        this.host.Draw();
        this.join.Draw();
    }
    public override void Unload()
    {
        this.host.Unload();
        this.join.Unload();
    }
}