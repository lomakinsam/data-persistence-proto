using UnityEngine;
using System.IO;

public class CachedData : MonoBehaviour
{
    public static CachedData Instance { get; private set; }

    public Leaderboard Leaderboard { get; private set; }

    private void Awake()
    {
        InitSingleton();
        LoadCachedData();
    }

    private void InitSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LoadCachedData()
    {
        string saveFilePath = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerResultGroup playerResultGroup = JsonUtility.FromJson<PlayerResultGroup>(json);
            Leaderboard = new(playerResultGroup.PlayerResults);
        }
        else
            Leaderboard = new();
    }

    public void SavePlayerResult(PlayerResult playerResult)
    {
        bool isApplied = Leaderboard.ApplyToLeaderboard(playerResult);

        if (isApplied)
        {
            string saveFilePath = Application.persistentDataPath + "/savefile.json";
            PlayerResultGroup playerResultGroup = new(Leaderboard.PlayerResults);
            string json = JsonUtility.ToJson(playerResultGroup);
            File.WriteAllText(saveFilePath, json);
        }
    }
}

[System.Serializable]
public class PlayerResultGroup
{
    public PlayerResult[] PlayerResults;

    public PlayerResultGroup(PlayerResult[] playerResults) => PlayerResults = playerResults;
}