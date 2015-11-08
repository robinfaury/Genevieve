using UnityEngine;
using System.Collections;

public class tp : MonoBehaviour {

    public int floor;

	// Use this for initialization
	void Start () {
        floor = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(floor);

        if (this.transform.position.y > 3.6043f)
        {
            if (floor == 0)
            {
                this.transform.position = this.transform.position + new Vector3(-46.2f, 0.0f, 0.0f);
                Debug.LogWarning("A" + this.transform.position);
            }
            if (floor == -1)
            {
                this.transform.position = this.transform.position + new Vector3(46.2f, 0.0f, 0.0f);
                Debug.LogWarning("B" + this.transform.position);
            }
            ++floor;
            this.transform.position = new Vector3(this.transform.position.x, -3.5838f, this.transform.position.z);
            Debug.LogWarning("E" + this.transform.position);
        }
        if (this.transform.position.y < -3.5838f)
        {
            if (floor == 0)
            {
                this.transform.position = this.transform.position + new Vector3(-46.2f, 0.0f, 0.0f);
                Debug.LogWarning("C" + this.transform.position);
            }
            if (floor == 1)
            {
                this.transform.position = this.transform.position + new Vector3(46.2f, 0.0f, 0.0f);
                Debug.LogWarning("D" + this.transform.position);
            }
            --floor;
            this.transform.position = new Vector3(this.transform.position.x, 3.6043f, this.transform.position.z);
            Debug.LogWarning("F" + this.transform.position);
        }
    }
}
