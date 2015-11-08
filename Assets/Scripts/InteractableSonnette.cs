using UnityEngine;
using System.Collections;

public class InteractableSonnette : Interactable
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
            if ((new Vector2(transform.position.x, transform.position.z) - new Vector2(-40.7f, 1.0f)).magnitude < 1.0f)
            {
                Debug.LogWarning((new Vector2(transform.position.x, transform.position.z) - new Vector2(2.7f, 5.2f)).magnitude);
                genevieve.gameManager.IncreaseCurrentLevelProgress(4);
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
