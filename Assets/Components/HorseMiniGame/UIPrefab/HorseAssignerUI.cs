using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseAssignerUI : MonoBehaviour
{
    [Header("HorseView")]
    public GameObject HorseView;

    [Header("HorseObject")]
    public GameObject HorseObject;

    [Header("UI Buttons")]
    public Button leftButton;
    public Button rightButton;

    private List<Transform> children1 = new List<Transform>();
    private List<Transform> children2 = new List<Transform>();
    private int currentIndex = 0;

    void Start()
    {
        if (HorseView == null || HorseObject == null)
        {
            Debug.LogError("Parent objeler eksik!");
            return;
        }

        InitializeChildren(HorseView, children1);
        InitializeChildren(HorseObject, children2);

        if (children1.Count != children2.Count)
        {
            Debug.LogError("Ýki parent'ýn çocuk sayýsý eþit olmalý!");
            return;
        }

        SetActiveAtIndex(currentIndex);

        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
    }

    void InitializeChildren(GameObject parent, List<Transform> list)
    {
        foreach (Transform child in parent.transform)
        {
            list.Add(child);
            child.gameObject.SetActive(false);
        }
    }

    void SetActiveAtIndex(int index)
    {
        for (int i = 0; i < children1.Count; i++)
        {
            children1[i].gameObject.SetActive(i == index);
            children2[i].gameObject.SetActive(i == index);
        }
    }

    void MoveRight()
    {
        currentIndex = (currentIndex + 1) % children1.Count;
        SetActiveAtIndex(currentIndex);
    }

    void MoveLeft()
    {
        currentIndex = (currentIndex - 1 + children1.Count) % children1.Count;
        SetActiveAtIndex(currentIndex);
    }

}
