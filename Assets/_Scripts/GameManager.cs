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

           
            Debug.Log(value);
            gameState = value;
            CameraManager.Instance.SetLive(value);
            FunctionHandler.Instance.SetActiveCanvas(value);
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
        GameState = GameStates.Paused;
        UpdateScore.AddListener(CalculateScore);

    }

    private void Update()
    {
        if(GameState == GameStates.LevelStarted)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameState = GameStates.IsFlying;
            }
        }
    }

    private void CalculateScore(int value)
    {
        if (GameState == GameStates.IsFlying)
            Score += value;
    }
    
}
 