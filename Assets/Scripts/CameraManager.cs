using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public float sensitivity;
    float cameraVerticalRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // got help form YT video for FPS camera movement
        
        // Get input for the X and Y axis and mult by the sensitivity the user has chosen
        float inputX = Input.GetAxis("Mouse X") * sensitivity;
        float inputY = Input.GetAxis("Mouse Y") * sensitivity;



        // able to rotate along the x axis
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right*cameraVerticalRotation;

        // the player will rotate around the y axis to look around horizontally
        player.Rotate(Vector3.up * inputX);
    }
}
