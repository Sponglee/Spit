using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnegryChangeEvent : UnityEvent<float>{ };

public class DropController : MonoBehaviour
{
    public static EnegryChangeEvent OnEnergyChange = new EnegryChangeEvent();

    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private CameraManager cameraManager;


    public Rigidbody dropRigibody;
    public float dropForceMultiplier = 30f;

    [SerializeField] private float energyRate = 1;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float energy;
    public float Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
            OnEnergyChange?.Invoke(value/maxEnergy);
        }
    }

    public float rotationSpeed = 1f;
    public Vector2 rotationHorizontalBounds;


    public class FinishEvent:UnityEvent<Transform> {}
    public static FinishEvent FinishTarget = new FinishEvent();

    private void Start()
    {
        energy = maxEnergy;
        FunctionHandler.GameStart.AddListener(ResetDrop);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.GameState == GameManager.GameStates.Fly)
        {
            
            if (Input.GetMouseButton(0))
            {
                if(energy>=0)
                {
                    dropRigibody.velocity = (Vector3.forward + Vector3.up * inputManager.input.y +
                                    Vector3.right * inputManager.input.x) * dropForceMultiplier;

                    transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);

                    

                    Vector3 lookDirection = new Vector3(inputManager.input.x, inputManager.input.y / 2f, 1f);

                    Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                    float step = rotationSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);



                    Energy -= energyRate* (Mathf.Abs(inputManager.input.x)+Mathf.Abs(inputManager.input.y));
                }
                else
                {
                    gameManager.GameState = GameManager.GameStates.NoEnergy;
                }
                
               

            }
            else
            {
                DropDown();

            }
        }
        else if( gameManager.GameState== GameManager.GameStates.NoEnergy)
        {
            DropDown();

            if (energy >= maxEnergy || Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.GameState = GameManager.GameStates.Fly;

            }
            else if(energy<maxEnergy)
            {
                Energy += energyRate * 5f;
            }
        }
        
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
        if (collision.transform.CompareTag("Ground"))
        {

            gameManager.GameState = GameManager.GameStates.NoEnergy;

            //ResetDrop();

        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.CompareTag("Target"))
        //{
          
        //    GameManager.Instance.GameState = GameManager.GameStates.Finish;
        //    FinishTarget?.Invoke(other.transform);
        //    gameObject.SetActive(false);
        //    other.GetComponent<CitizenBehaviour>().enabled = false;
        //    //ResetDrop();
        //}
    }

    public void ResetDrop()
    {
        dropRigibody.velocity = Vector3.zero;
        transform.localPosition = Vector3.zero;
        dropRigibody.isKinematic = true;
        gameObject.SetActive(false);
    }
}
