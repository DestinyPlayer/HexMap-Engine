using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    void Start() // Start is called before the first frame update
    {
        GenerateMap();
    }

    //Let's keep all the various objects here
    public float mapHeight, mapWidth;
    public bool  verticalWrap, horizontalWrap;
    public GameObject HexPrefab;
    public Material[] HexMats; //0 - Water 1 - Grass 2 - Snow 3 - Desert

    private void OnDrawGizmos() //Debug position Gizmo thingy
    {}

    public void GenerateMap() //Generates the hex map
    {
        for (int column = 0; column < mapHeight; column++) //Vertical
        {
            for (int row = 0; row < mapWidth; row++)       //Horizontal
            {
                //Let's make a Hex
                Hex h = new Hex(column, row);

                Vector3 pos = h.PosFromCamera(
                        mapWidth,
                        mapHeight,
                        Camera.main.transform.position
                    );

                GameObject hexObj = (GameObject)Instantiate
                (
                    HexPrefab,
                    pos,
                    Quaternion.identity,
                    this.transform
                );
                hexObj.GetComponent<HexBehaviour>().Hex = h;
                hexObj.GetComponent<HexBehaviour>().HexMap = this;

                //This is the part that deals with the Hexmap's material distribution
                MeshRenderer mr = hexObj.GetComponentInChildren<MeshRenderer>();
                mr.material = HexMats[Random.Range(0, HexMats.Length)];
            }
        }
    }

    public void Update()
    {
        
    }
}
