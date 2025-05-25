using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance;
    public bool isInGame = false;

    public RectTransform notificationPanel;

    public List<GameType> totalGameList = new List<GameType>();
    public GameType nextGame;
    public GameType curGame;

    public GameObject curGamePanel;
    public TextMeshProUGUI curGameCountdownText;
    public float currentGameCountdown;

    public bool isNextGameReady;
    public float nextGameCountdown;


    public TextMeshProUGUI notificationTitleText;
    public TextMeshProUGUI countdownText;


    public float notificatonFreq = 25f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        curGamePanel.gameObject.SetActive(false);
    }
    void Start()
    {
        ShowNotification();
    }

    public void ShowNotification()
    {
        SelectNextGame();
        notificationPanel.gameObject.SetActive(true);
        notificationPanel.DOAnchorPosX(-415.8324f, .2f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
           
        });
    }
    public void HideNotification()
    {
        notificationPanel.DOAnchorPosX(-300, .2f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            notificationPanel.gameObject.SetActive(false);
        });

    }

    private void Update()
    {
        if (currentGameCountdown<=0 && isInGame)
        {
            SceneManager.LoadScene(0);
        }

        if (currentGameCountdown>=0 && isInGame)
        {
            currentGameCountdown -= Time.deltaTime;
            curGameCountdownText.text = $"{currentGameCountdown:0.00}";
        }

        if (isNextGameReady)
        {
            nextGameCountdown -= Time.deltaTime;
            countdownText.text = $"{nextGameCountdown:0.00}";
        }

        if (!isNextGameReady && notificatonFreq<0)
        {
            ShowNotification();
        }
        else
        {
            notificatonFreq -= Time.deltaTime;
        }
    }

    public void SelectNextGame()
    {
        isNextGameReady = true;
        nextGame = totalGameList[Random.Range(0, totalGameList.Count)];
        notificationTitleText.text = nextGame.gameName;
        nextGameCountdown = 90f;
    }
    public bool LoadNextGame(bool type, UiMiniGameType uiType, string sceneName)
    {
        Debug.Log(uiType);
        Debug.Log(nextGame.uiMiniGameType);
        if (type)
        {
            if (sceneName != nextGame.scene)
            {
                return false;
            }
        }
        else
        {
            if (uiType != nextGame.uiMiniGameType)
            {
                return false;
            }
        }
        curGamePanel.gameObject.SetActive(true);
        HideNotification();
        isInGame = true;
        curGame = nextGame;
        currentGameCountdown = nextGameCountdown;
        isNextGameReady = false;
        nextGame = null;
        notificatonFreq = 25f;
        return true;
    }
    public void ReturnToMainMenu()
    {
        isInGame = false;
        curGamePanel.gameObject.SetActive(false);
        if (!isNextGameReady)
        {
            ShowNotification();
        }

    }

}
