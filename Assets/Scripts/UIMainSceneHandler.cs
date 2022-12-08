using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainSceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] inputSensetiveButtons;

    public void LoadMenu() => SceneManager.LoadScene(0);

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void SwitchInputSensetiveButtons(string value)
    {
        for (int i = 0; i < inputSensetiveButtons.Length; i++)
        {
            if (value.Length == 0)
                inputSensetiveButtons[i].SetActive(false);
            else
                inputSensetiveButtons[i].SetActive(true);
        }
    }
}
