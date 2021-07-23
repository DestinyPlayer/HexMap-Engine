using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is basically to define all the data about a singular hex itself

//Column + Row + S = 0
//S = -(Column + Row)
public class Hex
{
    public readonly int column; //Column
    public readonly int row;    //Row
    public readonly int s;

    static readonly float HEIGHT_MATH = Mathf.Sqrt(3) / 2;

    private readonly float radius = 1f;

    public Hex(int column, int row) //Column and Row info
    {
        this.column = column;
        this.row = row;
        this.s = -(column + row);
    }

    public Vector3 Position() //Returns the position of this hex in the world
    {
        return new Vector3(
            HexHorizontalSpacing() * this.row,
            0,
            HexVerticalSpacing() * (this.column + this.row / 2f)
            );
    }

    public float HexWidth()
    {
        return radius * 2;
    }

    public float HexHeight()
    {
        return HEIGHT_MATH * HexWidth();
    }

    public float HexHorizontalSpacing()
    {
        return HexWidth() * 0.75f;
    }

    public float HexVerticalSpacing()
    {
        return HexHeight();
    }

    public Vector3 PosFromCamera(float numRows, float numColumns, Vector3 cameraPosition)
    {
        float mapWidth  = numColumns * HexHorizontalSpacing();
        float mapHeight = numRows    * HexVerticalSpacing();

        Vector3 curPos = Position();

        //Aka how many hexes away from camera view is this hex
        //Preferrably between -0.5 and 0.5
        float widthsFromCamera  = (curPos.x - cameraPosition.x) / mapWidth;
        float heightsFromCamera = (curPos.z - cameraPosition.z) / mapHeight;
        
        /*if (Mathf.Abs(widthsFromCamera) <= 0.5f)
        {
            return Position(); //Aka it's within this range
        }*/

        // 0.6 => -0.4
        // 2.2 =>  0.2
        // 2.8 =>  0.2
        // 2.6 => -0.4
        //-0.5 =>  0.4
        //West to East
            if (widthsFromCamera > 0)
            {
                widthsFromCamera += 0.5f;
            }
            else
            {
                widthsFromCamera -= 0.5f;
            }

            int widthsToFix = (int)widthsFromCamera;

            curPos.x -= widthsToFix * mapWidth;

        //North to South
        //if (HexMap.VerticalWrap == true )
        //{
            if (heightsFromCamera > 0)
            {
                heightsFromCamera += 0.5f;
            }
            else
            {
                heightsFromCamera -= 0.5f;
            }

            int heightsToFix = (int)heightsFromCamera;

            curPos.z -= heightsToFix * mapHeight;
        //}
        
        return curPos;
    }

}
