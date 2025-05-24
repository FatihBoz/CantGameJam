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
    private DraggableObject arrowHead;
    private DraggableObject arrowBody;
    private DraggableObject arrowFeather;

    [Header("Arrow Parts Values")]
    [SerializeField] private float distanceBetweenArrowParts = 250f;

    [Header("Arrow Feather Sprites")]
    [SerializeField] private List<Sprite> arrowFeatherSprites;

    [Header("Closer Panels")]
    [SerializeField] private GameObject arrowHeadPanel;
    [SerializeField] private GameObject arrowBodyPanel;
    [SerializeField] private GameObject arrowFeatherPanel;


    private ArrowCombiningMiniGame miniGame;

    private void Awake()
    {
        miniGame = GetComponent<ArrowCombiningMiniGame>();
        
    }

    private void Start()
    {
        qualityCheckButton.onClick.AddListener(CheckForQuality);
        resetButton.onClick.AddListener(ResetArrowParts);

        //ResetArrowParts();
    }

    Queue<DraggableObject> GatherArrowParts()
    {

        Queue<DraggableObject> draggableObjects = new();
        
        draggableObjects.Enqueue(arrowHead);
        draggableObjects.Enqueue(arrowBody);
        draggableObjects.Enqueue(arrowFeather);

        return draggableObjects;
    }


    bool CheckForQualityXAxis()
    {
        Queue<DraggableObject> tempQ = GatherArrowParts();
        int tempCount = tempQ.Count;
        print(tempCount);
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
        if (!(arrowHead.transform.localPosition.y > arrowBody.transform.localPosition.y + comparisonLength) ||
            !(arrowBody.transform.localPosition.y > arrowFeather.transform.localPosition.y + comparisonLength))
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
        List<float> positionList = new() { 0, distanceBetweenArrowParts, -distanceBetweenArrowParts };

        Queue<DraggableObject> tempQ = GatherArrowParts();

        float tempCount = tempQ.Count;

        for(int i = 0; i < tempCount; ++i)
        {
            if (tempQ.Peek() != null &&tempQ.Dequeue().TryGetComponent<ArrowPart>(out var arrow))
            {
                arrow.ResetPosition();
            }
        }

        arrowHeadPanel.SetActive(false);
        arrowBodyPanel.SetActive(false);
        arrowFeatherPanel.SetActive(false);
    }


    public void SetArrowHead(DraggableObject arrowHead)
    {
        this.arrowHead = arrowHead;
        arrowHeadPanel.SetActive(true);
    }

    public void SetArrowBody(DraggableObject arrowBody)
    {
        this.arrowBody = arrowBody;
        arrowBodyPanel.SetActive(true);
    }

    public void SetArrowFeather(DraggableObject arrowFeather)
    {
        this.arrowFeather = arrowFeather;
        arrowFeatherPanel.SetActive(true);

    }


}
