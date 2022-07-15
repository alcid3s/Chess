using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

public class ChooseSideScreen
{
    private Button white;
    private Button black;
    private Button random;

    public int State { get; set; }
    public ChooseSideScreen()
    {
        this.State = 0;
        int y = GetScreenHeight() / 8;
        Texture2D texture = LoadTexture("../../../res/buttons/White.png");
        this.white = new(texture, 2, new Vector2(GetScreenWidth() / 4, y));
        texture = LoadTexture("../../../res/buttons/Black.png");
        this.black = new(texture, 2, new Vector2(GetScreenWidth() / 4, y + (texture.height / 2)));
        texture = LoadTexture("../../../res/buttons/Random.png");
        this.random= new(texture, 2, new Vector2(GetScreenWidth() / 4, y + texture.height));
    }

    public void Update()
    {
        this.white.Update(GetMousePosition());
        this.black.Update(GetMousePosition());
        this.random.Update(GetMousePosition());

        if(this.white.State == 2)
        {
            this.State = 1;
        }else if(this.black.State == 2)
        {
            this.State = 2;
        }
        else if(this.random.State == 2)
        {
            this.State = 3;
        }
    }
    public void Draw()
    {
        this.white.Draw();
        this.black.Draw();
        this.random.Draw();
    }

    public void Unload()
    {
        this.white.Unload();
        this.black.Unload();
        this.random.Unload();
    }
}