using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance;
    public bool isInGame = false;

    public RectTransform notificationPanel;

    public List<GameType> totalGameList = new List<GameType>();
    GameType nextGame;
    GameType curGame;


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
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        ShowNotification();
    }

    public void ShowNotification()
    {
        SelectNextGame();
        notificationPanel.gameObject.SetActive(true);
        notificationPanel.DOAnchorPosX(0, .2f).SetEase(Ease.InOutCubic).OnComplete(() =>
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
        if (currentGameCountdown>=0)
        {
            currentGameCountdown -= Time.deltaTime;
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
        GameType nextGame = totalGameList[Random.Range(0, totalGameList.Count)];
        notificationTitleText.text = nextGame.gameName;
        nextGameCountdown = 60f;
    }
    public void LoadNextGame()
    {
        if (curGame.isGameFinished)
        {
            curGame = nextGame;
            currentGameCountdown = nextGameCountdown;
            isNextGameReady = false;
            notificatonFreq = 25f;
        }
    }

}
