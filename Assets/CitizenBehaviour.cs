using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBehaviour : IncludeManagers, IInteractable
{
    [SerializeField] private Vector2 speedRange;
    private float speed; 
    private float routeDuration = 5f;
    private float timer = 0f;

    private Vector3 startPos;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        speed = Random.Range(speedRange.x, speedRange.y);
        routeDuration = Random.Range(5f, 10f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = Vector3.forward * speed;
        transform.Translate(Vector3.forward * speed);


    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Respawn"))
        {
            transform.position = startPos;
        }
    }

    public void Interact(Transform target)
    {
        GameObject tmpObject = Instantiate(gameManager.poopPref, target.position, Quaternion.identity, target);
    }
}
