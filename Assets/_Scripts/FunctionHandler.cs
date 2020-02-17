using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionHandler : Singleton<FunctionHandler>
{

    public UnityEvent<bool> menuOpenEvent;


    public GameObject menuCanvas;
    public GameObject uiCanvas;

    public void ToggleMenu(bool toggle)
    {
        StartCoroutine(ToggleMenuDelay(toggle));
    }

    
    public IEnumerator ToggleMenuDelay(bool toggle)
    {
        menuOpenEvent?.Invoke(toggle);
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
        }
    }
}
