using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cirit : MonoBehaviour, IPointerDownHandler
{

    public static Action<Cirit> OnCiritCollected;
    [SerializeField] private CiritTypes types;
    [SerializeField] private float scaleFactor = 1.15f;
    private RectTransform targetPosAfterGrab;
    private Vector2 initialScale;
    private RectTransform rect;



    private bool isGrabbed;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        initialScale = rect.localScale;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isGrabbed)
        {
            isGrabbed = true;
            rect.localScale *= scaleFactor;
        }
        else
        {
            OnCiritCollected?.Invoke(this);
            rect.DOAnchorPos(targetPosAfterGrab.position, 1f).OnComplete(() =>
            {
                rect.localScale = initialScale;
                rect.SetParent(targetPosAfterGrab);
                rect.anchoredPosition = Vector2.zero;
                var dc = rect.AddComponent<DraggableCirit>();
                dc.SetCiritType(types);
                if (types == CiritTypes.Half)
                {
                    ++CiritMiniGame.halfCiritCount;
                }
                Destroy(GetComponent<Cirit>());
            }
            

            );

        }
    }


    public void SetPositionAfterGrab(RectTransform rt)
    {
        targetPosAfterGrab = rt;
    }

}


public enum CiritTypes
{
    None,
    Complete,
    Half,
}