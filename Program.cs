using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.TraceLogLevel;

public class Program
{
    public const string scholarsMate = "rnbqkbnr/1ppppppp/8/8/p1B/4PQ/PPPP1PPP/RNB1K1NR w";
    public const string reverseScholarsMate = "rnb1k1nr/pppp1ppp/4pq/P1b/8/8/1PPPPPPP/RNBQKBNR b";
    public const string checkMateBug = "rnb2bnr/1pp2kpp/5q/8/p4N/5p/PPP3PP/RNB1K2R b";
    public const string standard = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w";
    public const string toStudy = "8/1k4q/1ppp1p/2n1p/2P1P/P2P3Q/7P/2B4K w";
    public const string casteling = "r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w";
    public const string test = "5p/5r/5n/5b/5q/5k/8/P/ b";
    public static void Main()
    {
        const int screenWidth = 768;
        const int screenHeight = 768;
        InitWindow(screenWidth, screenHeight, "Chess");

        SetTraceLogLevel(LOG_NONE);

        ScreenManager screenManager = new();
        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.GREEN);
            screenManager.Update();
            screenManager.Draw();
            EndDrawing();
        }
    }

    private static void Update()
    {

    }
}