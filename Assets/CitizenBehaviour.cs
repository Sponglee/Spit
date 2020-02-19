using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 speedRange;
    private float speed; 
    private float routeDuration = 5f;
    private float timer = 0f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        speed = Random.Range(speedRange.x, speedRange.y);
        routeDuration = Random.Range(5f, 10f);
    }
    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.forward * speed);
        
      
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Respawn"))
        {
            transform.position = startPos;
        }
    }

}
