using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class hexgridMaker : MonoBehaviour
{
    public Transform hexPrefab;

    public int gridWidth = 11;
    public int gridHeight = 11;

    float hexWidth = 1.73f;
    float hexHeight = 2.0f;
    public float gap = 1.0f;

    Vector3 startPos;


    public List<List<GameObject>> columns = new List<List<GameObject>>();
    public List<GameObject> rows = new List<GameObject>();

    public float columnCount = 10;
    public float rowCount = 10;
    private float columnOffset = 0;
    private float rowOffset = 0;
    public int coreWidth = 1;

    public float columnInterval = 1;
    public float rowInterval = 1;

    public Transform spawnTile;
    public Transform powerSourceTile;
    public Transform resourceTile;
    public Transform groundLevelTile;

    private Transform myTile;

    void Start()
    {
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float y = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, y, 0);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float y = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, y, 0);
    }

    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                setTileType(y,x);
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                //hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
            }
        }
    }

    void setTileType(int i, int k)
    {
        Transform mySpawnTile;
        mySpawnTile = spawnTile;
        if (i > ((columnCount / 2) - coreWidth - 1) && i < ((columnCount / 2) + coreWidth +1))
        {
            if (k > ((rowCount / 2) - coreWidth - 1) && k < ((rowCount / 2) + coreWidth +1))
            {
                mySpawnTile = powerSourceTile;
            }
        }
        if (i == columnCount || k == rowCount || i == 0 || k == 0)
        {
            mySpawnTile = groundLevelTile;
        }
        else if (i == columnCount -1 || k == rowCount -1 || i == 1 || k == 1)
        {
            mySpawnTile = resourceTile;
        }
        hexPrefab = mySpawnTile;
    }
}
