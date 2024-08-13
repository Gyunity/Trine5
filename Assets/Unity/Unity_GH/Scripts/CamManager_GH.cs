using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CamManager_GH : MonoBehaviour
{
    //바꿀 카메라 전역변수
    public GameObject deadCam;

    CinemachineVirtualCamera deadcamtrans;
    float currTime = 0;
    float transeTime = 5;
    float moveTime = 7;

    public ValeribotFSM_GH boss;

    public GameObject dragonBallFac;
    public Transform dragonBallPos;

    bool ballCreate = false;

    void Start()
    {
        deadcamtrans = deadCam.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.currState == ValeribotFSM_GH.EValeribotState.DIE)
        {
            CamTranse();
        }
    }

    void CamTranse()
    {
        currTime += Time.deltaTime;
        if (currTime > moveTime)
        {
            deadcamtrans.transform.Translate(Vector3.forward * 0.2f * Time.deltaTime, Space.Self);

            if (!ballCreate)
            {
                Instantiate(dragonBallFac, dragonBallPos);
                ballCreate = true;
            }

        }
        if (currTime > transeTime)
        {
            deadcamtrans.Priority = 11;
        }
    }

}
