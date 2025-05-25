using System;
using UnityEngine;

public class HumanRotater : MonoBehaviour
{
    public int facingDirection = -1; // -1: arka, 1: ön
    public OilingManager backOiler;
    public OilingManager frontOiler;
    public HandPositioner handPositioner;


    public ParticleTraker particleTraker;
    private bool oilingFinished = false;
    private void Start()
    {
        facingDirection = -1;
        backOiler.gameObject.SetActive(true);
        frontOiler.gameObject.SetActive(false);
        particleTraker.SwitchOilManager(backOiler);
        handPositioner.SwitchOilManager(backOiler);
    }
    public void HumanRotateButtonClick()
    {
        facingDirection = -facingDirection;
        if (facingDirection==1)
        {
            backOiler.gameObject.SetActive(false);
            frontOiler.gameObject.SetActive(true);
        }
        else
        {
            backOiler.gameObject.SetActive(true);
            frontOiler.gameObject.SetActive(false);
        }
        handPositioner.SwitchOilManager(facingDirection == 1 ? frontOiler : backOiler);
        particleTraker.SwitchOilManager(facingDirection == 1 ? frontOiler : backOiler);

    }
    private void Update()
    {
        if (!oilingFinished && backOiler.GetRubAmount() == 1f && frontOiler.GetRubAmount()==1f)
        {
            oilingFinished = true;
            Debug.Log("game ended");
        }
    }
}
