using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private CameraManager cameraManager;

    public bool CanDrop = false;

    public Rigidbody dropRigibody;
    public Transform droplet;

    public float forceMultiplier = 1f;
    public float rotationSpeed = 1f;

    public Vector2 rotationHorizontalBounds;

    // Update is called once per frame
    void Update()
    {
        if(gameManager.GameState == GameManager.GameStates.Player)
        {
            if(Input.GetMouseButtonDown(0))
            {
                CanDrop = true;
            }
            else if (Input.GetMouseButton(0))
            {

                Vector3 moveVector = (Vector3.up * inputManager.input.x/* + Vector3.left * inputManager.input.Vertical*/);
                if (inputManager.input.x != 0 || inputManager.input.y != 0)
                {
                    Vector3 lookDirection = new Vector3(-inputManager.input.x, -inputManager.input.y/5f, 1f);
                    Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                    float step = rotationSpeed *Time.deltaTime;
                    transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);


                    //transform.eulerAngles += Vector3.up * Mathf.Clamp((Mathf.Atan(inputManager.input.x) * Mathf.Rad2Deg * rotationSpeed *Time.deltaTime),rotationHorizontalBounds.x,rotationHorizontalBounds.y);
                   
                    //transform.eulerAngles
                    //   += Vector3.right * Mathf.Atan(joystick.Vertical) * Mathf.Rad2Deg * speed *
                    //   Time.deltaTime;
                }

            }
            else if (CanDrop && Input.GetMouseButtonUp(0))
            {
                dropRigibody.isKinematic = false;
                droplet.gameObject.SetActive(true);

                dropRigibody.AddForce((Vector3.forward * -inputManager.input.y
                                        - Vector3.up * inputManager.input.y
                                        - Vector3.right * inputManager.input.x) * forceMultiplier);

                CanDrop = false;
                transform.rotation = Quaternion.identity;
                gameManager.GameState = GameManager.GameStates.Fly;
            }
        }
        
    }
}
