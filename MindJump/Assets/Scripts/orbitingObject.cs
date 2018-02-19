using UnityEngine;
using System.Collections;

public class orbitingObject : MonoBehaviour {
    public Transform gravityWellTarget;
    public Transform orbital;
    public GameObject[] gravityWells;
    public float xForce = 100;
    public float yForce = 100;
    // Use this for initialization
    void Awake () {
        orbital = transform;
        GetComponent<Rigidbody>().AddForce(transform.forward * xForce);
        GetComponent<Rigidbody>().AddForce(transform.up * yForce);
	
	}

    void Start()
    {
        gravityWells = GameObject.FindGameObjectsWithTag("gravityWell");

    }
	
	// Update is called once per frame
	void Update () {

        foreach(GameObject gravityWell in gravityWells)
        {
            gravityWellTarget = gravityWell.transform;

            Vector3 line = gravityWellTarget.position - orbital.position;
            line.Normalize();

            float distance = Vector3.Distance(gravityWellTarget.position, orbital.position);
            float attractionForce = gravityWell.GetComponent<Rigidbody>().mass;
            GetComponent<Rigidbody>().AddForce(line * (attractionForce / (1.5f*distance)));
        }
	
	}
}
