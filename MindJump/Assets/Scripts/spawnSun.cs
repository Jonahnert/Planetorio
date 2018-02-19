using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSun : MonoBehaviour {
    public GameObject sunPrefab;
    private RaycastHit rayInfo;
    private int layerMask;
    private float startSize;
    private float startX;
    public float sizingFactor = 0.02f;
    private GameObject lastSpawn = null;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            layerMask = 1 << 8;
            //RaycastHit myRaycast = Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), out rayInfo, layerMask))
            {
                lastSpawn = Instantiate(sunPrefab, rayInfo.point, Quaternion.identity);
            }
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            startX = position.x;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 size = lastSpawn.transform.localScale;
            size.x = startSize + Mathf.Abs(Input.mousePosition.x - startX) * sizingFactor;
            size.y = startSize + Mathf.Abs(Input.mousePosition.x - startX) * sizingFactor;
            lastSpawn.transform.localScale = size;
        }

    }

       

        
    }
