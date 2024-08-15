using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition_GH : MonoBehaviour
{
    public Image fadeInOut;

    public bool fade = true;
    float fadeInSpeed = 0.5f;
    float fadeOutSpeed = 0.5f;

    void Start()
    {
        fadeInOut.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fade = false;
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
        else if (fadeInOut.color.a >= 1)
        {
            SceneManager.LoadScene(0);
        }


    }
}
