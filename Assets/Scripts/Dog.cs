using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour
{
    private float rotatingSpeed = 0.2f;
    void Start()
    {

    }
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(-rotatingSpeed/20.0f * Time.deltaTime, Vector3.forward);
    }
    public void Rotate()
    {
        rotatingSpeed = 2.5f * 360.0f;
    }
}
