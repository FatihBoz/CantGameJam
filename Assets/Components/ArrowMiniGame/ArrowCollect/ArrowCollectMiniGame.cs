using System.Collections.Generic;
using UnityEngine;

public class ArrowCollectMiniGame : MonoBehaviour
{
    [SerializeField] private float arrowsMaxIntantiateDegree = 75f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private List<RectTransform> intantiateTransforms;
    [SerializeField] private int arrowCountToFinishMiniGame = 5;


    [Header("Middle Point")]
    [SerializeField] private RectTransform middlePoint;
    [SerializeField] private float reachTimeToMiddlePoint;

    private bool arrowsCanBeCollected = true;


    public RectTransform MiddlePoint { get => middlePoint;}
    public float ReachTimeToMiddlePoint { get => reachTimeToMiddlePoint; }
    public bool ArrowsCanBeCollected { get => arrowsCanBeCollected;}



    private void Start()
    {
        Queue<RectTransform> tempQ = new(intantiateTransforms);

        for (int i = 0; i < arrowCountToFinishMiniGame; i++)
        {
            float randomDegree = Random.Range(arrowsMaxIntantiateDegree, -arrowsMaxIntantiateDegree);
            GameObject obj = Instantiate(arrowPrefab, tempQ.Dequeue().position, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.transform.localRotation = Quaternion.Euler(0, 0, 90 + randomDegree);
        }
    }

    public void OnArrowCollected(ArrowToCollect arrow)
    {
        arrowCountToFinishMiniGame--;
        arrowsCanBeCollected = false;

        if (arrowCountToFinishMiniGame <= 0)
        {
            
            // Mini Game Finished
        }
    }


    private void OnEnable()
    {
        BoxToCollectArrows.OnArrowReceivecByBox += OnArrowCollected;
    }

    private void OnArrowCollected()
    {
        arrowsCanBeCollected = true;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        BoxToCollectArrows.OnArrowReceivecByBox -= OnArrowCollected;
    }


}
