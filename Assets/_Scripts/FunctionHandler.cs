using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameStartEvent : UnityEvent { };
public class MenuOpenEvent : UnityEvent<bool> { };

public class FunctionHandler : Singleton<FunctionHandler>
{
    public static GameStartEvent GameStart = new GameStartEvent();
    public static MenuOpenEvent MenuOpenEvent = new MenuOpenEvent();


    public GameObject menuCanvas;
    public GameObject uiCanvas;

    public void ToggleMenu(bool toggle)
    {
        StartCoroutine(ToggleMenuDelay(toggle));
    }

    
    public IEnumerator ToggleMenuDelay(bool toggle)
    {
        MenuOpenEvent?.Invoke(toggle);
        menuCanvas.SetActive(toggle);
        uiCanvas.SetActive(!toggle);

        if (toggle == true)
        {

            yield return null;
            GameManager.Instance.GameState = GameManager.GameStates.Paused;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.GameState = GameManager.GameStates.Player;
            GameStart?.Invoke();
        }
    }
}
