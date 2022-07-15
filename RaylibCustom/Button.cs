using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

public class Button
{
    public int State { get; set; }
    private Texture2D texture;
    private Rectangle buttonFilter;
    private Rectangle locationOnScreen;
    private int frameHeight;
    public Button(Texture2D texture, int amountOfButtons, Vector2 startPosition)
    {
        this.texture = texture;
        this.frameHeight = texture.height / amountOfButtons;
        this.buttonFilter = new(0, 0, texture.width, texture.height / amountOfButtons);
        this.locationOnScreen = new(startPosition.X, startPosition.Y, texture.width, texture.height / amountOfButtons);
    }

    public void Update(Vector2 mousePosition)
    {
       if(CheckCollisionPointRec(mousePosition, locationOnScreen))
        {
            this.State = 1;
            if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                this.State = 2;
            }
        }
        else
        {
            this.State = 0;
        }

        this.buttonFilter.y = State * frameHeight;
    }

    public void Draw()
    {
         DrawTextureRec(this.texture, this.buttonFilter, new Vector2(this.locationOnScreen.x, this.locationOnScreen.y), WHITE);
    }

    public void Unload()
    {
        UnloadTexture(this.texture);
    }
}