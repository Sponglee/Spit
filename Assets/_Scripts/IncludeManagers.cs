using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IncludeManagers : MonoBehaviour
{
    //Interface that contains references to gameManager, inputManager

    protected InputManager inputManager;
    protected GameManager gameManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        gameManager = GameManager.Instance;
    }
}
