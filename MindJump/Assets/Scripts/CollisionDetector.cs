using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay(Collision collisionInfo) {
        Debug.Log("TOUCHING");
        foreach (ContactPoint contact in collisionInfo.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);

            }
        }
}
