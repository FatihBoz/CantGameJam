using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShotCleaningSpot : MonoBehaviour, IDropHandler
{
    public static Action<Shot> OnShotPlaced; 

    private RectTransform cleaningPoint;

    private bool shotCanBeCleaned = false;


    private void Awake()
    {
        cleaningPoint = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("girdi");
        GameObject obj = eventData.pointerDrag;

        if (obj.TryGetComponent<Shot>(out var shot) && obj.TryGetComponent<RectTransform>(out var rect))
        {
            OnShotPlaced?.Invoke(shot);
            rect.anchoredPosition = cleaningPoint.anchoredPosition;
            shot.SetAnchorPosition(cleaningPoint.anchoredPosition);
            shotCanBeCleaned = true;
        }
    }
}
