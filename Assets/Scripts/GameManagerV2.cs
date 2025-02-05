using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerV2 : MonoBehaviour
{
    public static GameManagerV2 instance;

    public int score = 0;
    public int health = 3;
    public bool hasWatch = false;
    public bool hasKey = false;
    public bool hasCoffee = false;


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


    public void HandleScenes(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            SceneManager.LoadScene("LevelOne");
        }
        if (sceneName == "LevelOne")
        {
            SceneManager.LoadScene("LevelTwo");
        }
    }
}
