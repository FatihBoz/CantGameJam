using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float borderThickness = 10f; // Kenar alg�lama mesafesi (piksel)

    public Vector2 minPosition; // Sol alt s�n�r
    public Vector2 maxPosition; // Sa� �st s�n�r

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        // Sa�
        if (mousePos.x >= Screen.width - borderThickness)
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        // Sol
        else if (mousePos.x <= borderThickness)
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        // Yukar�
        if (mousePos.y >= Screen.height - borderThickness)
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        // A�a��
        else if (mousePos.y <= borderThickness)
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }

        // Clamp (S�n�rland�r)
        pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);

        transform.position = pos;
    }
}
