using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public GameObject Cube;
    public GameObject Cylinder;
    public GameObject CubeStatic;
    public GameObject CylinderStatic;
    private string ActiveBlock = "Cube"; //this will hold which block to place depending on button pressed. by default its Cube
    private bool ToggleStatus = true;
    private string SelectedFunction = "place"; //default is to place blocks

    public Button CubeButton;
    public Button CylinderButton;
    public Button DeleteButton;
    public Button SelectButton;

    public Button PlusX;
    public Button MinusX;
    public Button PlusY;
    public Button MinusY;
    public Button PlusZ;
    public Button MinusZ;

    public TextMeshProUGUI ToggleText;
    private GameObject SelectedObject;

    bool clicked = false;

    private void Update()
    {
        //check if clicking on any UI objects or not
        bool OverUI = false;
        //detecting where mouse is
        //Debug.Log(Input.mousePosition);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //moving visual mouse pointer
            transform.position = raycastHit.point;
            //check for left click
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Left Click");
                if (ToggleStatus == true && SelectedFunction == "place" && OverUI == false)
                {
                    switch (ActiveBlock)
                    {
                        case "Cube":
                            Instantiate(Cube, new Vector3(raycastHit.point.x, raycastHit.point.y + 0.5f, raycastHit.point.z), Quaternion.identity);
                            break;
                        case "Cylinder":
                            Instantiate(Cylinder, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                            break;
                    }
                }
                if (ToggleStatus == false && SelectedFunction == "place")
                {
                    switch (ActiveBlock)
                    {
                        case "Cube":
                            Instantiate(CubeStatic, new Vector3(raycastHit.point.x, raycastHit.point.y + 0.5f, raycastHit.point.z), Quaternion.identity);
                            break;
                        case "Cylinder":
                            Instantiate(CylinderStatic, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                            break;
                    }
                }
                //deleting blocks
                if (raycastHit.collider.tag == "Block" && SelectedFunction == "delete")
                {
                    string objectname = raycastHit.collider.gameObject.name;
                    Debug.Log(objectname);
                    Destroy(raycastHit.transform.gameObject);
                }

                if (raycastHit.collider.tag == "Block" && SelectedFunction == "select")
                {
                    string objectname = raycastHit.collider.gameObject.name;
                    Debug.Log(objectname);
                    SelectedObject = raycastHit.transform.gameObject;
    
                   // Vector3 newScale = SelectedObject.transform.localScale;
                    //newScale.x += 2f;
                   // SelectedObject.transform.localScale = newScale;
                }

            }
        }

        if (Input.GetKeyDown("q"))
        {
            Debug.Log("q pressed");
            Toggle(ToggleStatus);
        }
       

        CubeButton.onClick.AddListener(() => CubeButtonPress());
        CylinderButton.onClick.AddListener(() => CylinderButtonPress());
        DeleteButton.onClick.AddListener(() => DeleteButtonPress());
        SelectButton.onClick.AddListener(() => SelectButtonPress());

        
        PlusX.onClick.AddListener(() => Scale(SelectedObject, "X","P"));
        MinusX.onClick.AddListener(() => Scale(SelectedObject, "X", "N"));
        PlusY.onClick.AddListener(() => Scale(SelectedObject, "Y", "P"));
        MinusY.onClick.AddListener(() => Scale(SelectedObject, "Y", "N"));
        PlusZ.onClick.AddListener(() => Scale(SelectedObject, "Z", "P"));
        MinusZ.onClick.AddListener(() => Scale(SelectedObject, "Z", "N"));

        clicked = false;
        
    }

    //button press functions
    void CubeButtonPress()
    {
        ActiveBlock = "Cube";
        SelectedFunction = "place";
    }

    void CylinderButtonPress()
    {
        ActiveBlock = "Cylinder";
        SelectedFunction = "place";
    }

    void DeleteButtonPress()
    {
        SelectedFunction = "delete";
    }

    void SelectButtonPress()
    {
        SelectedFunction = "select";
    }

    void Toggle(bool ToggleStatuscheck)
    {
       // switch(ToggleStatuscheck)
        //true = dynamic blocks
        if (ToggleStatuscheck == true)
        {
            ToggleStatus = false;
            Debug.Log("set to static");
            ToggleText.text = "Current Toggle: Static";
        }
        //false = static blocks
        if (ToggleStatuscheck == false)
        {
            ToggleStatus = true;
            Debug.Log("set to dynamic");
            ToggleText.text = "Current Toggle: Dynamic";
        }
    }

    //pass in the object being scaled, which axis to scale on and if its +ve or -ve
    
    void Scale(GameObject tempObject, string axis, string sign)
    {
        if (!clicked)
        {
            Vector3 newScale = tempObject.transform.localScale;
            if (axis == "X")
            {
                if (sign == "P")
                {
                    newScale.x += 1.0f;
                }
                if (sign == "N")
                {
                    newScale.x -= 1.0f;
                }
            }

            if (axis == "Y")
            {
                if (sign == "P")
                {
                    newScale.y += 1.0f;
                }
                if (sign == "N")
                {
                    newScale.y -= 1.0f;
                }
            }

            if (axis == "Z")
            {
                if (sign == "P")
                {
                    newScale.z += 1.0f;
                }
                if (sign == "N")
                {
                    newScale.z -= 1.0f;
                }
            }

            tempObject.transform.localScale = newScale;

            //end here
            clicked = true;
            
        }
        
    }
    
}
