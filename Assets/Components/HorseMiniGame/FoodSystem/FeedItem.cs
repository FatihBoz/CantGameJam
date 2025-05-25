using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeedItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public NutritionSO nutritionSO;
    public event Action OnItemDropped;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        Collider2D col = Physics2D.OverlapPoint(mousePos2D);

        if (col != null)
        {
            PlayerHorse horse = col.GetComponent<PlayerHorse>();

            if (horse != null)
            {
                horse.FeedHorse(nutritionSO);
                transform.position = originalPosition;
                return;
            }
        }

        transform.position = originalPosition;

    }


}
