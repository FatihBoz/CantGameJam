using DG.Tweening;
using UnityEngine;

public class DraggablePour : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 startPos;
    private Vector3 startRotation;
    private Camera cam;
    [SerializeField] private ParticleSystem pourEffect;

    public float oilAmount = 100f;
    void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
        startRotation = transform.eulerAngles;
        pourEffect.Stop();

    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0)) // Sol tıkla drag başlar
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
            isDragging = true;
            transform.DORotate(new Vector3(0, 0, -135), 0.5f);
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            transform.position = startPos; // Eski pozisyona dön
            transform.eulerAngles = startRotation;
            pourEffect.Stop();

        }
    }


    void Update()
    {
        if (isDragging)
        {
            // Sürüklemeyi takip et
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
            Pour();
        }
    }

    void Pour()
    {
        if (oilAmount>0)
        {
           
            oilAmount -= Time.deltaTime*10; // Her tıklamada yağ miktarını azalt
            Debug.Log($"{gameObject.name} is POURING!");
            pourEffect.Play();

        }
        else
        {
            pourEffect.Stop();
        }
    }
}
