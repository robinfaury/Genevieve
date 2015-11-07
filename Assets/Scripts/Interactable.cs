using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Init();
    public abstract void Refresh();
    public virtual void MouseOver(Genevieve genevieve)
    {

    }
    public virtual void Interact(Genevieve genevieve)
    {

    }
}
