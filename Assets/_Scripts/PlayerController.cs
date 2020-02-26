using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : IncludeManagers
{

   
    //[SerializeField] private CameraManager cameraManager;



    public Rigidbody dropRigibody;
    public float dropForceMultiplier = 30f;



    public float rotationSpeed = 1f;
    public Vector2 rotationHorizontalBounds;


    public class FinishEvent : UnityEvent<Transform> { }
    public static FinishEvent FinishTarget = new FinishEvent();

    private void Start()
    {

        FunctionHandler.GameStart.AddListener(ResetDrop);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.GameState == GameManager.GameStates.IsFlying)
        {

            if (Input.GetMouseButton(0))
            {
                MovePlayer();
            }
        }
        else if (gameManager.GameState == GameManager.GameStates.NoEnergy)
        {
            DropDown();
        }
        else if (gameManager.GameState == GameManager.GameStates.CanFly)
        {
            if (Input.GetMouseButton(0))
            {
                gameManager.GameState = GameManager.GameStates.IsFlying;

            }
        }

    }

    private void MovePlayer()
    {
        dropRigibody.velocity = (Vector3.forward + Vector3.up * inputManager.input.y +
                               Vector3.right * inputManager.input.x) * dropForceMultiplier;

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);



        Vector3 lookDirection = new Vector3(inputManager.input.x, inputManager.input.y / 2f, 1f);

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }


    private void DropDown()
    {
        dropRigibody.velocity = Vector3.up * -dropForceMultiplier;
        Vector3 lookDirection = -Vector3.up + Vector3.forward;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }

    private void OnCollisionEnter(Collision collision)
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Food"))
        {

            gameManager.GameState = GameManager.GameStates.Rest;

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
