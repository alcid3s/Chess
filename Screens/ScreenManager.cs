using Raylib_cs;
using static Raylib_cs.Raylib;

public class ScreenManager : Screen
{
    private TitleScreen titleScreen;
    private ChooseSideScreen chooseSideScreen;
    private MultiPlayerScreen multiPlayerScreen;
    private HostScreen hostScreen;

    public int State;  // state -1 playing, state 0 Titlescreen, state 1 Choose side,
                       // state 2 MultiPlayer, state 3 hostScreen, state 4 clientscreen

    public static bool SinglePlayer;
    public bool CheckMate { get; set; }
    public static Board? board;
    public ScreenManager()
    {
        this.State = 0;
        this.titleScreen = new TitleScreen();
        this.chooseSideScreen = new ChooseSideScreen();
        this.multiPlayerScreen = new MultiPlayerScreen();
        this.hostScreen = new HostScreen();
    }

    public override void Update()
    {
        if (this.State == -1)
        {
            board.Update();
        }
        else if (this.State == 0)
        {
            this.titleScreen.Update();
            if (this.titleScreen.State == 1)
            {
                this.State = 1;
            }
            else if (this.titleScreen.State == 2)
            {
                this.State = 2;
                SinglePlayer = true;
            }
        }
        else if (this.State == 1)
        {
            this.chooseSideScreen.Update();
            if (this.chooseSideScreen.State == 1)
            {
                // player chooses white
                board = new(true);
                board.LoadPieces(Program.standard);
                this.State = -1;
            }
            else if (this.chooseSideScreen.State == 2)
            {
                // player chooses black
                board = new(false);
                board.LoadPieces(Program.standard);
                this.State = -1;
            }
            else if (this.chooseSideScreen.State == 3)
            {
                // player chooses random
                Random random = new();
                bool boo = random.Next(2) == 1 ? true : false;

                board = new(boo);

                board.LoadPieces(Program.standard);
                this.State = -1;
            }
        }
        else if (this.State == 2)
        {
            this.multiPlayerScreen.Update();
            if(this.multiPlayerScreen.State == 1)
            {
                this.State = 3;
            }else if(this.multiPlayerScreen.State == 2)
            {
                this.State = 4;
            }
        }
        else if(this.State == 3)
        {
            // hostScreen
        }
    }

    public override void Draw()
    {
        if (this.State == -1)
        {
            board.Draw();
        }
        else if (this.State == 0)
        {
            this.titleScreen.Draw();
        }
        else if (this.State == 1)
        {
            this.chooseSideScreen.Draw();
        }
        else if(this.State == 2)
        {
            this.multiPlayerScreen.Draw();
        }
    }

    public override void Unload()
    {
        this.titleScreen.Unload();
        this.chooseSideScreen.Unload();
        this.multiPlayerScreen.Unload();
        this.hostScreen.Unload();
    }
}