using UnityEngine;

public class OilingManager : MonoBehaviour
{
    [Header("Grid Ayarları")]
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public float cellSize = 0.5f;
    public Vector2 gridStartPos;

    [Header("Görsel Ayarlar")]
    public GameObject shinyPrefab;

    private GameObject[,] oilGrid;

    private int filledCount = 0;


    void Awake()
    {
        oilGrid = new GameObject[gridSizeX, gridSizeY];
    }

    /// <summary>
    /// Bu fonksiyonu damla yere düştüğünde çağır.
    /// </summary>
    public void DropOilAt(Vector2 worldPos)
    {
        // World konumunu grid koordinatına çevir
        Vector2 localPos = worldPos - gridStartPos;
        int x = Mathf.FloorToInt(localPos.x / cellSize);
        int y = Mathf.FloorToInt(localPos.y / cellSize);

        // Grid sınırlarının dışındaysa işlem yapma
        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY)
            return;

        // Zaten yağ dökülmüşse tekrar yapma
        if (oilGrid[x, y])
            return;


        CreateShinyEffect(x, y);
    }

    private void CreateShinyEffect(int x, int y)
    {
        Vector2 cellCenter = gridStartPos + new Vector2((x + 0.5f) * cellSize, (y + 0.5f) * cellSize);
        oilGrid[x, y]=Instantiate(shinyPrefab, cellCenter, Quaternion.identity);
        filledCount++; // Yeni eklendi
    }

    /// <summary>
    /// (İsteğe bağlı) Grid temizleyici
    /// </summary>
    public void ClearGrid()
    {
        oilGrid = new GameObject[gridSizeX, gridSizeY];
        filledCount = 0;
    }
    public bool GetIsFullfilled()
    {
        return filledCount >= gridSizeX * gridSizeY;
    }
    public void DecreaseOilTransparency(float amount)
    {
        // Yağlı hücrelerin saydamlığını azalt
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (oilGrid[x, y])
                {
                    Color oilGridColor= oilGrid[x, y].GetComponent<SpriteRenderer>().color;
                    oilGridColor.a -= amount;
                    oilGridColor.a = Mathf.Clamp(oilGridColor.a, 0f, 1f); // 0 ile 1 arasında sınırla
                    oilGrid[x, y].GetComponent<SpriteRenderer>().color=oilGridColor;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 cellCenter = gridStartPos + new Vector2((x + 0.5f) * cellSize, (y + 0.5f) * cellSize);
                Vector2 cellSizeVec = new Vector2(cellSize, cellSize);

                // Hücre çerçevesini çiz
                Gizmos.DrawWireCube(cellCenter, cellSizeVec);
            }
        }

        // Yağlı hücreleri mavi ile doldur
        if (oilGrid != null)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (oilGrid[x, y])
                    {
                        Vector2 cellCenter = gridStartPos + new Vector2((x + 0.5f) * cellSize, (y + 0.5f) * cellSize);
                        Gizmos.color = new Color(0f, 0.5f, 1f, 0.25f); // yarı saydam mavi
                        Gizmos.DrawCube(cellCenter, new Vector3(cellSize * 0.95f, cellSize * 0.95f, 0f));
                    }
                }
            }
        }
    }
}
