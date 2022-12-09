using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public UIMainSceneHandler UIHandler;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        UIHandler.SetScoreText(m_Points);

        if (CachedData.Instance.Leaderboard.highestTopScore < m_Points)
            UIHandler.SetHighestScoreText(m_Points);
    }

    public void GameOver()
    {
        if (m_GameOver) return;

        GameOverType gameOverType = m_Points > CachedData.Instance.Leaderboard.lowestTopScore ? GameOverType.TopScore : GameOverType.Default;
        UIHandler.ActivateGameOverMenu(gameOverType);

        if (gameOverType == GameOverType.TopScore)
            UIHandler.onTopScoreSubmit += SaveScore;

        m_GameOver = true;
    }

    private void SaveScore(string playerName)
    {
        PlayerResult playerResult = new(playerName, m_Points);
        CachedData.Instance.SavePlayerResult(playerResult);

        UIHandler.onTopScoreSubmit -= SaveScore;
    }
}
