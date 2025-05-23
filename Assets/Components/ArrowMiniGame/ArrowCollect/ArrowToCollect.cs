using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowToCollect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private ArrowDirection arrowDirection = ArrowDirection.Up;
    private Vector3 lastPressPos;
    private int rotationNeedToGather = 3;
    private bool isDragging = false;
    private float rotateAmount = 30f;
    private float initialRotationZ;

    private ArrowCollectMiniGame miniGame;
    private Transform parentTransform;

    private void Start()
    {
        miniGame = GetComponentInParent<ArrowCollectMiniGame>();
        initialRotationZ = transform.localRotation.eulerAngles.z;
        parentTransform = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPressPos = eventData.pressPosition;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
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
                miniGame.OnArrowCollected();
                Debug.Log("Arrow gathered");
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