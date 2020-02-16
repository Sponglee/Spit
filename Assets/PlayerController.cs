using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CameraManager cameraManager;

    public Joystick joystick;

    public Vector2 joystickInput;
    public Rigidbody dropRigibody;
    public Transform droplet;

    public float forceMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            joystickInput = new Vector2(joystick.Vertical, joystick.Horizontal);
            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            dropRigibody.isKinematic = false;
            droplet.gameObject.SetActive(true);
            cameraManager.SetLive("DropCam");

            dropRigibody.AddForce((Vector3.forward*(-joystickInput.x) + -Vector3.right*joystickInput.y)*forceMultiplier);
        }
    }
}
