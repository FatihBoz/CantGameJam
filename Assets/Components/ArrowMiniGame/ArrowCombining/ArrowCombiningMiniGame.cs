using UnityEngine;

public class ArrowCombiningMiniGame : MonoBehaviour
{
    [SerializeField] private int arrowCountToCraft = 3;
    private int currentArrowCount = 0;

    public void OnArrowCrafted()
    {
        currentArrowCount++;
        if (currentArrowCount >= arrowCountToCraft)
        {
            //Mini Game Bitti
            return;
        }
    }
}
