using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    private bool escPressed = false;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //in update, set this to unlocked and visable when esc toggled to access UI
    }

    // Update is called once per frame
    private void Update()
    {
        if (escPressed == false)
        {
            //rotating camera with cursor
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            //make rotation changes
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //not been pressed yet, game active and mouse locked
            if (escPressed == false)
            {
                Debug.Log("Menu Open");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                escPressed = true;
            }
            //mouse unlocked then:
            else if (escPressed == true)
            {
                Debug.Log("Menu Closed");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                escPressed = false;
            }

        }
    }
}
