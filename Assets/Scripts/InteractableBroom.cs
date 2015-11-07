using UnityEngine;
using System.Collections;

public class InteractableBroom : Interactable
{
    public override void Init()
    {

    }

    public override void Held(Genevieve genevieve)
    {
        transform.position = genevieve.transform.position;
        transform.rotation = genevieve.transform.rotation;
    }
}
