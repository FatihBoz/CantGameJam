using UnityEngine;

public class FinishManager : MonoBehaviour
{
    private bool isFinished = false;
    public static FinishManager Instance;

    public GameObject truePanel;
    private void Awake()
    {
        Instance = this;
        truePanel.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishTrue()
    {
        isFinished = true;
        truePanel.SetActive(true);
    }
    public bool IsFinished()
    {
        return isFinished;
    }
}
