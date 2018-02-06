using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Changer : MonoBehaviour
{

    public Sprite[] sprite;

    int index;
    string tileType;

    bool seaUp;
    bool seaDown;
    bool seaLeft;
    bool seaRight;

    bool tmpSeaFwd;
    bool tmpSeaBck;
    bool tmpSeaLeft;
    bool tmpSeaRight;

    bool userChangedTile = false;

    int totalIndex;

    GameObject tileFront;
    GameObject tileBack;
    GameObject tileLeft;
    GameObject tileRight;

    void Start()
    {

        index = Random.Range(0, 2);
        
        if(Random.Range(0,2) == 0)
        {
            index++;
        }
        
        GetComponent<SpriteRenderer>().sprite = sprite[index];

        if (index == 0)
        {
            tileType = "Sea";
        }

        if (index > 0)
        {
            tileType = "Land";
        }


        ScanLocalTiles();

    }

    void Update()
    {

        if (index == 0)
        {
            tileType = "Sea";
        }

        if (index > 0)
        {
            tileType = "Land";
        }

        if (tileType == "Sea")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[0];

        }

        if (tileType == "Land")
        {
            
            CheckTilesAround();
            if (userChangedTile)
            {
                ScanLocalTiles();
                userChangedTile = false;
            }
            //  CheckBeachDirection();

            if (tmpSeaFwd != seaUp || tmpSeaBck != seaDown || tmpSeaLeft != seaLeft
                || tmpSeaRight != seaRight)
            {
                Debug.Log("1");
                //totalIndex = 1;

                CheckBeachDirection();
                ScanLocalTiles();
                TileTypeCalculation();
            }

            //CheckBeachDirection();
            //TileTypeCalculation();


            tmpSeaFwd = seaUp;
            tmpSeaBck = seaDown;
            tmpSeaLeft = seaLeft;
            tmpSeaRight = seaRight;

        }
    }

    private void FixedUpdate()
    {


    }
    
    public string GetTileTypeString()
    {
        return tileType;
    }
    
    private void CheckBeachDirection()
    {
        if (seaUp == true)
        {
            totalIndex += 2;
        }

        if (seaDown == true)
        {
            totalIndex += 3;
        }

        if (seaLeft == true)
        {
            totalIndex += 7;
        }

        if (seaRight == true)
        {
            totalIndex += 13;
        }
    }

    private void CheckTilesAround()
    {
        if (tileFront != null)
        {
            if (tileFront.GetComponent<Tile_Changer>().GetTileTypeString() == "Sea")
            {
                seaUp = true;
            }
            else { seaUp = false; }
        }

        if (tileBack != null)
        {
            if (tileBack.GetComponent<Tile_Changer>().GetTileTypeString() == "Sea")
            {
                seaDown = true;
            }
            else { seaDown = false; }
        }

        if (tileLeft != null)
        {
            if (tileLeft.GetComponent<Tile_Changer>().GetTileTypeString() == "Sea")
            {
                seaLeft = true;
            }
            else { seaLeft = false; }

        }

        if (tileRight != null)
        {
            if (tileRight.GetComponent<Tile_Changer>().GetTileTypeString() == "Sea")
            {
                seaRight = true;
            }
            else { seaRight = false; }

        }
    }

    private void TileTypeCalculation()
    {

        if (seaUp && !seaDown && !seaLeft && !seaRight) // sea up
        {
            GetComponent<SpriteRenderer>().sprite = sprite[2];
        }

        else if (seaUp && !seaDown && !seaLeft && seaRight) // sea up right
        {
            GetComponent<SpriteRenderer>().sprite = sprite[4];
        }

        else if (seaUp && !seaDown && seaLeft && !seaRight) // sea up left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[3];
        }

        else if (!seaUp && seaDown && !seaLeft && !seaRight) // sea bck
        {
            GetComponent<SpriteRenderer>().sprite = sprite[5];
        }

        else if (!seaUp && seaDown && !seaLeft && seaRight) // sea bck right
        {
            GetComponent<SpriteRenderer>().sprite = sprite[7];
        }

        else if (!seaUp && seaDown && seaLeft && !seaRight) // sea bck left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[6];
        }

        else if (!seaUp && !seaDown && seaLeft && !seaRight) // sea left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[9];
        }

        else if (!seaUp && !seaDown && !seaLeft && seaRight) // sea right
        {
            GetComponent<SpriteRenderer>().sprite = sprite[8];
        }

        else if (!seaUp && !seaDown && seaLeft && seaRight) // sea right left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[10];
        }

        else if (seaUp && seaDown && !seaLeft && !seaRight) // sea up down
        {
            GetComponent<SpriteRenderer>().sprite = sprite[11];
        }

        else if (seaUp && seaDown && seaLeft && !seaRight) // sea up down left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[13];
        }

        else if (seaUp && seaDown && !seaLeft && seaRight) // sea up down right
        {
            GetComponent<SpriteRenderer>().sprite = sprite[12];
        }

        else if (seaUp && !seaDown && seaLeft && seaRight) // sea up right left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[14];
        }

        else if (!seaUp && seaDown && seaLeft && seaRight) // sea  down right left
        {
            GetComponent<SpriteRenderer>().sprite = sprite[15];
        }

        else if (seaUp && seaDown && seaLeft && seaRight) // sea up right left down
        {
            index = 0;
        }

        else if (!seaUp && !seaDown && !seaLeft && !seaRight)// land
        {
            GetComponent<SpriteRenderer>().sprite = sprite[1];
            index = 1;

        }

    }

    public void ScanLocalTiles()
    {
        //Debug.Log("ScanLocalTiles function called");
        RaycastHit hit;

        if (tileType == "Land")
        {
            Vector3 fwd = transform.TransformDirection(Vector3.up);
            Vector3 bck = transform.TransformDirection(Vector3.down);
            Vector3 left = transform.TransformDirection(Vector3.left);
            Vector3 right = transform.TransformDirection(Vector3.right);

            

            if (Physics.Raycast(transform.position, fwd, out hit, 2))
            {
                tileFront = hit.transform.gameObject;
            }

            if (Physics.Raycast(transform.position, bck, out hit, 2))
            {
                tileBack = hit.transform.gameObject;
            }

            if (Physics.Raycast(transform.position, left, out hit, 2))
            {
                tileLeft = hit.transform.gameObject;
            }

            if (Physics.Raycast(transform.position, right, out hit, 2))
            {
                tileRight = hit.transform.gameObject;
            }

            CheckTilesAround();
            TileTypeCalculation();

            tmpSeaFwd = seaUp;
            tmpSeaBck = seaDown;
            tmpSeaLeft = seaLeft;
            tmpSeaRight = seaRight;
        }
    }

    public void SetTypeTileInt(int changeTile)
    {
        
        index = changeTile;
        userChangedTile = true;
    }

    public int GetTypeTileInt()
    {
        return index;
    }
}
