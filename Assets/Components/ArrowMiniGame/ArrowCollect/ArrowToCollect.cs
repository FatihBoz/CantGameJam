using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowToCollect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Arrow Sprites")]
    [SerializeField] private Sprite initialBlueSprite;
    [SerializeField] private Sprite initialRedSprite;
    [SerializeField] private Sprite wholeRedSprite;
    [SerializeField] private Sprite wholeBlueSprite;

    private ArrowDirection arrowDirection = ArrowDirection.Up;
    private Vector3 lastPressPos;
    private int rotationNeedToGather = 7;
    private bool isDragging = false;
    private float rotateAmount = 30f;
    private float initialRotationZ;

    private ArrowCollectMiniGame miniGame;
    private Transform parentTransform;
    bool isHolding;
    int r;

    private void Start()
    {
        List<Sprite> arrowSprites = new() { initialBlueSprite, initialRedSprite};
        r = Random.Range(0, arrowSprites.Count);
        if(gameObject.TryGetComponent<Image>(out Image image))
        {
            image.sprite = arrowSprites[r];
        }


        miniGame = GetComponentInParent<ArrowCollectMiniGame>();
        initialRotationZ = transform.localRotation.eulerAngles.z;
        parentTransform = transform.parent;
        initialRotationZ = parentTransform.localRotation.eulerAngles.z;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPressPos = eventData.pressPosition;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && !isHolding)
        {
            if (Input.mousePosition.y < lastPressPos.y && arrowDirection == ArrowDirection.Up)
            {
                arrowDirection = ArrowDirection.Down;
                parentTransform.localRotation = Quaternion.Euler(0, 0, initialRotationZ - rotateAmount);
                //SPRITE CHANGE TO DOWN DIRECTION
            }
            else if (Input.mousePosition.y > lastPressPos.y && arrowDirection == ArrowDirection.Down)
            {
                arrowDirection = ArrowDirection.Up;
                parentTransform.localRotation = Quaternion.Euler(0, 0, initialRotationZ + rotateAmount);
            }
            else
            {
                return;
            }

            --rotationNeedToGather;
            if (rotationNeedToGather <= 0)
            {
                if(miniGame == null)
                {
                    print("MiniGame is null");
                }

                List<Sprite> arrowSprites = new() { wholeBlueSprite, wholeRedSprite };
                if (gameObject.TryGetComponent<Image>(out Image image))
                {
                    image.sprite = arrowSprites[r];
                }


                miniGame.OnArrowCollected();
                isHolding = true;
                transform.parent.AddComponent<DraggableObject>();
                Destroy(gameObject.GetComponent<ArrowToCollect>());
            }
            
             

        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

}


public enum ArrowDirection
{
    Up, Down
}