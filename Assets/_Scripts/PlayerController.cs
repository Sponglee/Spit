using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : IncludeManagers
{

   

    public Rigidbody dropRigibody;
    public float dropForceMultiplier = 30f;



    public float rotationSpeed = 1f;
    public Vector2 rotationHorizontalBounds;


    public class FinishEvent : UnityEvent<Transform> { }
    public static FinishEvent FinishTarget = new FinishEvent();


  
    private void Start()
    {

        //FunctionHandler.GameStart.AddListener(ResetDrop);

    }
    

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
            if(Input.GetMouseButton(0))
            {
                gameManager.GameState = GameManager.GameStates.IsFlying;
            }

        }
        else if(gameManager.GameState == GameManager.GameStates.Rest)
        {
            DropDown();
            
        }

    }


    private void MovePlayer()
    {
        //Move a player with rb velocity forward + joystick offsets
        dropRigibody.velocity = (Vector3.forward + Vector3.up * inputManager.input.y +
                               Vector3.right * inputManager.input.x) * dropForceMultiplier;

        //Offset player within a screen
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);



        //Rotate player towards movement
        Vector3 lookDirection = new Vector3(inputManager.input.x, inputManager.input.y / 2f, 1f);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);

    }


    //Fall towards the ground
    private void DropDown()
    {
        //Fall down, adjust rotation
        dropRigibody.velocity = Vector3.up * -dropForceMultiplier;
        Vector3 lookDirection = -Vector3.up + Vector3.forward;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IInteractable>() != null)
        {
            //Make other object interact
            other.gameObject.GetComponent<IInteractable>().Interact(transform.position);

            if(other.CompareTag("Citizen"))
                //Kill bird and point of contact
                KillBird(transform.position);
        }

    }


    public void ResetDrop()
    {
        //dropRigibody.velocity = Vector3.zero;
       
       
       
    }


    public void KillBird(Vector3 point)
    {

        GameObject tmpObject = Instantiate(gameManager.deathEffect, point, Quaternion.identity, transform);
        gameManager.GameState = GameManager.GameStates.NoEnergy;

    }
}
