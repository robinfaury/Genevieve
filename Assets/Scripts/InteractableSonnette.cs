using UnityEngine;
using System.Collections;

public class InteractableSonnette : Interactable
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
        transform.position = genevieve.rightHand.position;
        transform.rotation = genevieve.rightHand.rotation;
        if (genevieve.gameManager.running && Input.GetMouseButton(1) && !genevieve.moving)
        {
            genevieve.animToPlay = 6;
            if (timer.Get() >= hitDuration)
            {
                timer.Substract(hitDuration);
                genevieve.gameManager.IncreaseCurrentLevelProgress(1);
            }
        }
        else
        {
            if (genevieve.moving)
                genevieve.animToPlay = 5;
            else
                genevieve.animToPlay = 4;
            timer.Reset();
        }
    }
}
