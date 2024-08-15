using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CamManager_GH : MonoBehaviour
{
    //바꿀 카메라 전역변수
    public GameObject deadCam;

    CinemachineVirtualCamera deadcamtrans;
    float currTime = 0;
    float shiftTime = 5;
    float moveTime = 2;
    float fadeTime = 6;

    public ValeribotFSM_GH boss;

    public GameObject dragonBallFac;
    public Transform dragonBallPos;

    bool ballCreate = false;

    //fade 효과
    public Image fadeInOut;

    bool fade = true;
    float fadeInSpeed = 0.5f;
    float fadeOutSpeed = 0.2f;


    public GameObject endingUI;

    void Start()
    {
        //bgm 사운드 바꾸기 todo 주석 없애기
        SoundManager.instance.PlayBgmSound(SoundManager.EBgmType.BGM_BOSS);

        deadcamtrans = deadCam.GetComponent<CinemachineVirtualCamera>();

        fadeInOut.gameObject.SetActive(true);
    }

    void Update()
    {
        if (boss.currState == ValeribotFSM_GH.EValeribotState.DIE)
        {
            StartCoroutine(CamTest());
        }
        if (fade)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }


    }
    void FadeIn()
    {
        if (fadeInOut.color.a > 0)
        {
            fadeInOut.color -= new Color(0, 0, 0, fadeInSpeed) * Time.deltaTime;
        }
    }
    void FadeOut()
    {
        if (fadeInOut.color.a < 1)
        {
            fadeInOut.color += new Color(0, 0, 0, fadeOutSpeed) * Time.deltaTime;
        }
        else if(fadeInOut.color.a >= 1)
        {
            SoundManager.instance.StopBgmSound();
            SceneManager.LoadScene(3);
        }


    }


    IEnumerator CamTest()
    {
        yield return new WaitForSeconds(shiftTime);
        deadcamtrans.Priority = 11;
        endingUI.SetActive(false);

        yield return new WaitForSeconds(moveTime);
        deadcamtrans.transform.Translate(Vector3.forward * 0.1f * Time.deltaTime, Space.Self);
        if (!ballCreate)
        {
            Instantiate(dragonBallFac, dragonBallPos);
            ballCreate = true;
        }

        yield return new WaitForSeconds(fadeTime);
        fade = false;
        //SceneManager.LoadScene(3);

    }
}
