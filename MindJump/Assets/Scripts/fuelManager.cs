using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuelManager : MonoBehaviour {
    public GameObject defaultDeposit;
    public float fuelAmount = 10;
    public List<Vector3> myPastPositions;
    private float trackDelay = 0.1f;
    public bool pathComplete = false;

    public GameObject myOriginFactory;

	// Use this for initialization
	void Start () {
        myPastPositions = new List<Vector3>();
        StartCoroutine("LogPosition");
    }

	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.CompareTag("Deposit"))
        {
            pathComplete = true;
            DrawPath();

            depositManager myDepositManager = collision.gameObject.transform.root.gameObject.GetComponent<depositManager>();
            if (myDepositManager != null)
            {
                myDepositManager.amount += fuelAmount;
                //Debug.Log("I added: " + fuelAmount);

            }
            Destroy(gameObject);



        }
        else if (collision.gameObject.transform.root.CompareTag("Resource"))
        {
            pathComplete = true;
            DrawPath();

            collision.gameObject.transform.root.gameObject.SetActive(false);
            GameObject newDepositNode = Instantiate(defaultDeposit, collision.gameObject.transform.root.position, Quaternion.identity);
            depositManager newDeposit = newDepositNode.GetComponent<depositManager>();
            newDeposit.amount += fuelAmount;
            //Debug.Log("I added: " + fuelAmount);
            Destroy(gameObject);
        }
        else if (collision.gameObject.transform.root.CompareTag("Factory"))
        {
            pathComplete = true;
            DrawPath();

            factoryManager myFactoryManager = collision.gameObject.transform.root.gameObject.GetComponent<factoryManager>();
            if (myFactoryManager != null)
            {
                //myFactoryManager.amount += fuelAmount;
                Debug.Log("I Factorized: " + fuelAmount);

            }
            Destroy(gameObject);
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator LogPosition()
    {
        if (!pathComplete)
        {
            myPastPositions.Add(gameObject.transform.position);
            yield return new WaitForSeconds(trackDelay);
            StartCoroutine("LogPosition");
        }
        else
        {
            yield break;
        }
    }
    public void DrawPath()
    {
        Vector3[] pathPositions = new Vector3[myPastPositions.Count];
       
        for(int i =0; i< myPastPositions.Count; i++)
        {
            pathPositions[i] = myPastPositions[i];
            //Debug.Log(pathPositions[i]);
        }
        
        myOriginFactory.GetComponent<factoryManager>().DrawPath(pathPositions, 1.0f, gameObject.transform.position);
    }
}
