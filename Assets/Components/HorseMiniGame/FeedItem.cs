using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeedItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public NutritionSO nutritionSO;
    public event Action OnItemDropped;

    private Vector3 originalPosition;
    private Canvas canvas; // UI i�in s�r�kleme yaparken gerekebilir

    void Start()
    {
        originalPosition = transform.position;
        canvas = GetComponentInParent<Canvas>();
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
        print("OnEndDrag called");
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        print("Mouse Position: " + mousePos2D);

        Debug.DrawRay(mousePos2D, Vector2.up * 0.1f, Color.red, 1f);


        if (hit.collider != null)
        {
            Horse horse = hit.collider.GetComponent<Horse>();
            Debug.Log("Hit: " + hit.collider.name);

            if (horse != null)
            {
                horse.FeedHorse(nutritionSO);
                Destroy(gameObject); // Yem t�ketildi
                return;
            }
        }

        // Uygun yere b�rak�lmad�ysa geri d�n
        transform.position = originalPosition;
    }


}
