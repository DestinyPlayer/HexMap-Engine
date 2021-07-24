using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    void Start() // Start is called before the first frame update
    {
        GenerateMap();
    }

    //Base stats
    public int   mapHeight, mapWidth;
    public bool  verticalWrap, horizontalWrap;
    public bool  debug;
    
    //Base prefab
    public GameObject HexPrefab;

    //Meshes
    public Mesh MeshWater;
    public Mesh MeshFlat;
    public Mesh MeshHill;
    public Mesh MeshMountain;
    
    //Materials
    public Material MatWater;
    public Material MatGrass;
    public Material MatSnow;
    public Material MatDesert;

    //Hex Array
    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> objMap;

    public Hex GetHex(int x, int y)
    {
        if(hexes == null)
        {
            Debug.LogError("Hexes Array not yet instantiated.");
            return null;
        }

        if (horizontalWrap == true) { x = x % mapWidth; }
        if (verticalWrap   == true) { y = y % mapHeight; }


        return hexes[x, y];
    }

    public Hex[] GetHexesWithinRadius(Hex centerHex, int radius)
    {
        List<Hex> results = new List<Hex>();

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = Mathf.Max(-radius, -dx-radius); dy <= Mathf.Min(radius, -dx+radius); dy++)
            {
                results.Add(GetHex(centerHex.column + dx, centerHex.row + dy));
            }
        }
        return results.ToArray();
    }

    public bool VerticalWrap()   { return verticalWrap;   }
    public bool HorizontalWrap() { return horizontalWrap; }

    virtual public void GenerateMap() //Generates the hex map. Nothing but water by default
    {
        hexes = new Hex[mapWidth, mapHeight];
        objMap = new Dictionary<Hex, GameObject>();

        for (int column = 0; column < mapHeight; column++) //Vertical
        {
            for (int row = 0; row < mapWidth; row++)       //Horizontal
            {
                //Let's make a Hex
                Hex h = new Hex(column, row);
                h.Elevation = -1;

                hexes[column, row] = h;

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

                objMap[h] = hexObj;

                hexObj.name = string.Format("HEX:{0},{1}", row, column);
                hexObj.GetComponent<HexBehaviour>().Hex = h;
                hexObj.GetComponent<HexBehaviour>().HexMap = this;

                if (debug == true)
                {
                    hexObj.GetComponentInChildren<TextMesh>().text = string.Format("{0}\n{1}", row, column);
                }
            }
        }
        UpdateHexVisuals();
    }

    public void UpdateHexVisuals()
    {
        for (int column = 0; column < mapHeight; column++) //Vertical
        {
            for (int row = 0; row < mapWidth; row++)       //Horizontal
            {
                Hex h = hexes[column, row];
                GameObject hexObj = objMap[h];
                //This is the part that deals with the Hex's material
                MeshRenderer mr = hexObj.GetComponentInChildren<MeshRenderer>();
                mr.material = MatWater;
                if(h.Elevation >= 0)
                {
                    mr.material = MatGrass;
                }
                else
                {
                    mr.material = MatWater;
                }
                //This is the part that deals with the Hex's mesh
                MeshFilter mf = hexObj.GetComponentInChildren<MeshFilter>();
                mf.mesh = MeshWater;
            }
        }
    }
}
