using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameStartEvent : UnityEvent { };
public class MenuOpenEvent : UnityEvent<bool> { };

public class FunctionHandler : Singleton<FunctionHandler>
{
    public static GameStartEvent GameStart = new GameStartEvent();
    public static MenuOpenEvent MenuOpenEvent = new MenuOpenEvent();


    public GameObject menuCanvas;
    public GameObject uiCanvas;
    public GameObject gameOverCanvas;

    public GameObject[] canvases;

    private void Awake()
    {
        canvases = GameObject.FindGameObjectsWithTag("Canvas");
    }

    public void ToggleMenu(bool toggle)
    {
        StartCoroutine(ToggleMenuDelay(toggle));
    }


    public IEnumerator ToggleMenuDelay(bool toggle)
    {
        //MenuOpenEvent?.Invoke(toggle);
       
        if (toggle == true)
        {

            yield return null;
            GameManager.Instance.GameState = GameManager.GameStates.Paused;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            GameManager.Instance.GameState = GameManager.GameStates.LevelStarted;
            GameStart?.Invoke();
        }
    }


    public void SetActiveCanvas(GameManager.GameStates targetState)
    {
        GameObject targetCanvas = null;

        switch (targetState)
        {
            case GameManager.GameStates.Paused:
                {
                    targetCanvas = menuCanvas;
                }
                break;
            case GameManager.GameStates.LevelStarted:
                {
                    targetCanvas = uiCanvas;
                }
                break;
            case GameManager.GameStates.CanFly:
                {
                    targetCanvas = uiCanvas;
                }
                break;
            case GameManager.GameStates.IsFlying:
                {
                    targetCanvas = uiCanvas;
                }
                break;
            case GameManager.GameStates.Rest:
                {
                    targetCanvas = uiCanvas;
                }
                break;
            case GameManager.GameStates.Finish:
                {
                    targetCanvas = gameOverCanvas;
                }
                break;
            case GameManager.GameStates.NoEnergy:
                {
                    targetCanvas = gameOverCanvas;
                }
                break;
            default:
                break;
        }

          
        foreach (var canvas in canvases)
        {
            Debug.Log("S " + targetCanvas + " : " + canvas);
            if(canvas == targetCanvas)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }


    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }
  
}
