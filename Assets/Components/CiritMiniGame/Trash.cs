using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trash : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button finishButton;

    private readonly string animationName = "CapOpen";

    private bool canDrop = false;
    private int halfCiritCount = 0;
    private bool miniGameCanBeFinished = false;

    private void Start()
    {
        finishButton.onClick.AddListener(OnGameFinishButtonPressed);
    }

    void OnGameFinishButtonPressed()
    {
        print("bitti");
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        if (obj.TryGetComponent<DraggableCirit>(out var d) && canDrop && d.CiritType == CiritTypes.Half)
        {
            ++halfCiritCount;
            Destroy(obj);
            Cursor.visible = true;

            if (halfCiritCount == CiritMiniGame.halfCiritCount)
            {
                miniGameCanBeFinished = true;
                finishButton.gameObject.SetActive(true);    


            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canDrop = true;
        _animator.SetBool(animationName, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canDrop = false;
        _animator.SetBool(animationName, false);
    }
}
