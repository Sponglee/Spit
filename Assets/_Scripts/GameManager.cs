using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GameManager : Singleton<GameManager>
{

    public enum GameStates
    {
        Paused,
        LevelStarted,
        CanFly,
        IsFlying,
        NoEnergy,
        Rest,
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
                case GameStates.LevelStarted:
                   
                    break;
                case GameStates.CanFly:
                   
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

    [Header ("")]
    public GameObject poopPref;




    private void Start()
    {
        GameState = GameState;


    }

    
}
 