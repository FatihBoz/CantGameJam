using UnityEngine;

public class OilingFinishCheck : MonoBehaviour
{
    public static OilingFinishCheck Instance;
    public bool isFrontOilingFinished = false;
    public bool isBackOilingFinished = false;

    public GameObject checkMark;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            isFrontOilingFinished = false;
            isBackOilingFinished = false;
            checkMark.SetActive(false);

        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FinishOiling()
    {

        isFrontOilingFinished = true;
        isBackOilingFinished = true;
        checkMark.SetActive(true);
        Debug.Log("Oiling process finished.");
    }
    public bool IsFrontOilingFinished()
    {
        return isFrontOilingFinished;
    }
    public bool IsBackOilingFinished()
    {
        return isBackOilingFinished;
    }
    public bool IsFullOilingFinished()
    {
        return isFrontOilingFinished && isBackOilingFinished;
    }
}
