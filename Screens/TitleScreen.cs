using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

public class TitleScreen
{
    private Button singlePlayer;
    private Button multiPlayer;
    public int State { get; set; }  // 0 titlescreen, 1 singleplayer, 2 multiplayer
    public TitleScreen()
    {
        Texture2D texture = LoadTexture("../../../res/buttons/SinglePlayer.png");
        this.singlePlayer = new(texture, 2, new Vector2(GetScreenWidth() / 4, GetScreenHeight() / 4));
        texture = LoadTexture("../../../res/buttons/MultiPlayer.png");
        this.multiPlayer = new(texture, 2, new Vector2(GetScreenWidth() / 4, GetScreenHeight() / 2));
        this.State = 0;
    }

    public override void Update()
    {
        this.singlePlayer.Update(GetMousePosition());
        this.multiPlayer.Update(GetMousePosition());
        if (this.singlePlayer.State == 2)
        {
            this.State = 1;
        }
        else if(this.multiPlayer.State == 2)
        {
            this.State = 2;
        }
    }

    public override void Draw()
    {
        this.singlePlayer.Draw();
        this.multiPlayer.Draw();
    }

    public override void Unload()
    {
        this.singlePlayer.Unload();
        this.multiPlayer.Unload();
    }
}
