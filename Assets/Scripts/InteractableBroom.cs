using UnityEngine;
using System.Collections;

public class InteractableBroom : Interactable
{
    private bool isOver = false;
    private Material originMat;
    void Start() // Unity callback
    {
        originMat = GetComponent<Renderer>().material;
    }
    public override void Init() // Main callback
    {

    }

    void Update() // Unity callback
    {
        if (isOver)
            GetComponent<Renderer>().material = Main.staticOverMat;
        else
            GetComponent<Renderer>().material = originMat;
        isOver = false;
    }
    public override void Refresh() // Main callback
    {

    }

    public override void MouseOver(Genevieve genevieve)
    {
        isOver = true;
    }
    public override void Interact(Genevieve genevieve)
    {

    }
}
