using UnityEngine;
using System.Collections;

public class InteractableTrotter : Interactable
{
    public override void Init()
    {

    }

    public override void Held(Genevieve genevieve)
    {
        genevieve.speed = 5;
        transform.position = genevieve.transform.position;
        transform.rotation = genevieve.transform.rotation;
    }
}
