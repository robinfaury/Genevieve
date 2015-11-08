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

        if (this.transform.position.y > 3.6043f)
        {
            if (floor == 0)
            {
                this.transform.position = this.transform.position + new Vector3(-46.2f, 0.0f, 0.0f);
            }
            if (floor == -1)
            {
                this.transform.position = this.transform.position + new Vector3(46.2f, 0.0f, 0.0f);
            }
            ++floor;
            this.transform.position = new Vector3(this.transform.position.x, -3.5838f, this.transform.position.z);
        }
        if (this.transform.position.y < -3.5838f)
        {
            if (floor == 0)
            {
                this.transform.position = this.transform.position + new Vector3(-46.2f, 0.0f, 0.0f);
            }
            if (floor == 1)
            {
                this.transform.position = this.transform.position + new Vector3(46.2f, 0.0f, 0.0f);
            }
            --floor;
            this.transform.position = new Vector3(this.transform.position.x, 3.6043f, this.transform.position.z);
        }
    }
}
