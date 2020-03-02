using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : IncludeManagers, IInteractable
{
    public void Interact(Vector3 point)
    {
        gameManager.GameState = GameManager.GameStates.Rest;
      
    }
}

