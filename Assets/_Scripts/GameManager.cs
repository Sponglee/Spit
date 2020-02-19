using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GameManager : Singleton<GameManager>
{

    public enum GameStates
    {
        Paused,
        Player,
        Drop,
        Finish
    };


    [SerializeField] private GameStates gameState;
    public GameStates GameState
    {
        get
        {
            return gameState;
        }

        set
        {

            switch (value)
            {
                case GameStates.Paused:
                    
                    break;
                case GameStates.Player:
                   
                    break;
                case GameStates.Drop:
                   
                    break;
                case GameStates.Finish:
                   
                    break;
                default:
                    break;
            }
            Debug.Log(value);
            gameState = value;
            CameraManager.Instance.SetLive(value);
        }
    }

    private void Start()
    {
        GameState = GameStates.Paused;


    }

    
}
 