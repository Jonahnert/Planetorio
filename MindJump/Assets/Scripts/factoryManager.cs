using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factoryManager : MonoBehaviour {
    public List<GameObject> myPath = new List<GameObject>();
    public GameObject powerSource;
    public GameObject baseTile;

    public GameObject myResource;
    private float spawnDelay = 1.5f;

    private float spawnOffset = 3.0f;
    public Transform spawnLocation;
    private int resourceMask;
    public Collider[] boxCheck;
    // Use this for initialization
    public Vector3 startPos;
    public Vector3 endPos;
    public Material pathMat;
    private bool isLaunching = false;

    public GameObject newSpawn;
    public Vector3 launchVector;
    public float launchFactor = 100;

    void Start () {
        //StartCoroutine("spawnResource");
        resourceMask = 1<<13;
        launchVector = new Vector3(0, 0, 0);

    }
	void OnMouseDown()
    {
        //Debug.Log("Factory Clicked!");
        //isLaunching = true;
        //startPos = transform.position;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) && isLaunching)
        {
            //endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //newSpawn.GetComponent<Rigidbody>().AddForce(launchVector * launchFactor);
            //newSpawn = new GameObject();
        }
		
	}

    //adds force to obj spawned then loses reference to it
    public void LaunchResource(Vector3 myLaunchVector)
    {
        if(newSpawn != null)
        {
            Rigidbody myBody = newSpawn.GetComponent<Rigidbody>();
            myBody.useGravity = true;
            myBody.AddForce(myLaunchVector * launchFactor);
            launchVector = myLaunchVector;
            newSpawn.GetComponent<fuelManager>().myOriginFactory = gameObject;
            //newSpawn = null;
        }


    }
    //spawns repeatedly at spawndelay intervals
    IEnumerator spawnResource()
    {
        yield return new WaitForSeconds(spawnDelay);
        boxCheck = Physics.OverlapSphere(spawnLocation.position, 1, resourceMask);
        if (boxCheck.Length == 0)
        {
            
            if(launchVector != new Vector3(0, 0, 0))
            {
                Vector3 spawnPositon = transform.position + (launchVector * spawnOffset);
                spawnPositon.z = 0f;
                newSpawn = Instantiate(myResource, spawnPositon, Quaternion.identity);
                LaunchResource(launchVector);
            }
            
            StartCoroutine("spawnResource");
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine("spawnResource");
        }
        
    }
    IEnumerator spawnTracker()
    {
        //yield return new WaitForSeconds(spawnDelay);
        boxCheck = Physics.OverlapSphere(spawnLocation.position, 1, resourceMask);
        if (boxCheck.Length == 0)
        {

            if (launchVector != new Vector3(0, 0, 0))
            {
                Vector3 spawnPositon = transform.position + (launchVector.normalized * spawnOffset);
                spawnPositon.z = 0f;
                newSpawn = Instantiate(myResource, spawnPositon, Quaternion.identity);
                LaunchResource(launchVector);
            }

            //StartCoroutine("spawnResource");
        }
        else
        {
            //yield return new WaitForSeconds(0.5f);
            //StartCoroutine("spawnResource");
        }
        yield break;

    }
    public void DrawPath(Vector3[] points, float width, Vector3 endPos)
    {
        Debug.Log("I drew a path");
        //Mesh myPath = extrudeAlongPath(points, width);
        //GameObject myNewPath = new GameObject();
        //GameObject path = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        GameObject path = Instantiate(new GameObject(), transform.TransformPoint(transform.position), Quaternion.identity);
        path.AddComponent<MeshRenderer>();
        path.GetComponent<Renderer>().material = pathMat;   
        path.AddComponent<MeshFilter>();

        for(int i = 0; i < points.Length; i++)
        {
            points[i] -= path.transform.position;
        }


        path.GetComponent<MeshFilter>().mesh = extrudeAlongPath(points, width);
        path.AddComponent<MeshCollider>();



    }
    public static Mesh extrudeAlongPath(Vector3[] points, float width)
    {
        if (points.Length < 2)
            return null;
        Mesh m = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<Vector3> norms = new List<Vector3>();

        for (int i = 0; i < points.Length; i++)
        {
            if (i != points.Length - 1)
            {
                Vector3 perpendicularDirection = new Vector3(-(points[i + 1].z - points[i].z), points[i].y, points[i + 1].x - points[i].x).normalized;
                verts.Add(points[i] + perpendicularDirection * width);
                norms.Add(Vector3.up);
                verts.Add(points[i] + perpendicularDirection * -width);
                norms.Add(Vector3.up);
            }
            else {
                Vector3 perpendicularDirection = new Vector3(-(points[i].z - points[i - 1].z), points[i].y, points[i].x - points[i - 1].x).normalized;
                verts.Add(points[i] + perpendicularDirection * -width);
                norms.Add(Vector3.up);
                verts.Add(points[i] + perpendicularDirection * width);
                norms.Add(Vector3.up);
            }
        }
        m.vertices = verts.ToArray();
        m.normals = norms.ToArray();

        List<int> tris = new List<int>();
        //Changed i+=3 to i++
        for (int i = 0; i < m.vertices.Length - 3; i++)
        {
            if (i % 2 == 0)
            {
                tris.Add(i + 2);
                tris.Add(i + 1);
                tris.Add(i);
            }
            else {
                tris.Add(i);
                tris.Add(i + 1);
                tris.Add(i + 2);
            }
        }
        m.triangles = tris.ToArray();

        m.name = "pathMesh";
        m.RecalculateNormals();
        m.RecalculateBounds();
        ;
        return m;
    }
}
