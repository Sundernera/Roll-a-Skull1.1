using System.Collections;
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
    public bool stopWatchTimerActive = false;
    public bool isSpeedBoosted = false;
    Ghost[] ghosts;


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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hasWatch)
            {
                ghosts = FindObjectsByType<Ghost>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                StartCoroutine(TemporaryStop());
                hasWatch = false;
                UIManagerV2.instance.UpdateUI();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hasCoffee)
            {
                StartCoroutine(TemporarySpeed(6f, 4f));
                hasCoffee = false;
                UIManagerV2.instance.UpdateUI();
            }
        }
    }


    public void HandleScenes(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            SceneManager.LoadScene("LevelOne");
            SoundManagerV2.instance.PlayMusic(SoundManagerV2.instance.audioClips[1]);
        }
        if (sceneName == "LevelOne")
        {
            SceneManager.LoadScene("LevelTwo");
        }
    }


    IEnumerator TemporaryStop()
    {
        stopWatchTimerActive = true;

        foreach (Ghost ghost in ghosts)
        {
            ghost.StopMovement();
        }

        UIManagerV2.instance.stopWatchTimer.gameObject.SetActive(true);
        float countdown = 4f;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UIManagerV2.instance.stopWatchTimerTXT.text = countdown.ToString("F1");
            yield return null;
        }
        UIManagerV2.instance.stopWatchTimer.SetActive(false);

        foreach (Ghost ghost in ghosts)
        {
            ghost.ResumeMovement();
        }
        stopWatchTimerActive = false;
    }


    IEnumerator TemporarySpeed(float newSpeed, float duration)
    {
        isSpeedBoosted = true;
        float ogSpeed = PlayerManagerV2.instance.speed;
        PlayerManagerV2.instance.speed = newSpeed;

        yield return new WaitForSeconds(duration);

        PlayerManagerV2.instance.speed = ogSpeed;
        isSpeedBoosted = false;
    }
}
