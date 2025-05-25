using DG.Tweening;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CiritMiniGame : MonoBehaviour
{
    public static int halfCiritCount = 0;
    [SerializeField] private float maxDegreeToRotate = 60f;

    [Header("Next Screen")]
    [SerializeField] private RectTransform bg1;
    [SerializeField] private RectTransform bg2;
    [SerializeField] private Button nextScreenButton;
    [SerializeField] private float distanceToSlide = 500f;
    [SerializeField] private float distanceTime = 2f;

    [Header("LISTS")]
    [SerializeField] private List<RectTransform> ciritPositionHolderList;
    [SerializeField] private List<RectTransform> ciritPositionHolderListForSeperating;
    [SerializeField] private List<GameObject> ciritPrefabs;

    private List<Cirit> collectedCiritList = new();
    private Queue<RectTransform> ciritQueue;
    private float initialRotationZ = 0;
    bool sceneCanBeChanged = false;

    private void Start()
    {
        halfCiritCount =0;
        nextScreenButton.onClick.AddListener(OnNextScreenButtonPressed);
        ciritQueue = new Queue<RectTransform>(ciritPositionHolderListForSeperating);
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
                c.SetPositionAfterGrab(ciritQueue.Dequeue());
            }
        }
    }

    void OnNextScreenButtonPressed()
    {
        if (sceneCanBeChanged)
        {
            bg1.DOAnchorPosX(bg1.anchoredPosition.x - distanceToSlide, distanceTime);
            bg2.DOAnchorPosX(bg2.anchoredPosition.x - distanceToSlide, distanceTime);
            sceneCanBeChanged = false;
        }
    }



    public void DESTROY()
    {
        Destroy(gameObject);
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
            sceneCanBeChanged = true;
        }
    }

    private void OnDisable()
    {
        Cirit.OnCiritCollected -= OnCiritCollected;
    }

}
