using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Events;

public class CameraManager : Singleton<CameraManager>
{



    [SerializeField] private CinemachineVirtualCamera liveCam = null;

    public static CameraData[] cameras;

    void Awake()
    {



        GameObject[] tmpCameraObjects = GameObject.FindGameObjectsWithTag("VCam");
        Debug.Log(tmpCameraObjects.Length);
        cameras = new CameraData[tmpCameraObjects.Length];

        for (int i = 0; i < tmpCameraObjects.Length; i++)
        {
            cameras[i] = tmpCameraObjects[i].GetComponent<CameraData>();
        }


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


    public void SetLive(GameManager.GameStates stateName)
    {
        CinemachineVirtualCamera tmpCam;

        tmpCam = FetchCam(stateName);
        if (tmpCam != null)
        {
            tmpCam.m_Priority = 10;
            liveCam = tmpCam;
        }
        else
        {
            liveCam.m_Priority = 10;
        }

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
