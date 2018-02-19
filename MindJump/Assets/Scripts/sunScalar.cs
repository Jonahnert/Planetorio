using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunScalar : MonoBehaviour {
    private Vector3 mySize;
    private Rigidbody myBody;
    public float startMass = 10.0f;
    public float startScale;
    public float massScalar = 100f;
	// Use this for initialization
	void Start () {
        myBody = GetComponent<Rigidbody>();
        startScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        mySize = transform.localScale;
        myBody.mass = Mathf.Abs(mySize.x)*massScalar;
        Debug.Log(myBody.mass + ": My Mass");
	}
}
