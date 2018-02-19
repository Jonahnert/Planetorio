using UnityEngine;
using System.Collections;

public class speedScaler : MonoBehaviour {
    public float speedScale = 0.1f;
    public Rigidbody speedRef;
    public float scaleVariation = 0.5f;
    private float randomScale;
	// Use this for initialization
	void Awake () {
       // randomScale = Random.Range(1.0f - scaleVariation, 1.0f + scaleVariation);
	}
	
	// Update is called once per frame
	void Update () {
       // transform.localScale = new Vector3((speedScale * speedRef.velocity.magnitude * randomScale), transform.localScale.y, transform.localScale.z);
	
	}
}
