using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CleanShotSpot : MonoBehaviour, IDropHandler
{
    public static Action<Shot> OnShotPlaced;

    [SerializeField] private List<RectTransform> holders;
    private Queue<RectTransform> q;

    public void OnDrop(PointerEventData eventData)
    {
        if (q.Count <= 0)
        {
            return;
        }


        GameObject obj = eventData.pointerDrag;

        if (obj.TryGetComponent<Shot>(out var shot) && shot.TryGetComponent<RectTransform>(out var rt))
        {
            RectTransform rect = q.Dequeue();
            q.Enqueue(rt);
            rt.anchoredPosition = rect.anchoredPosition;
            shot.SetAnchorPosition(rect.anchoredPosition);
            OnShotPlaced?.Invoke(shot);
            Destroy(rt.GetComponent<DraggableObject>());
            Cursor.visible = true;
        }
    }

    private void Start()
    {
        q = new Queue<RectTransform>(holders);
    }

    

}
