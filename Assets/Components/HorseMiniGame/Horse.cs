using UnityEngine;

public class Horse : MonoBehaviour
{
    MetabolismSystem metabolismSystem;

    public MetabolismView metabolismView;

    float speed;

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
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void UpdateHorse()
    {
        metabolismSystem.Digest(SetRacePhase(RaceManager.Instance.raceTime));
        // Horse güncellemeleri burada yapýlacak
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
