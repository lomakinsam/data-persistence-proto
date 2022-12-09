using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class UIMainSceneHandler : MonoBehaviour
{
    [SerializeField] private Button[] topScoreSubmitButtons;
    [SerializeField] private Text playerScoreValueText;
    [SerializeField] private Text highestScoreValueText;
    [SerializeField] private InputField playerNameInputField;
    [SerializeField] private GameObject gameOverMenuDefault;
    [SerializeField] private GameObject gameOverMenuTopScore;

    public System.Action<string> onTopScoreSubmit;

    private void Start()
    {
        LoadHighestScoreText();

        for (int i = 0; i < topScoreSubmitButtons.Length; i++)
            topScoreSubmitButtons[i].onClick.AddListener(SubmitPlayerName);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < topScoreSubmitButtons.Length; i++)
            topScoreSubmitButtons[i].onClick.RemoveAllListeners();
    }

    private void SubmitPlayerName() => onTopScoreSubmit.Invoke(playerNameInputField.text);

    public void LoadMenu() => SceneManager.LoadScene(0);

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void SwitchSubmitButtons(string value)
    {
        for (int i = 0; i < topScoreSubmitButtons.Length; i++)
        {
            if (value.Length == 0)
                topScoreSubmitButtons[i].gameObject.SetActive(false);
            else
                topScoreSubmitButtons[i].gameObject.SetActive(true);
        }
    }

    public void SetScoreText(int value) => playerScoreValueText.text = $"Score : {value}";

    public void SetHighestScoreText(int value)
    {
        if (value > 0)
            highestScoreValueText.gameObject.SetActive(true);

        highestScoreValueText.text = $"Highest Score : {value}";
    }

    private void LoadHighestScoreText() => SetHighestScoreText(CachedData.Instance.Leaderboard.highestTopScore);

    public void ActivateGameOverMenu(GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameOverType.Default:
                gameOverMenuDefault.SetActive(true);
                break;
            case GameOverType.TopScore:
                gameOverMenuTopScore.SetActive(true);
                break;
        }
    }
}

public enum GameOverType { Default, TopScore }