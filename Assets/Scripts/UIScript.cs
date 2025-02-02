using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject rulesUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "PlayGameButton":
                SceneManager.LoadScene("LevelOne");
                soundManager.ChangeMusic(1);
                MainMenuUIInactive();
                break;
            case "RulesButton":
                break;
            case "ExitButton":
                Application.Quit();
                break;
            case "CloseRules":
                break;
        }
    }

    void MainMenuUIInactive()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
    }
}
