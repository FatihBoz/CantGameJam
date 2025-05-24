using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float borderThickness = 10f; // Kenar algýlama mesafesi (piksel)

    public Vector2 minPosition; // Sol alt sýnýr
    public Vector2 maxPosition; // Sað üst sýnýr

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        // Sað
        if (mousePos.x >= Screen.width - borderThickness)
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        // Sol
        else if (mousePos.x <= borderThickness)
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        // Yukarý
        if (mousePos.y >= Screen.height - borderThickness)
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        // Aþaðý
        else if (mousePos.y <= borderThickness)
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }

        // Clamp (Sýnýrlandýr)
        pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);

        transform.position = pos;
    }
}
