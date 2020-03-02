using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Events;

public class CameraManager : Singleton<CameraManager>
{



    [SerializeField] private CinemachineVirtualCamera liveCam = null;
 
    public static GameObject[] cameras;

    void Awake()
    {


        cameras = GrabGameObjectCollection("VCam");

    }


    private void Start()
    {
        PlayerController.FinishTarget.AddListener(SetFinishTargetCam);
    }

    public void SetFinishTargetCam(Transform target = null)
    {
        liveCam.m_Follow = target;
        liveCam.m_LookAt = target;
    }


    public void SetLive(GameManager.GameStates state)
    {
        Debug.Log("SETLIVE{0}");
        CameraData tmpData = null;

        foreach (var cam in cameras)
        {

            if (cam.GetComponent<CameraData>().refState == state)
            {
                tmpData = cam.GetComponent<CameraData>();

                //Set active cam to live priority
                if (tmpData != null)
                {
                    tmpData.targetCam.m_Priority = 10;
                    liveCam = tmpData.targetCam;

                  
                    //Set active canvas if ther's one
                    if (tmpData.targetCanvas != null)
                    {
                        tmpData.targetCanvas.gameObject.SetActive(true);
                    }
                }
                else if (liveCam != null)
                {
                    liveCam.m_Priority = 10;
                }


               
            }
            else
            {
                //If not active gamestate - disable camera and canvas
                cam.GetComponent<CameraData>().targetCam.m_Priority = 0;

            }
        }
    }




    public GameObject[] GrabGameObjectCollection(string tag)
    {
        GameObject[] tmpObjects = GameObject.FindGameObjectsWithTag(tag);

        Debug.Log(tmpObjects.Length);

        return tmpObjects;
    }

}
