using UnityEngine;
using System.Collections;

public class launchSatelitev2 : MonoBehaviour {
    public Rigidbody Satelite;
    public Vector3 speed;
    private Rigidbody launchedProjectile;
    private Rigidbody rb;
    public float speedScale = 10;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {
        speed = rb.velocity;
        Debug.Log("SPEED = " + speed.x + ", " + speed.y + ", " + speed.z);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            launchedProjectile = Instantiate(Satelite, transform.position, transform.rotation) as Rigidbody;

            launchedProjectile.transform.SetParent(this.transform);
            
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
			launchedProjectile.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            speed = rb.velocity;
            Debug.Log("SPEED = " + speed.x + ", " + speed.y + ", " + speed.z);
            launchedProjectile.transform.SetParent(transform.parent.transform.parent.transform.parent);
            launchedProjectile.velocity = transform.TransformDirection(new Vector3(speed.x * speedScale, speed.y * speedScale, speed.z *speedScale));
            launchedProjectile.GetComponent<orbitingObject>().enabled = true;
            launchedProjectile.GetComponent<TrailRenderer>().enabled = true;
            launchedProjectile.GetComponentInChildren<TrailRenderer>().enabled = true;
            launchedProjectile.GetComponent<rocket>().enabled = true;
            launchedProjectile.tag = "satelite";
        }
        

    }
}
