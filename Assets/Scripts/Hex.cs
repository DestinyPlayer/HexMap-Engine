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

    public Hex(int column, int row) //Column and Row info
    {
        this.column = column;
        this.row = row;
        this.s = -(column + row);
    }

    public Vector3 Position() //Returns the position of this hex in the world
    {
        float radius = 1f;
        float width  = radius * 2;
        float height = HEIGHT_MATH * width;

        float verticalSpacing   = height;
        float horizontalSpacing = width * 0.75f;
        return new Vector3(
            horizontalSpacing * this.row,
            0,
            verticalSpacing * (this.column + this.row / 2f)
            );
    }

}
