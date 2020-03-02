using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopBehaviour : IncludeManagers
{
    [SerializeField] private float poopSpeed = 1f;

    [SerializeField]private Transform poopTarget = null;
    public Transform PoopTarget
    {
        get
        {
            return poopTarget;
        }

        set
        {
            poopTarget = value;
        }
    }

    //Drop down
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * poopSpeed);
        //transform.Translate((-transform.position+poopTarget.position).normalized*poopSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Citizen"))
        {
            //Update score based on citizen's score value
            GameManager.UpdateScore.Invoke(other.GetComponent<CitizenBehaviour>().CitizenScoreValue);

            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                //Make other target interact
                other.gameObject.GetComponent<IInteractable>().Interact(transform.position);
            }


            Destroy(gameObject);
        }
    }



   
}
