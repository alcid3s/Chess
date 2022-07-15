using Raylib_cs;
using static Raylib_cs.Raylib;

public class ScreenManager
{
    private TitleScreen titleScreen;
    private ChooseSideScreen chooseSideScreen;
    private MultiPlayerScreen multiPlayerScreen;


    private int state;  // state -1 playing, state 0 Titlescreen, state 1 Choose side,
                        // state 2 MultiPlayer, state 4 ClientScreen, state 5 hostScreen

    private bool playing;
    public bool CheckMate { get; set; }
    public static Board? board;
    public ScreenManager()
    {
        this.state = 0;
        this.playing = false;
        this.titleScreen = new TitleScreen();
        this.chooseSideScreen = new ChooseSideScreen();
        this.multiPlayerScreen = new MultiPlayerScreen();
    }

    public void Update()
    {
        if (this.state == -1)
        {
            board.Update();
        }
        else if (this.state == 0)
        {
            this.titleScreen.Update();
            if (this.titleScreen.State == 1)
            {
                this.state = 1;
            }
            else if (this.titleScreen.State == 2)
            {
                this.state = 2;
            }
        }
        else if (this.state == 1)
        {
            this.chooseSideScreen.Update();
            if (this.chooseSideScreen.State == 1)
            {
                // player chooses white
                board = new(true);
                board.LoadPieces(Program.standard);
                this.state = -1;
            }
            else if (this.chooseSideScreen.State == 2)
            {
                // player chooses black
                board = new(false);
                board.LoadPieces(Program.standard);
                this.state = -1;
            }
            else if (this.chooseSideScreen.State == 3)
            {
                // player chooses random
                Random random = new();
                bool boo = random.Next(2) == 1 ? true : false;

                board = new(boo);

                board.LoadPieces(Program.standard);
                this.state = -1;
            }
        }
        else if (this.state == 2)
        {
            this.multiPlayerScreen.Update();
        }
    }

    public void Draw()
    {
        if (this.state == -1)
        {
            board.Draw();
        }
        else if (this.state == 0)
        {
            this.titleScreen.Draw();
        }
        else if (this.state == 1)
        {
            this.chooseSideScreen.Draw();
        }
        else if(this.state == 2)
        {
            this.multiPlayerScreen.Draw();
        }
    }

    public void Unload()
    {
        this.titleScreen.Unload();
        this.chooseSideScreen.Unload();
        this.multiPlayerScreen.Unload();
    }
}