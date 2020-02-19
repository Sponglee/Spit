using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private CameraManager cameraManager;


    public Rigidbody dropRigibody;
    public float dropForceMultiplier = 30f;

  
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameState == GameManager.GameStates.Drop)
        {
            if (Input.GetMouseButton(0))
            {
               
                dropRigibody.AddForce((Vector3.forward * inputManager.input.y
                                    + Vector3.right * inputManager.input.x) * dropForceMultiplier);

            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            GameManager.Instance.GameState = GameManager.GameStates.Player;
            dropRigibody.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            dropRigibody.isKinematic = true;
            gameObject.SetActive(false);
        }
    }
}
