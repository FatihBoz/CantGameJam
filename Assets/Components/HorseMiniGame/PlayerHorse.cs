using UnityEngine;

public class PlayerHorse: Horse
{
    [SerializeField] HorseView view;
    [SerializeField] NutritionView nutritionView;
    
    MetabolismSystem metabolismSystem;

    //HorseModel Horse.model { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    private void Start()
    {
        metabolismSystem = new MetabolismSystem(this);
        //view.UpdateStatsUI(Model);
        //view.ShowHorseShoe(Model);

        //nutritionView.UpdateView(metabolismSystem.storageModel, metabolismSystem.Energy);

    }

    /*
    public void HandleDigestion()
    {
        metabolismSystem.SelectNutrientType(RaceManager.Instance.CurrentRacePhase);
    }
    */

    public void FeedHorse(NutritionSO so)
    {
        metabolismSystem.AddNutrition(so);
    }

}


