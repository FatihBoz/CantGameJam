using TMPro;
using UnityEngine;

public class OilingManager : MonoBehaviour
{
    [Header("Grid Ayarları")]
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public float cellSize = 0.5f;
    public Vector2 gridStartPos;

    private bool[,] oilGrid;

    private int filledCount = 0;

    public SpriteRenderer secondSpriteRenderer;
    public SpriteRenderer thirdSpriteRenderer;

    public TMP_Text oilCountText;
    public TMP_Text rubCountText;

    void Awake()
    {
        oilGrid = new bool[gridSizeX, gridSizeY];
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
        oilGrid[x, y] = true;
        Vector2 cellCenter = gridStartPos + new Vector2((x + 0.5f) * cellSize, (y + 0.5f) * cellSize);
        filledCount++; // Yeni eklendi

        Color tempColor = secondSpriteRenderer.color;
        tempColor.a = Mathf.Clamp01((float)filledCount / (gridSizeX * gridSizeY));
        secondSpriteRenderer.color = tempColor; // İkinci sprite renderer'ın rengini güncelle

        oilCountText.text = "%"+(int)(tempColor.a * 100);




    }
    public void RubOil(float amount)
    {
            Debug.Log("calisiy");
            Color tempColor = thirdSpriteRenderer.color;

            tempColor.a += amount;
            tempColor.a = Mathf.Clamp(tempColor.a,0, (float)filledCount / (gridSizeX * gridSizeY));
            thirdSpriteRenderer.color = tempColor;

            rubCountText.text = "%" + (int)(thirdSpriteRenderer.color.a * 100);
    }
    private void OnEnable()
    {
            rubCountText.text = "%" + (thirdSpriteRenderer.color.a * 100);
        Color tempColor = secondSpriteRenderer.color;
        tempColor.a = Mathf.Clamp01((float)filledCount / (gridSizeX * gridSizeY));
        secondSpriteRenderer.color = tempColor; // İkinci sprite renderer'ın rengini güncelle
        oilCountText.text = "%" + (int)(tempColor.a * 100);

    }
    /// <summary>
    /// (İsteğe bağlı) Grid temizleyici
    /// </summary>
    public void ClearGrid()
    {
        oilGrid = new bool[gridSizeX, gridSizeY];
        filledCount = 0;
    }
    public bool GetIsFullfilled()
    {
        return filledCount >= gridSizeX * gridSizeY;
    }
    public float GetRubAmount()
    {
        return thirdSpriteRenderer.color.a;
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
