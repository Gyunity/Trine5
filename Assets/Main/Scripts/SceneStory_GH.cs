using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStory_GH : MonoBehaviour
{
    public int GemCount = 0;

    int Gem = 9;
    public GameObject door;
    public GameObject Connon;

    public Text gem;
    float curr = 0;
    float dur = 4;

    bool dorrSound = false;
    void Start()
    {
        
    }

    void Update()
    {
        gem.text = Convert.ToString(Gem - GemCount);
        if(GemCount >= 9)
        {
            curr += Time.deltaTime;
            if(curr < dur)
            {
                if (!dorrSound)
                {
                    SoundManager.instance.PlayStoryEftSound(SoundManager.EStoryEftType.STORY_DOOR_OPEN);
                    dorrSound = true;
                }
                door.transform.localPosition += Vector3.up * 0.5f * Time.deltaTime;
                Connon.transform.localPosition -= Vector3.right * 1 * Time.deltaTime;
            }
        }

    }
}
