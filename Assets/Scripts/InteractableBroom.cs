using UnityEngine;
using System.Collections;

public class InteractableBroom : Interactable
{
    private Timer timer;
    private const float hitDuration = 0.333f;
    public override void Init()
    {
        timer = new Timer();
    }
    
    public override void Take(Genevieve genevieve)
    {
        base.Take(genevieve);
        timer.Reset();
    }
    public override void Held(Genevieve genevieve)
    {
        transform.position = genevieve.leftHand.position;
        transform.rotation = Quaternion.LookRotation(genevieve.rightHand.position - genevieve.leftHand.position);
        if (Input.GetMouseButton(1) && !genevieve.moving)
        {
            genevieve.animToPlay = 3;
            if(timer.Get() >= hitDuration)
            {
                timer.Substract(hitDuration);
                genevieve.gameManager.IncreaseCurrentLevelProgress();
            }
        }
        else
        {
            timer.Reset();
        }
    }
}
