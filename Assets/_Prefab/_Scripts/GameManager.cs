using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




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
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            UpdateUIText.Invoke(value);
        }
    }

    [SerializeField] private GameStates gameState;
    [SerializeField] private int score = 0;
   
    [Header ("")]
    public GameObject poopPref;
    public GameObject deathEffect;

    public class UpdateUIEvent : UnityEvent<int> { }
    public static UpdateUIEvent UpdateUIText = new UpdateUIEvent();

    public class UpdateScoreEvent : UnityEvent<int> { }
    public static UpdateScoreEvent UpdateScore = new UpdateScoreEvent();


    private void Start()
    {
        GameState = GameState;
        UpdateScore.AddListener(CalculateScore);

    }


    private void CalculateScore(int value)
    {

    }
    
}
 