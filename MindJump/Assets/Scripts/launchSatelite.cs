using UnityEngine;
using System.Collections;

public class launchSatelite : MonoBehaviour {
    public Rigidbody Satelite;
    public float speed = 5;
    //time between shots
    public float fireDelay = 1.0f;
    private Rigidbody launchedProjectile;
	// Use this for initialization
	void Start () {
        StartCoroutine("LaunchSatellites");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            speed = 5;
            launchedProjectile = Instantiate(Satelite, transform.position, transform.rotation) as Rigidbody;

            launchedProjectile.transform.SetParent(this.transform);
            
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (speed < 40)
            {
                speed += 0.1f;
                //Debug.Log("Speed: " + speed);
            }
			launchedProjectile.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            launchedProjectile.transform.SetParent(transform.parent.transform.parent.transform.parent);
            launchedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            launchedProjectile.GetComponent<orbitingObject>().enabled = true;
            launchedProjectile.GetComponent<TrailRenderer>().enabled = true;
            launchedProjectile.GetComponentInChildren<TrailRenderer>().enabled = true;
            launchedProjectile.GetComponent<rocket>().enabled = true;
            launchedProjectile.tag = "satelite";
        }
        */

    }
    IEnumerator LaunchSatellites()
    {
        Debug.Log("Lauch Enumerator Started");
        LaunchPrep();
        Launch();
        yield return new WaitForSeconds(fireDelay);
        StartCoroutine("LaunchSatellites");
    }
    void LaunchPrep()
    {
        launchedProjectile = Instantiate(Satelite, transform.position, transform.rotation) as Rigidbody;

        //launchedProjectile.transform.SetParent(this.transform);
        launchedProjectile.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
    void Launch()
    {
        //launchedProjectile.transform.SetParent(transform.parent.transform.parent.transform.parent);
        launchedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        launchedProjectile.GetComponent<orbitingObject>().enabled = true;
        launchedProjectile.GetComponent<TrailRenderer>().enabled = true;
        launchedProjectile.GetComponentInChildren<TrailRenderer>().enabled = true;
        launchedProjectile.GetComponent<rocket>().enabled = true;
        launchedProjectile.tag = "satelite";
    }
}
