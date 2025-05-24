using UnityEngine;

public class HandPositioner : MonoBehaviour
{
    public float verticalFollowMultiplier = 0.05f; // deltaY hareket çarpanı


    public float minPosY;
    public float maxPosY;

    public float shouldMinus;
    public bool handing = false;
    public Vector3 firstPos;


    public OilingManager oilManager;

    public float minSpeed = 0.5f;      // Minimum cycle süresi (sn)
    public float maxSpeed = 1.2f;      // Maksimum cycle süresi (sn)
    public float requiredCycles = 5f;  // Kaç doğru cycle yapılmalı

    private Vector2 lastMousePos;

    private float direction = 0; // -1 geri, 1 ileri
    private float lastDirection = 0;
    private float lastCycleTime = 0;

    private bool halfCycleStarted = false;
    private float halfCycleStartTime = 0;

    private float lastMouseY;
    private float lastMoveTime;

    void Start()
    {
        firstPos = transform.position;
        lastMouseY = Input.mousePosition.y;
        lastMoveTime = Time.time;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0)) // Sol tıkla drag başlar
        {
            handing = true;
            lastMousePos = Input.mousePosition;
            direction = 0;
            lastDirection = 0;
            lastCycleTime = Time.time;

        }
        else if(Input.GetMouseButton(2))
        {
            handing = false;
            transform.position = firstPos;
        }
    }
    void Update()
    {
        if (!oilManager.GetIsFullfilled())
            return;

        if (handing)
        {
            Movement();

            float currentMouseY = Input.mousePosition.y;
            float deltaY = currentMouseY - lastMouseY;
            float deltaTime = Time.time - lastMoveTime;

            if (Mathf.Abs(deltaY) > 5f && deltaTime > 0)
            {
                float verticalSpeed = Mathf.Abs(deltaY) / deltaTime;

                // örnek: verticalSpeed ~ 100 ile 600 arasında olmalı (piksel/saniye)
                if (verticalSpeed >= minSpeed && verticalSpeed <= maxSpeed)
                {
                    Debug.Log($"✔️ Doğru tempoda dikey hareket! Hız: {verticalSpeed:F1}");
                    oilManager.RubOil(0.02f);
                }

                lastMouseY = currentMouseY;
                lastMoveTime = Time.time;
            }
        }

        // Resetleme tuşları
        if (Input.GetMouseButtonDown(1))
        {
            handing = false;
            transform.position = firstPos;
        }
    }

    public void Movement()
    {
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePos.z = 0f;

        // ROTASYON: sadece Z rotasyonunu mouse yönüne göre ayarla
        Vector3 direction = currentMousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        Vector3 tempPosition = transform.position;
        tempPosition = new Vector3(0f, currentMousePos.y - shouldMinus, 0f);
        tempPosition.y = Mathf.Clamp(tempPosition.y, minPosY, maxPosY);
        transform.position = tempPosition;
    }
}
