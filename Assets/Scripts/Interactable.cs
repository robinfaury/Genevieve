using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    private Material originMat;
    void Start()
    {
        originMat = GetComponent<Renderer>().material;
        Init();
    }
    public virtual void Init()
    {

    }

    public virtual bool IsCloseEnough(Genevieve genevieve)
    {
        return (new Vector2(genevieve.transform.position.x, genevieve.transform.position.z) - new Vector2(transform.position.x, transform.position.z)).magnitude < 1.3f;
    }
    public virtual void MouseAimed(Genevieve genevieve)
    {
        if (IsCloseEnough(genevieve))
            GetComponent<Renderer>().material = Main.staticOverMatCloseEnough;
        else
            GetComponent<Renderer>().material = Main.staticOverMat;
    }
    public virtual void MouseEnter(Genevieve genevieve)
    {

    }
    public virtual void MouseLeave(Genevieve genevieve)
    {
        GetComponent<Renderer>().material = originMat;
    }
    public virtual void Take(Genevieve genevieve)
    {
        GetComponent<Renderer>().material = originMat;
        GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Collider>().enabled = false;
    }
    public virtual void Throw(Genevieve genevieve)
    {
        GetComponent<Rigidbody>().detectCollisions = true;
        GetComponent<Collider>().enabled = true;
        transform.position = genevieve.transform.position + new Vector3(genevieve.cameraController.GetDirection().x, 2, genevieve.cameraController.GetDirection().y);
    }
    public virtual void Held(Genevieve genevieve)
    {

    }
}
