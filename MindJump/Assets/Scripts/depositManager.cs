using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depositManager : MonoBehaviour {
    public float amount = 0;
    public float fuelThreshold = 30;
    public GameObject myFactory;
    private bool growing = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (growing && amount >= fuelThreshold)
        {
            GameObject newFactoryNode = Instantiate(myFactory, gameObject.transform.position, Quaternion.identity);
            factoryManager newFactory = newFactoryNode.GetComponent<factoryManager>();



            gameObject.SetActive(false);
            growing = false;
        }
    }
}
