using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        oldPosition = this.transform.position;
    }

    Vector3 oldPosition;

    // Update is called once per frame
    void Update()
    {
        // TODO: Moving Camera with mouse
        // TODO: Moving Camera with WASD
        // TODO: Zoom in and out

        CheckIfCameraMoved();
    }
    public void MoveToHex(Hex hex)
    {
        //TODO: Move Camera to specific Hex
    }

    void CheckIfCameraMoved()
    {
        if(oldPosition != this.transform.position)
        {
            //Camera moved!
            oldPosition = this.transform.position;

            //Make sure that the camera values don't overflow
            //Doesn't work
            /*if (oldPosition.x >= 30)
            {
                oldPosition.x = oldPosition.x - 30;
            }
            else if (oldPosition.x <= -30) {
                oldPosition.x = oldPosition.x + 30;
            }
            if (oldPosition.z >= 40)
            {
                oldPosition.z = oldPosition.z - 40;
            }
            else if (oldPosition.z <= -40)
            {
                oldPosition.z = oldPosition.z + 40;
            }*/

            //HexMap will later store this
            HexBehaviour[] hexes = GameObject.FindObjectsOfType<HexBehaviour>();

            foreach(HexBehaviour hex in hexes)
            {
                hex.UpdatePos();
            }
        }
    }   
}
