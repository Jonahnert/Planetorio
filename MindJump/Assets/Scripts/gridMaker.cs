using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridMaker : MonoBehaviour {
	public List<List<GameObject>> columns= new List<List<GameObject>> ();
	public List<GameObject> rows= new List<GameObject> ();

	public float columnCount = 10;
	public float rowCount = 10;
	private float columnOffset = 0;
	private float rowOffset = 0;
    public int coreWidth = 1;

	public float columnInterval = 1;
	public float rowInterval = 1;

	public GameObject spawnTile;
    public GameObject powerSourceTile;
    public GameObject resourceTile;

    private GameObject myTile;
	// Use this for initialization
	void Start () {
		//columns = new List<>();
		//rows = new List<> ();

		for (int i = 0; i < columnCount; i++) 
		{
			for (int k = 0; k < rowCount; k++) 
			{
                GameObject mySpawnTile = new GameObject();
                mySpawnTile = spawnTile;
                if (i > ((columnCount / 2) - coreWidth - 1) && i < ((columnCount / 2) + coreWidth ))
                {
                    if (k > ((rowCount / 2) - coreWidth - 1) && k < ((rowCount / 2) + coreWidth ))
                    {
                        mySpawnTile = powerSourceTile;
                    }
                }
                if(i == columnCount-1 || k == rowCount-1 || i == 0 || k == 0)
                {
                    mySpawnTile = resourceTile;                
                }
                myTile = Instantiate(mySpawnTile, new Vector3(transform.position.x + rowOffset, transform.position.y + columnOffset, transform.position.z), Quaternion.identity);
                rows.Add(myTile);
				rowOffset += rowInterval;
			}
			columns.Add(rows);
			rows.Clear();
			columnOffset += columnInterval;
			rowOffset = 0;
		}
		
	}
	

	// Update is called once per frame
	void Update () {
		
	}
}
