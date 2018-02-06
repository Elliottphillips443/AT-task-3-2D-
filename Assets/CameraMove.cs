using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {

    private float speed = 5.0f;

    GameObject tileSelected;

    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        Click();

    }

    void Click()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    tileSelected = hit.transform.gameObject;
                    tileSelected.GetComponent<Tile_Changer>().SetTypeTileInt(tileChanger());
                }

            }
        }
    }

    int tileChanger()
    {
        if (tileSelected.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
        {
            return 1;
        }
        else

        {
            return 0;
        }
        

    }
}
