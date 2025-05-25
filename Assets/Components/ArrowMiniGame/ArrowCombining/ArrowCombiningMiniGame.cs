using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ArrowCombiningMiniGame : MonoBehaviour
{
    [SerializeField] private int arrowCountToCraft = 3;
    [SerializeField] private List<GameObject> greenDots;
    [SerializeField] private GameObject tick;
    private int currentArrowCount = 0;

    public void OnArrowCrafted()
    {
        greenDots[currentArrowCount].SetActive(true);
        currentArrowCount++;
        if (currentArrowCount >= arrowCountToCraft)
        {
            tick.SetActive(true);
            return;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
