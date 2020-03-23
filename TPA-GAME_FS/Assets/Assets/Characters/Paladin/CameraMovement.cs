using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;
    float nowFacing;
 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CameraControl();
    }

    void CameraControl()
    {

        if(PauseGame.isPaused == false)
        {
            mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 40);

            transform.LookAt(Target);

            Target.rotation = Quaternion.Slerp(Target.rotation, Quaternion.Euler(mouseY, mouseX, 0), Time.deltaTime * 10f);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Player.rotation = Quaternion.Euler(0, mouseX, 0);
                nowFacing = mouseX;
            }
            else
            {
                Player.rotation = Quaternion.Euler(0, nowFacing, 0);
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Player.rotation = Quaternion.Euler(0, nowFacing, 0);
        }
    }
}
