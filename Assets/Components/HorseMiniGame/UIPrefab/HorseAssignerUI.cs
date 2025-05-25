using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseAssignerUI : MonoBehaviour
{
    [Header("HorseView")]
    [SerializeField] private HorseView horseView;

    [Header("MetabolismView")]
    [SerializeField] private NutritionView nutritionView;

    [Header("UI Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button changeUIButton;

    [Header("Horse Parent")]
    [SerializeField] private Transform horseParent;

    private int currentIndex = 0;
    private List<GameObject> horseList = new List<GameObject>();

    void Start()
    {
        if (horseView == null || nutritionView == null || horseParent == null)
        {
            Debug.LogError("Eksik referanslar: horseView, nutritionView veya horseParent atanmadý!");
            return;
        }

        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
        changeUIButton.onClick.AddListener(ChangeUIView);

        TeamManager.Instance.AssignPlayerTeamHorses(horseParent);
        horseList = TeamManager.Instance.ReturnPlayerTeamList();
        // Parent altýndaki PlayerHorse'larý listeye al
        //horseList = new List<Horse>(horseParent.GetComponentsInChildren<Horse>());

        print($"Toplam {horseList.Count} adet PlayerHorse bulundu.");

        if (horseList.Count == 0)
        {
            Debug.LogWarning("horseParent altýnda hiç PlayerHorse bulunamadý!");
            return;
        }

        UpdateHorseView();
    }

    public void ShowHorseDetails(HorseModel model)
    {
        horseView.UpdateStatsUI(model);
        nutritionView.UpdateView(model.NutrientStorage, model.Stamina); // enerji yoksa þimdilik sadece storage gönder

        model.OnHorseDataChanged += () =>
        {
            horseView.UpdateStatsUI(model);
            nutritionView.UpdateView(model.NutrientStorage, model.Stamina);
        };
    }

    public void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = horseList.Count - 1;
        }
        UpdateHorseView();
    }

    public void MoveRight()
    {
        currentIndex++;
        if (currentIndex >= horseList.Count)
        {
            currentIndex = 0;
        }
        UpdateHorseView();
    }

    private void UpdateHorseView()
    {
        Debug.Log("currentIndex: " + currentIndex);

        GameObject currentHorse = horseList[currentIndex];
        Horse currentHorseComp = currentHorse.GetComponent<Horse>();
        ShowHorseDetails(currentHorseComp.Model);

        for (int i = 0; i < horseParent.childCount; i++)
        {
            horseParent.GetChild(i).gameObject.SetActive(i == currentIndex);
        }
    }

    public void ChangeUIView()
    {
        horseView.gameObject.SetActive(!horseView.gameObject.activeSelf);
        nutritionView.gameObject.SetActive(!nutritionView.gameObject.activeSelf);

        UpdateHorseStats();
    }

    private void UpdateHorseStats()
    {
        GameObject currentHorse = horseList[currentIndex];
        Horse currentHorseComp = currentHorse.GetComponent<Horse>();

        currentHorseComp.CalculateScores(); // skorlarý hesapla
        horseView.UpdateStatsUI(currentHorseComp.Model); // UI'ý güncelle
    }
}
