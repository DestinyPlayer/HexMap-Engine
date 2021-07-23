using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBehaviour : MonoBehaviour
{
    public Hex Hex;
    public HexMap HexMap;
    public void UpdatePos()
    {
        this.transform.position = Hex.PosFromCamera(
            HexMap.mapWidth,
            HexMap.mapHeight,
            Camera.main.transform.position
            );
    }
}
