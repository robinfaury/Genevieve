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
            if ((new Vector2(transform.position.x, transform.position.z) - new Vector2(2.7f, 5.2f)).magnitude < 1.0f)
            {
                Debug.LogWarning((new Vector2(transform.position.x, transform.position.z) - new Vector2(2.7f, 5.2f)).magnitude);
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

    void Update()
    {
        if (this.transform.position.y < 0.0f)
            this.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
    }
}
