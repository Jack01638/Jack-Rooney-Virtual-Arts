using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDrag : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZ;
    public GameObject Pointer;

    void OnMouseDown()
    { 
        //keep object same distance from player
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //move object with mouse pointer
        mouseOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    { 
        //pixel coordinates of mouse
        Vector3 mousePoint = Input.mousePosition;
        //z coordinate of game object on screen
        mousePoint.z = mouseZ;
        //convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        //stop it going through the floor
        float pointer_y = Pointer.transform.position.y;
        if (pointer_y > 0.3f) //only move if above ground and not through floor (blocks can be up to 0.5 small)
        {
            transform.position = GetMouseAsWorldPoint() + mouseOffset;
        } 
    }
}
