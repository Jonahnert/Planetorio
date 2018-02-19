using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathMaker : MonoBehaviour {
	public Camera camera;
	public List<GameObject> tempPathTiles = new List<GameObject> ();
    public List<GameObject> pathTiles = new List<GameObject>();
    public bool makingPath = false;
    public bool launching = false;
    public GameObject originTile;
    private GameObject lastTile;

    public GameObject defaultFactory;
    public float adjCheckDistance = 1.0f;
    // Use this for initialization
    void Start () {
		
	}


	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform.root;
                //Debug.Log(objectHit.name);

                if (objectHit.gameObject.CompareTag("Resource"))
                {
                    makingPath = true;
                    originTile = objectHit.gameObject;
                }
                
                else if (objectHit.gameObject.CompareTag("Factory"))
                {
                    originTile = objectHit.gameObject;
                    launching = true;
                }
            }
        }
        if (makingPath && Input.GetMouseButton(0))
        {
            //Debug.Log("My Mouse Is Down!");
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform.root;
                //Debug.Log(objectHit.name);


                // Do something with the object that was hit by the raycast.
                if (objectHit.gameObject.CompareTag("Tile") )
                {
                    if(CheckAlongPath(objectHit.gameObject))
                    {
                        tempPathTiles.Add(objectHit.gameObject);
                        objectHit.gameObject.tag = "tempPath";
                        lastTile = objectHit.gameObject;
                        objectHit.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.gray;
                    }
                    
                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            Debug.Log("My Mouse Is Up!");
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform.root;

                // Do something with the object that was hit by the raycast.
                if (makingPath && objectHit.gameObject.CompareTag("PowerSource") && CheckAlongPath(objectHit.gameObject))
                {
                    foreach(GameObject tile in tempPathTiles)
                    {
                        tile.tag = "path";
                        pathTiles.Add(tile);
                        
                        tile.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
                    }
                    //create new factory at start location
                    GameObject newFactoryNode = Instantiate(defaultFactory, originTile.gameObject.transform.position, Quaternion.identity);
                    factoryManager newFactory = newFactoryNode.GetComponent<factoryManager>();
                    newFactory.myPath = pathTiles;
                    newFactory.powerSource = objectHit.gameObject;
                    newFactory.baseTile = originTile;
                    originTile.SetActive(false);
                    tempPathTiles.Clear();                  
                }              
                else if(!launching)
                {
                    foreach (GameObject tile in tempPathTiles)
                    {

                            tile.tag = "Tile";
                            tile.GetComponentInChildren<MeshRenderer>().material.color = Color.white;

                    }
                    tempPathTiles.Clear();
                }

                makingPath = false;
            }
            if (launching)
            {
                //set launch vector for factory
                launching = false;
                Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                //mousePos.z = 0.0f;
                Vector3 endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                Vector3 launchVector = endPos - originTile.transform.position;
                factoryManager originFactory = originTile.GetComponent<factoryManager>();
                originFactory.launchVector = launchVector;
                //originFactory.LaunchResource(launchVector);
                originFactory.StartCoroutine("spawnTracker");           
            }
        }
		
	}
    //TODO: if you mouse back over the last tile you were at it rewinds the path
    bool CheckAlongPath(GameObject centerObj)
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(centerObj.transform.position, adjCheckDistance, transform.forward, 10);
        foreach (RaycastHit hit in hits)
        {

            if (hit.collider.transform.root.gameObject == lastTile)
            {
                return true;
            }
            else if(tempPathTiles.Count == 0 && hit.collider.transform.root.gameObject == originTile)
            {
                return true;
            }
        }
        return false;
    }
}
