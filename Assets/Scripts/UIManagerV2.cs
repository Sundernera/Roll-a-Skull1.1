using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Constraints;

public class UIManagerV2 : MonoBehaviour
{
    public static UIManagerV2 instance;

    bool isGameRunning = false;
    float elapsedTime = 0f;

    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject rulesUI;
    [SerializeField] Image healthStage;
    [SerializeField] Image watchStage;
    [SerializeField] Image coffeeStage;
    [SerializeField] Image keyStage;
    [SerializeField] Sprite[] health;
    [SerializeField] TMP_Text boneCounterTXT;
    [SerializeField] TMP_Text gameTimerTXT;


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
                mainMenuUI.SetActive(false);
                gameplayUI.SetActive(true);
                isGameRunning = true;
                UpdateUI();
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
        }
    }   
    

    public void UpdateUI()
    {
        boneCounterTXT.text = GameManagerV2.instance.score.ToString();
        HealthUpdate();
        WatchUpdate();
        CoffeeUpdate();
        //KeyUpdate();
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
            color.a = 1f;
        }
        else
        {
            Color color = watchStage.color;
            color.a = .3f;
        }
    }


    void CoffeeUpdate()
    {
        if(GameManagerV2.instance.hasCoffee)
        {
            Color color = coffeeStage.color;
            color.a = 1f;
        }
        else
        {
            Color color = coffeeStage.color;
            color.a= .3f;
        }
    }


    void KeyUpdate()
    {
        if (GameManagerV2.instance.hasKey)
        {
            Color color = keyStage.color;
            color.a = 1f;
        }
        else
        {
            Color color = keyStage.color;
            color.a = .3f;
        }    
    }


    void TimerUpdate()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        gameTimerTXT.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}

