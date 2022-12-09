public class Leaderboard
{
    public int highestTopScore { get; private set; }
    public int lowestTopScore { get; private set; }

    private const int size = 3;
    
    public PlayerResult[] PlayerResults { get; private set; }

    public Leaderboard()
    {
        PlayerResults = new PlayerResult[size];

        for (int i = 0; i < size; i++)
            PlayerResults[i] = new();

        lowestTopScore = 0;
        highestTopScore = 0;
    }

    public Leaderboard(PlayerResult[] playerResults)
    {
        PlayerResults = playerResults;

        for (int i = 0; i < size; i++)
        {
            if (PlayerResults[i].Score > highestTopScore)
                highestTopScore = PlayerResults[i].Score;

            if(PlayerResults[i].Score < lowestTopScore)
                lowestTopScore = PlayerResults[i].Score;
        }
    }

    public bool ApplyToLeaderboard(PlayerResult playerResult)
    {
        bool isApplied = false;

        if (playerResult.Score > lowestTopScore)
        {
            PlayerResults[size - 1] = playerResult;
            lowestTopScore = playerResult.Score;

            if (playerResult.Score > highestTopScore)
                highestTopScore = playerResult.Score;

            for (int i = size - 2; i >= 0; i--)
            {
                if (PlayerResults[i].Score < lowestTopScore)
                    lowestTopScore = PlayerResults[i].Score;

                if (PlayerResults[i].Score < playerResult.Score)
                    (PlayerResults[i], PlayerResults[i + 1]) = (PlayerResults[i + 1], PlayerResults[i]);
                else break;
            }

            isApplied = true;
        }

        return isApplied;
    }
}

[System.Serializable]
public class PlayerResult
{
    public string PlayerName;
    public int Score;

    public PlayerResult()
    {
        PlayerName = "---//---//---";
        Score = 0;
    }

    public PlayerResult(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}