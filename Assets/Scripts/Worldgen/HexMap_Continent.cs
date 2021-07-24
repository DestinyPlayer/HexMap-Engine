using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    override public void GenerateMap()
    {
        //Make sure to generate all the hexes first
        base.GenerateMap();

        //Some kind of raised area
        ElevateArea(0, 0, 3);

        //Add lumpiness

        //Set mesh to mountain/hill/flat/water based on height

        //Simulate rain/moisture and set grass/desert/snow

        //Now update all of the visuals
        UpdateHexVisuals();
    }

    void ElevateArea(int q, int r, int radius)
    {
        Hex centerHex = GetHex(q, r);

        //centerHex.Elevation = 0.5f;

        Hex[] areaHexes = GetHexesWithinRadius(centerHex, radius);
        
        foreach(Hex h in areaHexes)
        {
            h.Elevation = 0.5f;
        }

    }
}
