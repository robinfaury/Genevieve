using UnityEngine;
using System.Collections;

public class InteractableCocotte : Interactable
{
    public bool gotEngrais = false;
    public bool gotWhiteSpirit = false;
    public bool gotPoireaux = false;
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
            if (!gotEngrais && (new Vector2(transform.position.x, transform.position.z) -
                new Vector2(GameObject.Find("Map(Clone)").transform.Find("engrais").position.x, GameObject.Find("Map(Clone)").transform.Find("engrais").position.z)).magnitude < 1.0f)
            {
                gotEngrais = true;
                GameObject.Find("Map(Clone)").transform.Find("engrais").position = new Vector3(1000, 1000, 1000);
            }
            else if (!gotWhiteSpirit && (new Vector2(transform.position.x, transform.position.z) -
                new Vector2(GameObject.Find("Map(Clone)").transform.Find("white_spirit").position.x, GameObject.Find("Map(Clone)").transform.Find("white_spirit").position.z)).magnitude < 1.0f)
            {
                gotWhiteSpirit = true;
                GameObject.Find("Map(Clone)").transform.Find("white_spirit").position = new Vector3(1000, 1000, 1000);
            }
            else if (!gotPoireaux && (new Vector2(transform.position.x, transform.position.z) -
                new Vector2(GameObject.Find("Map(Clone)").transform.Find("poireau").position.x, GameObject.Find("Map(Clone)").transform.Find("poireau").position.z)).magnitude < 1.0f)
            {
                gotPoireaux = true;
                GameObject.Find("Map(Clone)").transform.Find("poireau").position = new Vector3(1000, 1000, 1000);
            }
            if(gotEngrais && gotWhiteSpirit && gotPoireaux)
            {
                genevieve.gameManager.IncreaseCurrentLevelProgress(5);
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
