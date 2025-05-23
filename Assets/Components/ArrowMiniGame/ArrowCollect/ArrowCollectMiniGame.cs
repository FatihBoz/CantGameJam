using System.Collections.Generic;
using UnityEngine;

public class ArrowCollectMiniGame : MonoBehaviour
{
    [SerializeField] private float arrowsMaxIntantiateDegree = 75f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private List<RectTransform> intantiateTransforms;


    private int arrowCountToFinishMiniGame = 5;

    private void Start()
    {
        Queue<RectTransform> tempQ = new(intantiateTransforms);

        for (int i = 0; i < arrowCountToFinishMiniGame; i++)
        {
            float randomDegree = Random.Range(arrowsMaxIntantiateDegree, -arrowsMaxIntantiateDegree);
            print(randomDegree);
            GameObject obj = Instantiate(arrowPrefab, tempQ.Dequeue().position, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.transform.localRotation = Quaternion.Euler(0, 0, 90 + randomDegree);
        }
    }

    public void OnArrowCollected()
    {
        arrowCountToFinishMiniGame--;
        if (arrowCountToFinishMiniGame <= 0)
        {
            // Mini Game Finished
        }
    }
}
