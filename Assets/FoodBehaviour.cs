using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : IncludeManagers, IInteractable
{
    public void Interact(Transform target)
    {
        if(target.CompareTag("Player"))
        {
            gameManager.GameState = GameManager.GameStates.Rest;
        }
    }
}

