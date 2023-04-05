using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public GameObject Cube;
    public GameObject Cylinder;
    private string ActiveBlock = "Cube"; //this will hold which block to place depending on button pressed. by default its Cube

    public Button CubeButton;
    public Button CylinderButton;
    public Button DeleteButton;
    public Button SelectButton;

    private void Update()
    {
        //detecting where mouse is
        //Debug.Log(Input.mousePosition);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //moving visual mouse pointer
            transform.position = raycastHit.point;
        }
        //check for left click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Click");
            switch(ActiveBlock)
            {
                case "Cube":
                    Instantiate(Cube, new Vector3(raycastHit.point.x,raycastHit.point.y + 1,raycastHit.point.z), Quaternion.identity);
                    break;
                case "Cylinder":
                    Instantiate(Cylinder, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                    break;
            }
        }

        CubeButton.onClick.AddListener(() => ActiveBlock = "Cube");
        //CubeButton.onClick.AddListener(() => Debug.Log("Button Down"));
        CylinderButton.onClick.AddListener(() => ActiveBlock = "Cylinder");


    }
}
