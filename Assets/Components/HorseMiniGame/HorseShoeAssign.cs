using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HorseShoeAssign : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public HorseShoeType horseShoeType;
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

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        Debug.DrawRay(mousePos2D, Vector2.up * 0.1f, Color.red, 1f);

        if (hit.collider != null)
        {
            Horse horse = hit.collider.GetComponent<Horse>();

            if (horse != null)
            {
                horse.AssignHorseShoe(horseShoeType);
                Destroy(gameObject); // Yem t�ketildi
                return;
            }
        }
        transform.position = originalPosition;
    }
}

public enum HorseShoeType
{
    Plain,
    Spiked
}
