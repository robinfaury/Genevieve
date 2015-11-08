using UnityEngine;
using System.Collections;

public class InteractableSonnette : Interactable
{
    public override void Init()
    {
    }

    public override void Take(Genevieve genevieve)
    {
        if (genevieve.gameManager.running && !genevieve.moving)
            genevieve.gameManager.IncreaseCurrentLevelProgress(4);
    }
    public override void Held(Genevieve genevieve)
    {

    }
}
