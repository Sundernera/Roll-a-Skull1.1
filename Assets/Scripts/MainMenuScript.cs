using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;

    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "PlayGameButton":
                SceneManager.LoadScene("LevelOne");
                soundManager.ChangeMusic(1);
                break;
            case "RulesButton":
                break;
            case "ExitButton":
                Application.Quit();
                break;
        }
    }
}
