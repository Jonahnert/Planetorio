using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom2D : MonoBehaviour {
   
    public GameObject player1;
    public GameObject player2;
    private Vector3 player1pos;
    private Vector3 player2pos;
    public float playerDistance;
    public float zOffset = -5;
    public float minZoom = 10;
    public float minSize = 10;
    public float distMultiplier = 0.20f;
    public float previousDistance = 0;
    // Use this for initialization
    void Start () {
        playerDistance = CheckDistance();
        previousDistance = playerDistance;
    }
	
	// Update is called once per frame
	void Update () {
        playerDistance = CheckDistance();
        Vector3 lookPoint = ((player1pos - player2pos) * 0.5f) + player2pos;
        transform.position = new Vector3(player1pos.x, player1pos.y, minZoom); //lookpoint
        Camera.main.orthographicSize = minSize + (playerDistance * distMultiplier);
        //Camera.main.transform.Translate(Vector3.forward * (previousDistance - playerDistance) * distMultiplier);
        previousDistance = playerDistance;
        //Vector3 newPos = new Vector3(lookPoint.x, minZoom + playerDistance * distMultiplier, lookPoint.z);
        //transform.position = new Vector3(newPos.x, newPos.y, newPos.z + (zOffset * distMultiplier * 0.5f));
	}
    float CheckDistance()
    {
        player1pos = player1.transform.position;
        player2pos = player2.transform.position;
        return Vector3.Distance(player1pos, player2pos);
    }
}
