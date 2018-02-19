using UnityEngine;
using System.Collections;

public class orbitingObject2D : MonoBehaviour {
    public Transform gravityWellTarget;
    public Transform orbital;
    public GameObject[] gravityWells;
    public float xForce = 00;
    public float yForce = 00;
    // Use this for initialization
    void Awake () {
        //orbital = transform;
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * xForce);
        //GetComponent<Rigidbody2D>().AddForce(transform.up * yForce);

       // gravityWells = GameObject.FindGameObjectsWithTag("gravityWell");
        //Debug.Log("My Wells Are: " + gravityWells[0]);

    }

    void Start()
    {
        orbital = transform;
        GetComponent<Rigidbody2D>().AddForce(transform.forward * xForce);
        GetComponent<Rigidbody2D>().AddForce(transform.up * yForce);


        //StartCoroutine("delayStart");
        gravityWells = GameObject.FindGameObjectsWithTag("gravityWell");

    }
	
	// Update is called once per frame
	void Update () {
        gravityWells = GameObject.FindGameObjectsWithTag("gravityWell");
        foreach (GameObject gravityWell in gravityWells)
        {
            gravityWellTarget = gravityWell.transform;

            Vector3 line = gravityWellTarget.position - orbital.position;
            line.Normalize();

            float distance = Vector3.Distance(gravityWellTarget.position, orbital.position);
            float attractionForce = gravityWell.GetComponent<Rigidbody2D>().mass;
            GetComponent<Rigidbody2D>().AddForce(line * (attractionForce / (1.5f*distance)));
        }
	
	}
    IEnumerator delayStart()
    {
        yield return new WaitForSeconds(0);
        gravityWells = GameObject.FindGameObjectsWithTag("gravityWell");
        Debug.Log("My Wells Are: " + gravityWells[0]);
    }
}
