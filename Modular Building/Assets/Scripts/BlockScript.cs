using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public GameObject Cube;
    public GameObject Cylinder;

    private void Update()
    {
        Debug.Log(Input.mousePosition);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
        //check for left click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Click");
            Instantiate(Cube, new Vector3(raycastHit.point.x,raycastHit.point.y + 1,raycastHit.point.z), Quaternion.identity);
        }

    }
}
