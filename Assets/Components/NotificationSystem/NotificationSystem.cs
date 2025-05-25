using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;




public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance;
    public bool isInGame = false;

    public RectTransform notificationPanel;

    public List<GameType> totalGameList = new List<GameType>();


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
        notificationPanel.gameObject.SetActive(true);
        notificationPanel.DOAnchorPosX(0, .2f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
           
        });
    }
}
