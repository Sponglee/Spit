using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IManagable : MonoBehaviour
{
    //Interface that contains references to gameManager, inputManager

    public InputManager inputManager;
    public GameManager gameManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        gameManager = GameManager.Instance;
    }
}
