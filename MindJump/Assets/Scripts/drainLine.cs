using UnityEngine;
using System.Collections;

public class drainLine : MonoBehaviour {
    public bool isDraining = false;
    private LineRenderer line;
    private Transform LineStart;
    private Transform LineEnd;

    public GameObject drainTarget;
	// Use this for initialization
	void Awake () {
        line = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (isDraining)
        {
            line.SetPosition(1, LineEnd.position);
        }
	}
    public void Drain(GameObject start, GameObject end)
    {

        isDraining = true;
        line.enabled = true;
        LineStart = start.transform;
        LineEnd = end.transform;
        line.SetPosition(0, LineStart.position);
    }
    public void DrainEnd()
    {
        //TODO: Have this called when satelite collides and "dies"
        this.GetComponent<LineRenderer>().enabled = false;
        isDraining = false;
    }
}
