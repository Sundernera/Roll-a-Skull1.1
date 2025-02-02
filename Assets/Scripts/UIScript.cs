using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameManager gameManager;

    public static UIScript instance;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject rulesUI;
    [SerializeField] Image healthStage;
    [SerializeField] Image watchStage;
    [SerializeField] Image coffeeStage;
    [SerializeField] Sprite[] health;
    [SerializeField] TMP_Text boneCountTXT;
    [SerializeField] TMP_Text gameTimer;

    bool isGameRunning = false;
    float elapsedTime = 0f;

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
            UpdateTimerDisplay();
        }
    }

    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "PlayGameButton":
                SceneManager.LoadScene("LevelOne");
                soundManager.ChangeMusic(1);
                mainMenuUI.SetActive(false);
                gameplayUI.SetActive(true);
                isGameRunning = true;
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

    public void UpdateUI()
    {
        boneCountTXT.text = gameManager.score.ToString();
        HealthUpdate();
        WatchUpdate();
        CoffeeUpdate();
    }

    void HealthUpdate()
    {
        switch (gameManager.health)
        {
            case 0:
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
        if (gameManager.hasWatch)
        {
            Color color = watchStage.color;
            color.a = 1f;
        }
        else
        {
            Color color = watchStage.color;
            color.a = .5f;
        }
    }

    void CoffeeUpdate()
    {
        if (gameManager.hasCoffee)
        {
            Color color = coffeeStage.color;
            color.a = 1f;
        }
        else
        {
            Color color = coffeeStage.color;
            color.a = .5f;
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        gameTimer.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
