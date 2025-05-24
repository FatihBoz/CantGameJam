using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shot : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private Image dirt;
    private Vector2 anchorPos;
    private RectTransform rt;
    private bool isClean;
    public Image DirtImage => dirt;
    public bool IsClean => isClean;

    public void SetAnchorPosition(Vector2 anchorPos)
    {
        this.anchorPos = anchorPos;
    }

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        anchorPos = rt.anchoredPosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        DraggableObject draggableObject = GetComponent<DraggableObject>();
        rt.DOAnchorPos(anchorPos, 1f).OnComplete(() => draggableObject.enabled = true);
        draggableObject.enabled = false;
    }

    public void CleanShot()
    {
        isClean = true;
    }
}
