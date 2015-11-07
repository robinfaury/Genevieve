using UnityEngine;
using System.Collections;

public class MicroMove : MonoBehaviour
{
    public Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(Random.Range(-0.00002f, 0.00002f), Random.Range(-0.00002f, 0.00002f), Random.Range(-0.00002f, 0.00002f)));
    }
}
