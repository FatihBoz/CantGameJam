using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BoxToCollectArrows : MonoBehaviour, IDropHandler
{
    public static Action OnArrowReceivecByBox;


    [SerializeField] private List<RectTransform> arrowHolders;
    private Queue<RectTransform> arrowHoldersQ;
    

    private void Start()
    {
        arrowHoldersQ = new Queue<RectTransform>(arrowHolders);
    }

    public void ReceiveArrow(GameObject arrow)
    {
        if (arrow.TryGetComponent<RectTransform>(out var rect))
        {
            rect.localRotation = Quaternion.Euler(0, 0, 0);
            RectTransform rt = arrowHoldersQ.Dequeue();
            rect.SetParent(rt);
            rect.localPosition = Vector2.zero;
            Destroy(arrow.GetComponent<DraggableObject>());
            OnArrowReceivecByBox?.Invoke();

            if (arrowHoldersQ.Count == 0)
            {
                //END OF MINI GAME
            }
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        if (obj != null)
        {
            ReceiveArrow(obj);
        }


    }


}
