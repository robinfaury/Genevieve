using UnityEngine;
using System.Collections;

public class MicroMove : MonoBehaviour
{
    public float precision = 0.1f;

    public Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(Random.Range(-0.000000002f * precision, 0.000000002f * precision), Random.Range(-0.000000002f * precision, 0.000000002f * precision), Random.Range(-0.000000002f * precision, 0.000000002f * precision)));
    }
}
