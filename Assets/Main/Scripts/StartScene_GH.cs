using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene_GH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBgmSound(SoundManager.EBgmType.BGM_OPENING);

    }

    void Update()
    {
        
    }

    public void NewGame()
    {
        SoundManager.instance.PlayMainEftSound(SoundManager.EMainEftType.MAIN_MANU);
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        SoundManager.instance.PlayMainEftSound(SoundManager.EMainEftType.MAIN_MANU);
        Application.Quit();
    }
}
