using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas gameCanvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameCanvas = FindFirstObjectByType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Cursor.visible = false;
        canvasGroup.blocksRaycasts = false;
        print("On Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / gameCanvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Cursor.visible = true;
        canvasGroup.blocksRaycasts = true;

    }


    //OnDrop, OnEndDrag METODUNDAN ÖNCE CAÐIRILIYOR!

}