using UnityEngine;
using UnityEngine.EventSystems;

public class ResponsiveObjectToHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float zoomFactor = 1.05f;
    private Vector3 initialScale;
    private RectTransform rectTransform;
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialScale = rectTransform.localScale;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = initialScale * zoomFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = initialScale;
    }
}
