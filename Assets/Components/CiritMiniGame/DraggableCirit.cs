using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCirit : MonoBehaviour, IDragHandler,IBeginDragHandler, IEndDragHandler
{
    private Canvas gameCanvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPos;
    private CiritTypes type;

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
        rectTransform.anchoredPosition += eventData.delta / gameCanvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Cursor.visible = true;
        rectTransform.DOAnchorPos(initialPos, 1f).OnComplete(() => canvasGroup.blocksRaycasts = true);
    }


    public void SetCiritType(CiritTypes type)
    {
        this.type = type;
    }

    public CiritTypes CiritType => type;
}
