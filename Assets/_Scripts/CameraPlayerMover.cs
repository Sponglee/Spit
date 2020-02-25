using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerMover : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    private CinemachineTransposer cameraRef;

    [SerializeField]private float inputModifier=5f;
    [SerializeField]private Vector2 bounds;



    private void Start()
    {
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
        cameraRef = gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameState == GameManager.GameStates.LevelStarted)
        {
            if (Input.GetMouseButton(0))
            {
                    cameraRef.m_FollowOffset.z = Mathf.Clamp(cameraRef.m_FollowOffset.z - inputManager.input.y*inputModifier, bounds.x, bounds.y);
                    //cameraRef.m_FollowOffset.x = Mathf.Clamp(cameraRef.m_FollowOffset.x + inputManager.input.x * inputModifier, -0.5f, 0.5f);
            }
        }
    }


      
}
