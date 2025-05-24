using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tissue : MonoBehaviour, IBeginDragHandler, IEndDragHandler,IDragHandler
{
    [SerializeField] private float fadeAmountEveryFrame = 0.002f;
    [SerializeField] private string Tag;
    private Canvas gameCanvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 lastPosition;
    private float alpha = 1;
    private Image currentImage;
    private Shot currentShot;
    private Vector2 initialPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameCanvas = FindFirstObjectByType<Canvas>();
        initialPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Cursor.visible = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        lastPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition += eventData.delta / gameCanvas.scaleFactor;


        if (eventData.pointerEnter.layer != 11)
            return;


        if (alpha > 0)
        {
            print(eventData.pointerEnter.name);
            float distance = Vector2.Distance(lastPosition, rectTransform.anchoredPosition);
            alpha -= fadeAmountEveryFrame;
            currentImage.color = new Color(1, 1, 1, alpha);
        }
        else if (alpha <= 0.1)
        {
            currentShot.CleanShot();
        }
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Cursor.visible = true;
        canvasGroup.blocksRaycasts = true;
        rectTransform.DOAnchorPos(initialPos, 1f);
    }

    private void OnEnable()
    {
        ShotCleaningSpot.OnShotPlaced += OnShotPlaced;
    }

    private void OnDisable()
    {
        ShotCleaningSpot.OnShotPlaced -= OnShotPlaced;
    }

    private void OnShotPlaced(Shot shot)
    {
        currentShot = shot;
        currentImage = shot.DirtImage;
        alpha = 1;
    }
}
