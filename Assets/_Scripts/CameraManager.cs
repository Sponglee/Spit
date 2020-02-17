using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class CameraManager : Singleton<CameraManager>
{

   

    [SerializeField] private  CinemachineVirtualCamera liveCam = null;

    public static CameraData[] cameras;

    void Awake()
    {
        GameObject[] tmpCameraObjects = GameObject.FindGameObjectsWithTag("VCam");
        //Debug.Log(tmpCameraObjects.Length);
        cameras = new CameraData[tmpCameraObjects.Length];

        for (int i = 0; i < tmpCameraObjects.Length; i++)
        {
            cameras[i] = tmpCameraObjects[i].GetComponent<CameraData>();
        }

    }



    public void SetLive(GameManager.GameStates stateName)
    {
        CinemachineVirtualCamera tmpCam;

        tmpCam = FetchCam(stateName);
        tmpCam.m_Priority = 10;
        liveCam = tmpCam;
    }



    public CinemachineVirtualCamera FetchCam(GameManager.GameStates state)
    {
        CinemachineVirtualCamera tmpCam = null;

        foreach (var cam in cameras)
        {
            
            if (cam.refState == state)
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
