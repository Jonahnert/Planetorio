using UnityEngine;
using System.Collections;

public class proximitySphere : MonoBehaviour {
    public GameObject drainTarget;
    public GameObject entryTrailPrefab;
    public GameObject entrySpherePrefab;
    public GameObject entryLookCenterPrefab;
    public GameObject exitTrailPrefab;
    public GameObject exitLookCenterPrefab;
    void OnTriggerEnter(Collider other)
    {
        GameObject parentObj;
        if (other.transform.parent.transform.parent.gameObject != null)
        {
            parentObj = other.transform.parent.transform.parent.gameObject;

           //Entry impact looks wierd TODO: fix it
            //Debug.Log(other + "ENTERED");
            if (parentObj.tag == "satelite")
            {
                if(entryTrailPrefab != null)
                {
                    //GameObject entryTrail = Instantiate(entryTrailPrefab, parentObj.transform.position, parentObj.transform.rotation) as GameObject;
                }
                if (entrySpherePrefab != null)
                {
                    //GameObject entrySphere = Instantiate(entrySpherePrefab, transform.position, Quaternion.identity) as GameObject;
                    //entrySphere.transform.LookAt(parentObj.transform);
                }
                if (entryLookCenterPrefab != null)
                {
                    GameObject entryLookCenter = Instantiate(entryLookCenterPrefab, parentObj.transform.position, Quaternion.identity) as GameObject;
                    entryLookCenter.transform.LookAt(transform);
                }
               
                
                parentObj.GetComponent<rocket>().inRange = false;              
            } 
        }
    }
    void OnTriggerStay(Collider other)
    {
        GameObject parentObj;
        if (other.transform.parent.transform.parent.gameObject != null)
        {
            parentObj = other.transform.parent.transform.parent.gameObject;


            //Debug.Log(other + "ENTERED");
            if (parentObj.tag == "satelite")
            {
                drainTarget.GetComponent<energyStar>().drainEnergy(1);
                //Debug.Log(other + "ENTERED");
                rocket myRocket = parentObj.GetComponent<rocket>();
                bool inRange = myRocket.inRange;
                if(drainTarget.GetComponent<energyStar>().currentEnergy > 0)
                {
                    myRocket.gainEnergy();
                }
                if (inRange == false)
                {
                   parentObj.GetComponent<rocket>().inRange = true;
                   parentObj.GetComponent<rocket>().StartDrain(drainTarget);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject parentObj;
        if (other.transform.parent.transform.parent.gameObject != null)
        {
            parentObj = other.transform.parent.transform.parent.gameObject;


          
            if (parentObj.tag == "satelite")
            {
                if (exitTrailPrefab != null)
                {
                    //GameObject exitTrail = Instantiate(exitTrailPrefab, parentObj.transform.position, parentObj.transform.rotation) as GameObject;
                    
                }
                if (exitLookCenterPrefab != null)
                {
                    GameObject exitLook = Instantiate(exitLookCenterPrefab, parentObj.transform.position, transform.rotation) as GameObject;
                    exitLook.transform.LookAt(transform);

                }
                
                bool inRange = parentObj.GetComponent<rocket>().inRange;
                if (inRange == true)
                {
                    parentObj.GetComponent<rocket>().inRange = false;
                    parentObj.GetComponent<rocket>().EndDrain(drainTarget);
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
