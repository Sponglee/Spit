using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class CameraManager : MonoBehaviour
{

    
    public CameraData[] cameras;
    

    // Start is called before the first frame update
    void Start()
    {
        SetLive("PlayerCam");
    }

 


    public void SetLive(string camName)
    {
        CinemachineVirtualCamera tmpCam;
        switch (camName)
        {
            case "PlayerCam":
                tmpCam = FetchCam(camName);
                tmpCam.m_Priority = 10;
                break;
            case "DropCam":
                tmpCam = FetchCam(camName);
                tmpCam.m_Priority = 10;
                break;
            default:
                break;
        }
    }



    public CinemachineVirtualCamera FetchCam(string name)
    {
        CinemachineVirtualCamera tmpCam = null;

        foreach (var cam in cameras)
        {
            if (cam.targetName == name)
            {
                tmpCam = cam.targetCam;
                
            }
            else
            {
                cam.targetCam.m_Priority = 0;
            }
        }



        return tmpCam;

    }
}
