using UnityEngine;

public class Horse : MonoBehaviour
{
    MetabolismSystem metabolismSystem;

    public MetabolismView metabolismView;

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
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void UpdateHorse()
    {
        metabolismSystem.SelectNutrient(SetRacePhase(RaceManager.Instance.raceTime));
        StartCoroutine(metabolismSystem.Digest());
        // Horse güncellemeleri burada yapýlacak
    }

    public void FeedHorse(NutritionSO so)
    {
        metabolismSystem.AddNutrition(so);
    }

    public RacePhase SetRacePhase(float raceTime)
    {
        if (raceTime <= 20)
        {
            return RacePhase.Start;
        }
        else if (raceTime > 20 && raceTime <= 40)
        {
            return RacePhase.Race;
        }
        else
        {
            return RacePhase.Finish;

        }
    }
}
