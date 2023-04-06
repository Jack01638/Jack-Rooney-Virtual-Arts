using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BlockScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Header("Game Objects")]
    public GameObject Cube;
    public GameObject Cylinder;
    public GameObject CubeStatic;
    public GameObject CylinderStatic;
    public GameObject SphereStatic;
    public GameObject Sphere;

    //logic variables
    private string ActiveBlock = "Cube"; //this will hold which block to place depending on button pressed. by default its Cube
    private bool ToggleStatus = true; //toggle between gravity and static blocks, default is dynamic
    private string SelectedFunction = "place"; //default is to place blocks
    bool clicked = false; //helps allow only 1 click of buttons at a time
    bool firstSelect = true; //used to help toggle selected block material
    bool OverUI = false; //check if clicking on UI

    [Header("Object Buttons")]
    public Button CubeButton;
    public Button CylinderButton;
    public Button DeleteButton;
    public Button SelectButton;
    public Button SphereButton;

    [Header("Size Buttons")]
    public Button PlusX;
    public Button MinusX;
    public Button PlusY;
    public Button MinusY;
    public Button PlusZ;
    public Button MinusZ;

    [Header("Text")]
    public TextMeshProUGUI ToggleText;
    public TextMeshProUGUI FunctionText;
    private GameObject SelectedObject;
    private GameObject TempSelectedObject;

    [Header("Materials")]
    public Material SelectedMaterial;
    public Material DefaultBlockMaterial;

    private void Update()
    {

        //detecting where the mouse is
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //moving visual mouse pointer
            transform.position = raycastHit.point;
            //check for left click
            if (Input.GetMouseButtonDown(0))
            {
                //check if clickign on UI
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    OverUI = true;
                    //Debug.Log("UI HIT");
                }
                //Debug.Log("Left Click");
                if (ToggleStatus == true && SelectedFunction == "place" && OverUI == false) //dynamic gravity blocks
                {
                    //if placed on the ground, place where user is looking
                    if (raycastHit.transform.tag == "Ground")
                        switch (ActiveBlock)
                        {
                            case "Cube":
                                Instantiate(Cube, new Vector3(raycastHit.point.x, raycastHit.point.y + 0.5f, raycastHit.point.z), Quaternion.identity);
                                break;
                            case "Cylinder":
                                Instantiate(Cylinder, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                                break;
                            case "Sphere":
                                Instantiate(Sphere, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                                break;
                        }
                    else if (raycastHit.transform.tag != "Ground")//placed on another object
                    {
                        //find center of block face
                        Vector3 position = raycastHit.transform.position + raycastHit.normal;
                        //find block rotation
                        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
                        switch (ActiveBlock)
                        {
                            case "Cube":
                                Cube.transform.position = position;
                                Cube.transform.rotation = rotation;
                                Instantiate(Cube, position, rotation);
                                break;
                            case "Cylinder":
                                Cylinder.transform.position = position;
                                Cylinder.transform.rotation = rotation;
                                Instantiate(Cylinder, position, rotation);
                                break;
                            case "Sphere":
                                Sphere.transform.position = position;
                                Sphere.transform.rotation = rotation;
                                Instantiate(Sphere, position, rotation);
                                break;
                        }
                    }
                }
                if (ToggleStatus == false && SelectedFunction == "place" && OverUI == false) //static non gravity blocks
                {
                    if (raycastHit.transform.tag == "Ground")
                    {
                        switch (ActiveBlock)
                        {
                            case "Cube":
                                Instantiate(CubeStatic, new Vector3(raycastHit.point.x, raycastHit.point.y + 0.5f, raycastHit.point.z), Quaternion.identity);
                                break;
                            case "Cylinder":
                                Instantiate(CylinderStatic, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                                break;
                            case "Sphere":
                                Instantiate(SphereStatic, new Vector3(raycastHit.point.x, raycastHit.point.y + 1, raycastHit.point.z), Quaternion.identity);
                                break;
                        }
                    }
                    else if (raycastHit.transform.tag != "Ground" && OverUI == false)
                    {
                        //find center of block face
                        Vector3 position = raycastHit.transform.position + raycastHit.normal;
                        //find block rotation
                        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
                        switch (ActiveBlock)
                        {
                            case "Cube":
                                CubeStatic.transform.position = position;
                                CubeStatic.transform.rotation = rotation;
                                Instantiate(CubeStatic, position, rotation);
                                break;
                            case "Cylinder":
                                CylinderStatic.transform.position = position;
                                CylinderStatic.transform.rotation = rotation;
                                Instantiate(CylinderStatic, position, rotation);
                                break;
                            case "Sphere":
                                SphereStatic.transform.position = position;
                                SphereStatic.transform.rotation = rotation;
                                Instantiate(SphereStatic, position, rotation);
                                break;
                        }
                    }
                }

                //deleting blocks
                if (raycastHit.collider.tag == "Block" && SelectedFunction == "delete" && OverUI == false)
                {
                    //string objectname = raycastHit.collider.gameObject.name;
                    //Debug.Log(objectname);
                    Destroy(raycastHit.transform.gameObject);
                }

                //selecting blocks
                if (raycastHit.collider.tag == "Block" && SelectedFunction == "select" && OverUI == false)
                {

                    //string objectname = raycastHit.collider.gameObject.name;
                    //Debug.Log(firstSelect);
                    
                    if (firstSelect == true) //if its first time selected, just set to selected block
                    {
                        SelectedObject = raycastHit.transform.gameObject;
                        SelectedObject.GetComponent<MeshRenderer>().material = SelectedMaterial;
                        TempSelectedObject = SelectedObject;
                        firstSelect = false;
                    }

                    else if (firstSelect == false) //once TempSelectedObject is given a value:
                    {
                        SelectedObject = raycastHit.transform.gameObject;
                        TempSelectedObject.GetComponent<MeshRenderer>().material = DefaultBlockMaterial;
                        SelectedObject.GetComponent<MeshRenderer>().material = SelectedMaterial;
                        TempSelectedObject = SelectedObject;
                    }
                }

            }
        }

        //toggle static/dynamic(gravity) blocks
        if (Input.GetKeyDown("q"))
        {
           // Debug.Log("q pressed");
            Toggle(ToggleStatus);
        }
       
        //Buttons
        CubeButton.onClick.AddListener(() => CubeButtonPress());
        CylinderButton.onClick.AddListener(() => CylinderButtonPress());
        SphereButton.onClick.AddListener(() => SphereButtonPress());
        DeleteButton.onClick.AddListener(() => DeleteButtonPress());
        SelectButton.onClick.AddListener(() => SelectButtonPress());

        PlusX.onClick.AddListener(() => Scale(SelectedObject, "X","P"));
        MinusX.onClick.AddListener(() => Scale(SelectedObject, "X", "N"));
        PlusY.onClick.AddListener(() => Scale(SelectedObject, "Y", "P"));
        MinusY.onClick.AddListener(() => Scale(SelectedObject, "Y", "N"));
        PlusZ.onClick.AddListener(() => Scale(SelectedObject, "Z", "P"));
        MinusZ.onClick.AddListener(() => Scale(SelectedObject, "Z", "N"));

        clicked = false; //Reset clicked value to allow future button presses
        OverUI = false; //reset OverUI
        
    }

    //button press functions
    void CubeButtonPress()
    {
        ActiveBlock = "Cube";
        SelectedFunction = "place";
        FunctionText.text = "Selected\nFunction:\nPlace Cube";
    }

    void CylinderButtonPress()
    {
        ActiveBlock = "Cylinder";
        SelectedFunction = "place";
        FunctionText.text = "Selected\nFunction:\nPlace Tube";
    }

    void SphereButtonPress()
    {
        ActiveBlock = "Sphere";
        SelectedFunction = "place";
        FunctionText.text = "Selected\nFunction:\nPlace Sphere";
    }


    void DeleteButtonPress()
    {
        SelectedFunction = "delete";
        FunctionText.text = "Selected\nFunction:\nDelete";
    }

    void SelectButtonPress()
    {
        SelectedFunction = "select";
        FunctionText.text = "Selected\nFunction:\nSelect";
    }

    void Toggle(bool ToggleStatuscheck) //toggle between static/dynamic(gravity) blocks
    {
        if (ToggleStatuscheck == true) //static
        {
            //Debug.Log("set to static");
            ToggleText.text = "Current Toggle:\nStatic";
            ToggleStatus = false;
        }

        if (ToggleStatuscheck == false) //dynamic blocks
        {
            //Debug.Log("set to dynamic");
            ToggleText.text = "Current Toggle:\nDynamic";
            ToggleStatus = true;
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
                    newScale.x += 0.5f;
                }
                if (sign == "N")
                {
                    newScale.x -= 0.5f;
                }
            }

            if (axis == "Y")
            {
                if (sign == "P")
                {
                    newScale.y += 0.5f;
                }
                if (sign == "N")
                {
                    newScale.y -= 0.5f;
                }
            }

            if (axis == "Z")
            {
                if (sign == "P")
                {
                    newScale.z += 0.5f;
                }
                if (sign == "N")
                {
                    newScale.z -= 0.5f;
                }
            }

            tempObject.transform.localScale = newScale;

            //end here
            clicked = true;
            
        }
        
    }
    
}
