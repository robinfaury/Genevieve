using UnityEngine;
using System.Collections;

public class InteractableBroom : Interactable
{
    public override void Init()
    {

    }

    public override void Held(Genevieve genevieve)
    {
        transform.position = genevieve.leftHand.position;
        transform.rotation = Quaternion.LookRotation(genevieve.rightHand.position - genevieve.leftHand.position);
        if (Input.GetMouseButton(1) && !genevieve.moving)
        {
            genevieve.animToPlay = 3;
        }
    }
}
