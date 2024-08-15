using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    public Image fadeInOut;

    bool fadeStart = false;

    bool fade = true;
    float fadeInSpeed = 0.3f;
    float fadeOutSpeed = 0.2f;

    public CinemachineVirtualCamera startCam;

    bool textFade = true;

    //자막들
    public Image startText;

    //cam 전환 인트
    int camTransition = 0;
    void Start()
    {
        SoundManager.instance.PlayBgmSound(SoundManager.EBgmType.BGM_STORY);
        fadeInOut.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fadeStart)
        {
            StartCoroutine(TextDelay());
        }

        if (fadeStart)
        {
            if (fade)
            {
                FadeIn();
            }
            else
            {
                FadeOut();
            }
        }

        if (camTransition == 1)
        {
            startCam.Priority = 9;
        }
    }
    void FadeIn()
    {
        if (fadeInOut.color.a > 0)
        {
            fadeInOut.color -= new Color(0, 0, 0, fadeInSpeed) * Time.deltaTime;
            startText.color -= new Color(0, 0, 0, fadeInSpeed + 0.5f) * Time.deltaTime;
        }
    }
    void FadeOut()
    {
        if (fadeInOut.color.a < 1)
        {
            fadeInOut.color += new Color(0, 0, 0, fadeOutSpeed) * Time.deltaTime;
        }
        else if (fadeInOut.color.a >= 1)
        {
            SoundManager.instance.StopBgmSound();
            SceneManager.LoadScene(3);
        }
    }
    IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(2);
        if (textFade)
        {

            startText.color += new Color(0, 0, 0, fadeInSpeed) * Time.deltaTime;
        }
        if (startText.color.a >= 1)
        {
            textFade = false;
        }

        yield return new WaitForSeconds(5);
        fadeStart = true;
        startText.color -= new Color(0, 0, 0, fadeInSpeed) * Time.deltaTime;

        yield return new WaitForSeconds(3);
        camTransition = 1;
    }
}
