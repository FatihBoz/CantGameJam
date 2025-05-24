using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowPart : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private ArrowQualityCheck miniGame;
    [SerializeField] private ArrowPartType arrowPartType;
    private Vector3 initialPosition;
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        initialPosition = rt.anchoredPosition;
    }


    public void ResetPosition()
    {
        rt.anchoredPosition = initialPosition;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        DraggableObject draggable = GetComponent<DraggableObject>();

        switch (arrowPartType)
        {
            case ArrowPartType.HEAD:
                miniGame.SetArrowHead(draggable);
                break;
            case ArrowPartType.BODY:
                miniGame.SetArrowBody(draggable);
                break;
            case ArrowPartType.FEATHER:
                miniGame.SetArrowFeather(draggable);
                break;
        }
    }

}

public enum ArrowPartType
{
    HEAD,
    BODY,
    FEATHER
}