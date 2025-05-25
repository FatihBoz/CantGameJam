using System.Collections.Generic;
using UnityEngine;

public class ShotCleanMiniGame : MonoBehaviour
{
    [SerializeField] private int shotCountToFinish = 3;
    [SerializeField] private GameObject tick;
    int currShot = 0;
    List<Shot> currentShots = new();

    private void OnEnable()
    {
        CleanShotSpot.OnShotPlaced += OnShotPlace;
    }

    private void OnShotPlace(Shot s)
    {
        currentShots.Add(s);
        ++currShot;
        if (currShot >= shotCountToFinish)
        {
            if (AllShotsAreClean())
            {
                tick.SetActive(true);
                print("level completed");
                //LEVEL COMPLETED
            }
        }
    }


    bool AllShotsAreClean()
    {
        foreach (Shot s in currentShots)
        {
            print(s.name);
            if (!s.IsClean)
            {
                print(s.name + " is not clean");
                return false;
            }
        }
        return true;
    }

    private void OnDisable()
    {

        CleanShotSpot.OnShotPlaced -= OnShotPlace;
    }

    public void DESTROY()
    {
        Destroy(gameObject);
    }

}
