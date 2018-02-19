using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rocket : MonoBehaviour {
    private float localTimeAlive = 0;
    private bool isAlive = true;
    public GameObject drainLineLocator;
    List<GameObject> lines = new List<GameObject>();
    //used to create and remove draining of energy,
    //TODO: use a list or array to allow for multiple drains at once
    public bool inRange = false;
    public GameObject drainTarget;
    public float maxLightIntensity = 8;
    public float maxLightRange = 30;


    public GameObject gameManager;
    private gameManagerScript managerScript;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        managerScript = gameManager.GetComponent<gameManagerScript>();
    }

	
	// Update is called once per frame
	void Update () {
        if(transform.rotation != Quaternion.LookRotation(GetComponent<Rigidbody>().velocity))
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        }

        if (isAlive)
        {
            localTimeAlive += Time.deltaTime;
        }

        managerScript.SetTimeAlive(localTimeAlive);
       
	}
    void OnCollisionEnter()
    {
        isAlive = false;
        managerScript.CheckTimeAlive(localTimeAlive);
        managerScript.SetTimeAlive(localTimeAlive);
        this.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        mat.SetColor("_EmissionColor", Color.red);

        //turn off all line renderers to energy sources
        foreach (GameObject line in lines)
        {    
            Debug.Log("Satelite Died");
            line.GetComponent<drainLine>().DrainEnd();
            
        }

        //change tag so it no longer drains energy
        gameObject.tag = "";

    }
    public void StartDrain(GameObject target)
    {
        GameObject lineLocator = Instantiate(drainLineLocator, transform.position, Quaternion.identity) as GameObject;
        lineLocator.GetComponent<drainLine>().Drain(target, this.gameObject);
        lineLocator.GetComponent<drainLine>().drainTarget = target;
        lines.Add(lineLocator);
        //lineLocator.GetComponent<DrainLine>().Drain(this)
        //lineLocator.transform.parent = this.gameObject.transform;
        //LineRenderer line = lineLocator.GetComponent<LineRenderer>();
       //line.SetPosition(0, target.transform.position);
       // line.SetPosition(1, this.transform.position);
        Debug.Log("Satelite Entered");
    }
    public void EndDrain(GameObject target)
    {
        
        foreach (GameObject line in lines)
        {
            if (line.GetComponent<drainLine>().drainTarget.name == target.name)
            {
                Debug.Log("Satelite Exited");
                line.GetComponent<drainLine>().DrainEnd();
            }
        }
    }

    public void gainEnergy()
    {
        Light[] lights = GetComponentsInChildren<Light>();

        foreach(Light light in lights)
        {
            if(light.intensity < maxLightIntensity)
            {
                light.intensity += .025f;
            }
            if(light.range < maxLightRange)
            {
                light.range += .1f;
            }
        }
    }
}
