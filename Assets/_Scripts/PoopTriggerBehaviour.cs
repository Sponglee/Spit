using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopTriggerBehaviour : IncludeManagers
{

    [SerializeField] Transform playerReference;

    [SerializeField] private float coolDownTimer = 0f;
    [SerializeField] private float interactionCoolDown = 0.2f;
    [SerializeField] private bool onCoolDown = false;
    public bool OnCoolDown
    {
        get
        {
            return onCoolDown;
        }
        set
        {
            onCoolDown = value;
            if (value == false)
            {
                CancelInvoke(nameof(TickTimer));
                coolDownTimer = 0f;
            }
            else
            {
                InvokeRepeating(nameof(TickTimer), 0, Time.fixedDeltaTime);
            }
        }
    }

    //Timer for CoolDown checks
    private void TickTimer()
    {
        coolDownTimer += Time.fixedDeltaTime;
        if (coolDownTimer > interactionCoolDown)
            OnCoolDown = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Citizen") && !OnCoolDown)
        {
            OnCoolDown = true;
            ShootPoop(other.transform);
        }

    }

    //Move with player's rigidbody
    private void FixedUpdate()
    {
        transform.position = playerReference.position;
    }

    //Shoot when triggered an interactable
    private void ShootPoop(Transform target)
    {
        GameObject tmpObject = Instantiate(gameManager.poopPref, playerReference.position, Quaternion.identity, target);
        tmpObject.GetComponent<PoopBehaviour>().PoopTarget = target;
    }
}
