using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ArrowCombiningMiniGame))]
public class ArrowQualityCheck : MonoBehaviour
{
    [SerializeField] private Transform wholeArrow;
    [SerializeField] private float errorMargin = 3f;

    [SerializeField] private float comparisonLength = 50f;

    [Header("Buttons")]
    [SerializeField] private Button qualityCheckButton;
    [SerializeField] private Button resetButton;

    [Header("Arrow Parts")]
    [SerializeField] private DraggableObject ArrowHead;
    [SerializeField] private DraggableObject ArrowBody;
    [SerializeField] private DraggableObject ArrowFeather;

    [Header("Arrow Parts Values")]
    [SerializeField] private float distanceBetweenArrowParts = 250f;

    [Header("Arrow Feather Sprites")]
    [SerializeField] private List<Sprite> arrowFeatherSprites;

    private ArrowCombiningMiniGame miniGame;

    private void Awake()
    {
        miniGame = GetComponent<ArrowCombiningMiniGame>();
        
    }

    private void Start()
    {
        qualityCheckButton.onClick.AddListener(CheckForQuality);
        resetButton.onClick.AddListener(ResetArrowParts);

        ResetArrowParts();
    }

    Queue<DraggableObject> GatherArrowParts()
    {

        Queue<DraggableObject> draggableObjects = new();
        
        draggableObjects.Enqueue(ArrowHead);
        draggableObjects.Enqueue(ArrowBody);
        draggableObjects.Enqueue(ArrowFeather);

        return draggableObjects;
    }


    bool CheckForQualityXAxis()
    {
        Queue<DraggableObject> tempQ = GatherArrowParts();
        int tempCount = tempQ.Count;
        for (int i = 0; i < tempCount; ++i)
        {
            DraggableObject first = tempQ.Dequeue();
            tempQ.Enqueue(first);

            DraggableObject second = tempQ.Dequeue();
            tempQ.Enqueue(second);

            if (!(Mathf.Abs(first.transform.localPosition.x - second.transform.localPosition.x) < errorMargin))
            {
                print(" Quality Check FailedX");
                print(first.transform.localPosition.x + " " + second.transform.localPosition.x);
                return false;
            }
        }

        return true;


    }

    bool CheckForQualityYAxis()
    {
        if (!(ArrowHead.transform.localPosition.y > ArrowBody.transform.localPosition.y + comparisonLength) ||
            !(ArrowBody.transform.localPosition.y > ArrowFeather.transform.localPosition.y + comparisonLength))
        {
            print("Quality Check FailedY");
            return false;
        }
        else
        {
            print("Quality Check PassedY");
            
        }

        return true;
    }



    void CheckForQuality()
    {
        if (CheckForQualityXAxis() && CheckForQualityYAxis())
        {
            miniGame.OnArrowCrafted();
            
        }

        ResetArrowParts();
    }



    void ResetArrowParts()
    {
        int r = Random.Range(0, arrowFeatherSprites.Count);
        ArrowFeather.GetComponent<Image>().sprite = arrowFeatherSprites[r];



        List<float> positionList = new List<float>() { 0, distanceBetweenArrowParts, -distanceBetweenArrowParts };

        Queue<DraggableObject> tempQ = GatherArrowParts();

        float tempCount = tempQ.Count;

        for(int i = 0; i < tempCount; ++i)
        {
            int randomIndex = Random.Range(0, positionList.Count);
            tempQ.Dequeue().transform.localPosition = new Vector3(positionList[randomIndex], 0, 0);
            positionList.RemoveAt(randomIndex);
        }
    }



}
