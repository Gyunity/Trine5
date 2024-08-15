using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene_GH : MonoBehaviour
{
    //fade 효과
    public Image fadeInOut;

    bool fade = true;
    float fadeInSpeed = 0.5f;
    float fadeOutSpeed = 0.8f;


    void Start()
    {
        fadeInOut.gameObject.SetActive(true);
        SoundManager.instance.PlayBgmSound(SoundManager.EBgmType.BGM_OPENING);

    }

    void Update()
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

    public void NewGame()
    {
        SoundManager.instance.PlayMainEftSound(SoundManager.EMainEftType.MAIN_MANU);
        fade = false;
    }
    public void ExitGame()
    {
        SoundManager.instance.PlayMainEftSound(SoundManager.EMainEftType.MAIN_MANU);
        Application.Quit();
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
        else if (fadeInOut.color.a >= 1)
        {

            SceneManager.LoadScene(1);
        }


    }

}
