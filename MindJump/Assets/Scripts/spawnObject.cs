using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour {
    public GameObject spawnable;
    public float delay = 1.0f;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine("delayedSpawn");
	}
	
    IEnumerator delayedSpawn()
    {
        yield return new WaitForSeconds(delay);
        GameObject mySpawn = Instantiate(spawnable, transform.position, Quaternion.identity);
        mySpawn.GetComponent<orbitingObject2D>().enabled = true;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
