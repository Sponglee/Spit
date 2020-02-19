using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropController : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private CameraManager cameraManager;


    public Rigidbody dropRigibody;
    public float dropForceMultiplier = 30f;

    public class FinishEvent:UnityEvent<Transform> {}
    public static FinishEvent FinishTarget = new FinishEvent();

    private void Start()
    {
        FunctionHandler.GameStart.AddListener(ResetDrop);

    }
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
            
            ResetDrop();
            
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Target"))
        {
          
            GameManager.Instance.GameState = GameManager.GameStates.Finish;
            FinishTarget?.Invoke(other.transform);
            gameObject.SetActive(false);
            other.GetComponent<CitizenBehaviour>().enabled = false;
            //ResetDrop();
        }
    }

    public void ResetDrop()
    {
        dropRigibody.velocity = Vector3.zero;
        transform.localPosition = Vector3.zero;
        dropRigibody.isKinematic = true;
        gameObject.SetActive(false);
    }
}
