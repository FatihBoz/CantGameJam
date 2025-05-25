using UnityEngine;
public enum MixItemType
{
    BademYagi,
    Corekotu,
    Defne,
    Kantaron,
    Keten,
    Lavanta,
    Menekse,
    Susam,
    Zeytin,
}
public class MixItem : MonoBehaviour
{
    public GameObject itemPrefab;
    public MixItemType itemType;
    public Transform spawnedItemParent;
    private void Start()
    {
        spawnedItemParent = GameObject.Find("ITEMS").transform;
    }
    private void OnMouseDown()
    {
        if (itemPrefab != null && !FinishManager.Instance.IsFinished()) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            GameObject item = Instantiate(itemPrefab, mousePosition, Quaternion.identity);
            item.transform.parent = spawnedItemParent;
            // Olu?turulan objeye Drag bile?eni ekle
            MixDragging mixDragging = item.AddComponent<MixDragging>();
            mixDragging.itemType = this.itemType;
            mixDragging.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        }
    }

}
