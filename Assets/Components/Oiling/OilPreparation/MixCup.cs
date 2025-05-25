using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MixCup : MonoBehaviour
{

    public TextMeshProUGUI recipeText;
    public TextMeshProUGUI addedItemsText;

    public Transform spawnedItemsParent;


    public Dictionary<MixItemType, int> addedMixItems = new Dictionary<MixItemType, int>();
    public Dictionary<MixItemType, int> recipe = new Dictionary<MixItemType, int>();

    private void Start()
    {
        GenerateRandomRecipe(3);
        UpdateAllUIText();
        spawnedItemsParent = GameObject.Find("ITEMS").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MixDragging"))
        {
            MixDragging mixDragging = collision.gameObject.GetComponent<MixDragging>();
            if (mixDragging != null && !mixDragging.putted)
            {
                mixDragging.isDragging = false; // Dragging'i durdur
                mixDragging.putted = true;
                if (!addedMixItems.ContainsKey(mixDragging.itemType))
                    addedMixItems[mixDragging.itemType] = 0;

                addedMixItems[mixDragging.itemType]++;
                UpdateAllUIText();
            }
        }
    }
    public void UpdateAllUIText()
    {
        UpdateRecipeTextUI();
        UpdateAddedItemsTextUI();
    }
    public bool IsRecipeMatched()
    {
        foreach (var kvp in recipe)
        {
            if (!addedMixItems.ContainsKey(kvp.Key) || addedMixItems[kvp.Key] != kvp.Value)
            {
                return false;
            }
        }

        foreach (var kvp in addedMixItems)
        {
            if (!recipe.ContainsKey(kvp.Key))
            {
                return false;
            }
        }

        return true;
    }
    public void UpdateRecipeTextUI()
    {
        if (recipeText == null) return;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var kvp in recipe)
        {
            sb.AppendLine($"{kvp.Key} x{kvp.Value}");
        }
        recipeText.text = sb.ToString();
    }

    public void UpdateAddedItemsTextUI()
    {
        if (addedItemsText == null) return;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var kvp in addedMixItems)
        {
            MixItemType itemType = kvp.Key;
            int addedAmount = kvp.Value;

            if (!recipe.ContainsKey(itemType))
            {
                // Tarif dışı → tamamen kırmızı
                sb.AppendLine($"<color=red>{itemType} x{addedAmount}</color>");
            }
            else
            {
                int requiredAmount = recipe[itemType];

                if (addedAmount == requiredAmount)
                {
                    // Tam doğru → tamamen yeşil
                    sb.AppendLine($"<color=green>{itemType} x{addedAmount}</color>");
                }
                else
                {
                    // İsim doğru ama miktar yanlış → isim yeşil, sayı kırmızı
                    sb.AppendLine($"<color=green>{itemType}</color> <color=red>x{addedAmount}</color>");
                }
            }
        }

        addedItemsText.text = sb.ToString();
    }


    public string GetRecipeText()
    {
        if (recipe.Count == 0) return "Tarif yok.";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var kvp in recipe)
        {
            sb.AppendLine($"{kvp.Key} x{kvp.Value}");
        }
        return sb.ToString();
    }



    public void GenerateRandomRecipe(int ingredientCount)
    {
        recipe.Clear(); // Reset
        List<MixItemType> allItems = new List<MixItemType>((MixItemType[])System.Enum.GetValues(typeof(MixItemType)));

        for (int i = 0; i < ingredientCount && allItems.Count > 0; i++)
        {
            int index = Random.Range(0, allItems.Count);
            MixItemType selected = allItems[index];
            allItems.RemoveAt(index);

            recipe[selected] = Random.Range(1, 4); 
        }
    }

    public void ResetAddedItems()
    {
        if (FinishManager.Instance.IsFinished())
        {
            return;
        }

        addedMixItems.Clear();
        GenerateRandomRecipe(3);
        foreach (Transform child in spawnedItemsParent)
        {
            Destroy(child.gameObject);
        }
        UpdateAllUIText();
    }

    public void ResetRecipe()
    {
        recipe.Clear();
        UpdateAllUIText();
    }
    public void CheckSend()
    {
        if (IsRecipeMatched()&& !FinishManager.Instance.IsFinished())
        {
            Debug.Log("Tarif eşleşti! Gönderiliyor...");
            FinishManager.Instance.FinishTrue();
        }
        else
        {
            Debug.Log("Tarif eşleşmedi! Lütfen tekrar deneyin.");
            ResetAddedItems();
        }
    }

}
