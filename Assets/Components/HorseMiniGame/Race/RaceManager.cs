using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    [Header("Race Configuration")]
    [SerializeField] private float minRaceTime = 40f;
    [SerializeField] private float maxRaceTime = 80f;

    private RaceSettings currentRace;
    public RaceSettings CurrentRace => currentRace;

    private float raceTime;
    public float RaceTime => raceTime;

    private RacePhase currentRacePhase;
    public RacePhase CurrentRacePhase => currentRacePhase;

    private bool raceRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!raceRunning) return;

        raceTime += Time.deltaTime;

        RacePhase phaseByTime = SetRacePhase(raceTime);
        if (phaseByTime != currentRacePhase)
        {
            currentRacePhase = phaseByTime;
            Debug.Log($"Phase changed to: {currentRacePhase}");
        }

        if (raceTime >= currentRace.raceDuration)
        {
            EndRace();
        }
    }

    public void StartRace()
    {
        GenerateRandomRace();

        raceTime = 0f;
        raceRunning = true;
        currentRacePhase = SetRacePhase(0);
        Debug.Log($"Race started. Track: {currentRace.trackType}, Duration: {currentRace.raceDuration} seconds");
    }

    private void GenerateRandomRace()
    {
        currentRace = new RaceSettings
        {
            trackType = (TrackType)Random.Range(0, System.Enum.GetValues(typeof(TrackType)).Length),
            raceDuration = Random.Range(minRaceTime, maxRaceTime)
        };
    }

    private void EndRace()
    {
        raceRunning = false;
        Debug.Log("Race ended.");
    }

    public RacePhase SetRacePhase(float raceTime)
    {
        if (raceTime <= 20f)
        {
            return RacePhase.Start;
        }
        else if (raceTime > 20f && raceTime <= 40f)
        {
            return RacePhase.Race;
        }
        else
        {
            return RacePhase.Finish;
        }
    }
}
public enum RacePhase
{
    Start,
    Race,
    Finish
}
