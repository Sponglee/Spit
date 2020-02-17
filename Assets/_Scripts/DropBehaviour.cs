using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            GameManager.Instance.GameState = GameManager.GameStates.Paused;
            transform.localPosition = Vector3.zero;
        }
    }
}
