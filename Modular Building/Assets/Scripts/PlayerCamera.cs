using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    private void Start()
    {
        //Cursor.lockState = CursorLockMove.Locked;
        //Cursor.Visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //rotating camera with cursor
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //make rotation changes
        yRotation += mouseX;
        xRotation -= mouseY;

        //
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
