using UnityEngine;

public class MouseQTE : MonoBehaviour
{
    public OilingManager oilManager;
    public float minSpeed = 0.5f;      // Minimum cycle süresi (sn)
    public float maxSpeed = 1.2f;      // Maksimum cycle süresi (sn)
    public float requiredCycles = 5f;  // Kaç doğru cycle yapılmalı

    private Vector2 lastMousePos;
    private bool isDragging = false;

    private float direction = 0; // -1 geri, 1 ileri
    private float lastDirection = 0;
    private float lastCycleTime = 0;

    void Update()
    {
        if (!oilManager.GetIsFullfilled())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePos = Input.mousePosition;
            direction = 0;
            lastDirection = 0;
            lastCycleTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 currentMousePos = Input.mousePosition;
            float delta = currentMousePos.y - lastMousePos.y; // yukarı-aşağı hareket
            lastMousePos = currentMousePos;

            if (Mathf.Abs(delta) > 5f) // küçük hareketleri görmezden gel
            {
                direction = Mathf.Sign(delta);

                if (direction != lastDirection && lastDirection != 0)
                {
                    float cycleTime = Time.time - lastCycleTime;

                    if (cycleTime >= minSpeed && cycleTime <= maxSpeed)
                    {
                        Debug.Log("✔️ Doğru tempoda gel-git!");
                        oilManager.DecreaseOilTransparency(0.02f);

                    }
                    lastCycleTime = Time.time;
                }

                lastDirection = direction;
            }
        }
    }
}
