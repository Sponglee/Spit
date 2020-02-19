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


   
    public class FinishEvent:UnityEvent<Transform> {}
    public static FinishEvent FinishTarget = new FinishEvent();

    private void Start()
    {
        energy = maxEnergy;
        FunctionHandler.GameStart.AddListener(ResetDrop);

    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameState == GameManager.GameStates.Fly)
        {
            
            if (Input.GetMouseButton(0))
            {
                if(energy>=0)
                {
                    dropRigibody.velocity = (Vector3.forward /** inputManager.input.y*/ +
                                    Vector3.right * inputManager.input.x) * dropForceMultiplier;

                    transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, 0, transform.localPosition.z), Time.deltaTime);
                    Energy -= energyRate;
                    
                }
                else
                {
                    gameManager.GameState = GameManager.GameStates.NoEnergy;
                }
                
               

            }
            else
            {
                dropRigibody.velocity = Vector3.up * -dropForceMultiplier;

                if(energy<=maxEnergy)
                    Energy += energyRate;
            }
        }
        else if( gameManager.GameState== GameManager.GameStates.NoEnergy)
        {
            dropRigibody.velocity = Vector3.up * -dropForceMultiplier;
            if(Input.GetMouseButtonUp(0))
            {
                GameManager.Instance.GameState = GameManager.GameStates.Fly;
            }
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            
           
            
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
