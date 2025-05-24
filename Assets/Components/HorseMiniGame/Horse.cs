using UnityEngine;

public class Horse : MonoBehaviour
{
    MetabolismSystem metabolismSystem;

    public MetabolismView metabolismView;

    private float maxSpeed = 10f;
    float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        metabolismSystem = new MetabolismSystem(metabolismView);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHorse();
        MoveHorse();
    }

    public void MoveHorse()
    {
        speed = CalculateSpeedByEnergy();

        if (speed > 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            metabolismSystem.ConsumeEnergy(0.2f * Time.deltaTime);
        }
    }

    public void UpdateHorse()
    {
        metabolismSystem.SelectNutrientType(RaceManager.Instance.CurrentRacePhase);
        StartCoroutine(metabolismSystem.Digest());
        // Horse güncellemeleri burada yapýlacak
    }

    private float CalculateSpeedByEnergy()
    {
        float normalizedEnergy = Mathf.Clamp01(metabolismSystem.Energy);
        return normalizedEnergy * maxSpeed; 
    }

    public void FeedHorse(NutritionSO so)
    {
        metabolismSystem.AddNutrition(so);
    }

}
