using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Genevieve genevieve;
    private Vector3 genevieveToCamDirection = new Vector3(0, 1, -1).normalized;
    private float genevieveToCamDistance = 3.0f;
    public void Init(Genevieve genevieve) // Main callback
    {
        this.genevieve = genevieve;
    }

    public void Refresh() // Main callback
    {
        if (genevieve != null)
        {
            transform.position = genevieve.transform.position + genevieveToCamDistance * genevieveToCamDirection;
            transform.rotation = Quaternion.LookRotation(-genevieveToCamDirection);
        }
    }
}
