using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] HorseView view;
    [SerializeField] MetabolismView metabolismView;

    private HorseModel model;
    private CiritBehavior ciritBehavior;

    private void Start()
    {
        model = new HorseModel(metabolismView);

    }

    public void HandleDigestion()
    {
        model.MetabolismSystem.SelectNutrientType(RaceManager.Instance.CurrentRacePhase);
    }

    public void FeedHorse(NutritionSO so)
    {
        model.MetabolismSystem.AddNutrition(so);
    }

    public void AssignHorseShoe(HorseShoeType shoeType)
    {
        model.HorseShoeType = shoeType;
    }

    public void CalculateScores()
    {
        model.AttackScore = ciritBehavior.CalculateAttackScore(model.MetabolismSystem.storageModel, model.Speed);
        model.DefenseScore = ciritBehavior.CalculateDefenseScore(model.Speed, model.Weight);
    }

    public void CalculateWeight()
    {
        model.CalculateWeight();
    }

}


