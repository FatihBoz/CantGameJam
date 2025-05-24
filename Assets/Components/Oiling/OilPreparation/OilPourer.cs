using UnityEngine;

public class OilPourer : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Down on Oil Pourer");
        }
    }
}
