using System.Collections.Generic;

public class Constants
{
    public const string JoinKey = "j";
    public const string DifficultyKey = "d";
    public const string GameTypeKey = "t";

    public const int MAX_PLAYER = 2;

    public static readonly List<string> GameTypes = new() { "Normal", "Friends", "Rank" };
    
    public static readonly List<string> Difficulties = new() { "None", "Basic", "Medium", "Hard", "Extreme", "Nightmare" };
    // None: for Friends, Normal
    // Basic: for Rank 1 - 2
    // Medium: for Rank 3 - 4
    // Hard: for Rank 5
    // Extreme: for Rank 6
    // Nightmare: for Rank 7
}