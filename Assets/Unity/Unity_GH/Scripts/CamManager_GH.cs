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
    float shiftTime = 5;
    float moveTime = 7;

    public ValeribotFSM_GH boss;

    public GameObject dragonBallFac;
    public Transform dragonBallPos;

    bool ballCreate = false;

    //public Image fadeIn;
    //public Image fadeOut;


    void Start()
    {
        deadcamtrans = deadCam.GetComponent<CinemachineVirtualCamera>();
        //fadeOut.SetEnabled(false);
    }

    void Update()
    {
        if (boss.currState == ValeribotFSM_GH.EValeribotState.DIE)
        {
            StartCoroutine(CamTest());
        }
    }
    //void CamShift()
    //{
    //    currTime += Time.deltaTime;
    //    if (currTime > moveTime)
    //    {
    //        deadcamtrans.transform.Translate(Vector3.forward * 0.2f * Time.deltaTime, Space.Self);

    //        if (!ballCreate)
    //        {
    //            Instantiate(dragonBallFac, dragonBallPos);
    //            ballCreate = true;
    //        }

    //    }
    //    if (currTime > shiftTime)
    //    {
    //        deadcamtrans.Priority = 11;
    //    }
    //}
    IEnumerator CamTest()
    {
        yield return new WaitForSeconds(shiftTime);
        deadcamtrans.Priority = 11;

        yield return new WaitForSeconds(2);
        deadcamtrans.transform.Translate(Vector3.forward * 0.1f * Time.deltaTime, Space.Self);
        
        if (!ballCreate)
        {
            Instantiate(dragonBallFac, dragonBallPos);
            ballCreate = true;
        }

    }
}
