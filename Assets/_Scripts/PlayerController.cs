using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private CameraManager cameraManager;


    public Rigidbody dropRigibody;
    public Transform droplet;

    public float forceMultiplier = 1f;
    public float dropForceMultiplier = 1f;

    // Update is called once per frame
    void Update()
    {
       
        if(gameManager.GameState == GameManager.GameStates.Player && Input.GetMouseButtonUp(0))
        {

            dropRigibody.isKinematic = false;
            droplet.gameObject.SetActive(true);

            dropRigibody.AddForce((Vector3.forward * -inputManager.input.x
                                   - Vector3.up * inputManager.input.x
                                   - Vector3.right * inputManager.input.y) * forceMultiplier);

            Debug.Log("HERE");

            gameManager.GameState = GameManager.GameStates.Drop;
            
        }
        else if(gameManager.GameState == GameManager.GameStates.Drop && Input.GetMouseButton(0))
        {
             dropRigibody.AddForce((Vector3.forward * inputManager.input.x 
                                    + Vector3.right * inputManager.input.y)*dropForceMultiplier);
        }
    }
}
