using UnityEngine;

public class MixDragging : MonoBehaviour
{
    private Vector3 offset;
    public bool isDragging = true;
    public MixItemType itemType;
    public bool putted;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
