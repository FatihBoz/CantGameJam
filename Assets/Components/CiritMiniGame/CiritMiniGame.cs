using System.Collections.Generic;
using UnityEngine;

public class CiritMiniGame : MonoBehaviour
{
    [SerializeField] private RectTransform targetPosAfterGrab;
    [SerializeField] private float maxDegreeToRotate = 60f;

    [Header("LISTS")]
    [SerializeField] private List<RectTransform> ciritPositionHolderList;
    [SerializeField] private List<GameObject> ciritPrefabs;

    private List<RectTransform> holderList;
    private List<Cirit> collectedCiritList;
    private float initialRotationZ = 0;

    private void Start()
    {
        RandomCiritGeneration();
    }


    void RandomCiritGeneration()
    {
        foreach (var holder in ciritPositionHolderList)
        {
            var obj = GetRandomCirit();
            RectTransform cirit = Instantiate(obj, Vector3.zero, Quaternion.identity).GetComponent<RectTransform>();
            cirit.transform.SetParent(holder);
            cirit.anchoredPosition = Vector3.zero;
            cirit.localRotation = Quaternion.Euler(0,0,cirit.localRotation.z + Random.Range(maxDegreeToRotate, - maxDegreeToRotate));

            if (cirit.TryGetComponent<Cirit>(out var c))
            {
                c.SetPositionAfterGrab(targetPosAfterGrab);
            }


        }
    }


    GameObject GetRandomCirit()
    {
        int r = Random.Range(0,ciritPrefabs.Count);

        return ciritPrefabs[r];
    }

    private void OnEnable()
    {
        Cirit.OnCiritCollected += OnCiritCollected;
    }

    private void OnCiritCollected(Cirit cirit)
    {
        collectedCiritList.Add(cirit);
        if (collectedCiritList.Count == ciritPositionHolderList.Count)
        {
            print("diðer sahneye geçebilirsin");
            //Button
        }
    }

    private void OnDisable()
    {
        Cirit.OnCiritCollected -= OnCiritCollected;
    }

}
