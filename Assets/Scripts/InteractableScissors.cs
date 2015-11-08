using UnityEngine;
using System.Collections;

public class InteractableScissors : Interactable
{
    public override void Init()
    {
    }

    public override void Take(Genevieve genevieve)
    {
        base.Take(genevieve);
    }
    public override void Held(Genevieve genevieve)
    {
        transform.position = genevieve.rightHand.position;
        transform.rotation = genevieve.rightHand.rotation;
        if (genevieve.gameManager.running && Input.GetMouseButton(1) && !genevieve.moving)
        {
            genevieve.animToPlay = 9;
            if ((new Vector2(transform.position.x, transform.position.z) - new Vector2(transform.position.x, transform.position.z)).magnitude < 0.1f)
            {
                genevieve.gameManager.IncreaseCurrentLevelProgress(2);
            }
        }
        else
        {
            if (genevieve.moving)
                genevieve.animToPlay = 1;
            else
                genevieve.animToPlay = 0;
        }
    }
}
