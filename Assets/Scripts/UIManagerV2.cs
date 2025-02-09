using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Constraints;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class UIManagerV2 : MonoBehaviour
{
    public static UIManagerV2 instance;

    public bool isGameRunning = false;
    float elapsedTime = 0f;

    [SerializeField] public GameObject mainMenuUI;
    [SerializeField] public GameObject gameplayUI;
    [SerializeField] GameObject rulesUI;
    [SerializeField] public GameObject findMoreBonesUI;
    [SerializeField] public GameObject bearTrapUI;
    [SerializeField] public GameObject gameWinUI;
    [SerializeField] public GameObject gameOverUI;
    [SerializeField] public GameObject stopWatchTimer;
    [SerializeField] public Slider progressBar;
    [SerializeField] Image healthStage;
    [SerializeField] Image watchStage;
    [SerializeField] Image coffeeStage;
    [SerializeField] Image keyStage;
    [SerializeField] Sprite[] health;
    [SerializeField] TMP_Text boneCounterTXT;
    [SerializeField] TMP_Text gameTimerTXT;
    [SerializeField] public TMP_Text stopWatchTimerTXT;
    [SerializeField] Animator anim;


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
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            TimerUpdate();
        }
    }


    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "PlayGame":
                GameManagerV2.instance.HandleScenes("MainMenu");
                break;
            case "Rules":
                rulesUI.SetActive(true);
                break;
            case "ExitGame":
                Application.Quit();
                break;
            case "CloseRules":
                rulesUI.SetActive(false);
                break;
            case "Replay":
                SceneManager.LoadScene("LevelOne");
                mainMenuUI.SetActive(false);
                gameplayUI.SetActive(true);
                gameOverUI.SetActive(false);
                gameWinUI.SetActive(false);
                isGameRunning = true;
                elapsedTime = 0;
                GameManagerV2.instance.RestartGame();
                PlayerManagerV2.instance.speed = 5;
                PlayerManagerV2.instance.isAlive = true;
                PlayerManagerV2.instance.gameObject.GetComponent<SphereCollider>().enabled = true;
                PlayerManagerV2.instance.gameObject.transform.position = new Vector3(24.6f, 1f, -21f);
                CameraManagerV2.instance.followPlayer = true;
                UpdateUI();
                break;
        }
    }


    public void UpdateUI()
    {
        boneCounterTXT.text = GameManagerV2.instance.score.ToString();
        HealthUpdate();
        WatchUpdate();
        CoffeeUpdate();
        KeyUpdate();
    }


    void HealthUpdate()
    {
        switch (GameManagerV2.instance.health)
        {
            case 0:
                healthStage.sprite = health[0];
                break;
            case 1:
                healthStage.sprite = health[0];
                break;
            case 2:
                healthStage.sprite = health[1];
                break;
            case 3:
                healthStage.sprite = health[2];
                break;
        }
    }


    void WatchUpdate()
    {
        if (GameManagerV2.instance.hasWatch)
        {
            Color color = watchStage.color;
            color.a = Mathf.Clamp01(1f);
            watchStage.color = color;
        }
        else
        {
            Color color = watchStage.color;
            color.a = Mathf.Clamp01(.2f);
            watchStage.color = color;
        }
    }


    void CoffeeUpdate()
    {
        if (GameManagerV2.instance.hasCoffee)
        {
            Color color = coffeeStage.color;
            color.a = Mathf.Clamp01(1f);
            coffeeStage.color = color;
        }
        else
        {
            Color color = coffeeStage.color;
            color.a = Mathf.Clamp01(.2f);
            coffeeStage.color = color;
        }
    }


    void KeyUpdate()
    {
        if (GameManagerV2.instance.hasKey)
        {
            Color color = keyStage.color;
            color.a = Mathf.Clamp01(1f);
            keyStage.color = color;
        }
        else
        {
            Color color = keyStage.color;
            color.a = Mathf.Clamp01(.2f);
            keyStage.color = color;
        }
    }


    void TimerUpdate()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        gameTimerTXT.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }


    public IEnumerator ShowMoreBonesUI()
    {
        findMoreBonesUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        findMoreBonesUI.SetActive(false);
    }


    public IEnumerator sceneTransitonsOne(string scene)
    {
        anim.SetTrigger("transTrigger");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(scene);
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        isGameRunning = true;
        UpdateUI();
        SoundManagerV2.instance.PlayMusic(SoundManagerV2.instance.audioClips[1]);
    }


    public IEnumerator sceneTransitonsTwo(string scene)
    {
        anim.SetTrigger("transRetrigger");
        PlayerManagerV2.instance.rb.angularVelocity = Vector3.zero;
        PlayerManagerV2.instance.rb.linearVelocity = Vector3.zero;
        PlayerManagerV2.instance.speed = 0f;

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(scene);
        PlayerManagerV2.instance.transform.position = new Vector3(5.54f, 1f, -17.71f);
        PlayerManagerV2.instance.rb.linearVelocity = Vector3.zero;
        PlayerManagerV2.instance.rb.angularVelocity = Vector3.zero;
        PlayerManagerV2.instance.speed = 5f;
    }
}

