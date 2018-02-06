using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileMap : MonoBehaviour
{


    public GameObject prefab;

    public int xSize;
    public int ySize;

    public List<GameObject> InstanceList;

    // Use this for initialization
    void Start()
    {
        ySize = 50;
        xSize = 50;
        GenerateNewTileMap();

        for (int i = 0; i < InstanceList.Count; i++)
        {
            InstanceList[i].GetComponent<Tile_Changer>().ScanLocalTiles();
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (xSize < 0)
            xSize = 0;

        if (ySize < 0)
            ySize = 0;


        if (xSize >= 100)
            xSize = 100;

        if (ySize >= 100)
            ySize = 100;



        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveTileMap();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateNewTileMap();
        }
    }

    public void GenerateNewTileMap()
    {
        RemoveTileMap();

        for (int x = 0; x < xSize; x++)
        {

            for (int y = 0; y < ySize; y++)
            {
                GameObject currentTile = Instantiate(prefab, new Vector2(x * 2.0F, y * 2.0F), Quaternion.identity);
                InstanceList.Add(currentTile);

            }

        }

    }

   public void RemoveTileMap()
    {
        int count = InstanceList.Count;

        for (int i = 0; i < count; i++)
        {

            Destroy(InstanceList[0]);
            InstanceList.RemoveAt(0);
        }
    }


   public void AddYSize()
    {
        ySize++;
    }

    public void TakeYSize()
    {
        ySize--;
    }

    public void AddXSize()
    {
        xSize++;
    }

    public void TakeXSize()
    {
        xSize--;
    }

    public int GetXsize()
    {
        return xSize;
    }

    public int GetYsize()
    {
        return ySize;
    }
}
