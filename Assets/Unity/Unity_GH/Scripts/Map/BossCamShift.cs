using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class BossCamShift : MonoBehaviour
{
    public GameObject bossUI;

    public CinemachineVirtualCamera bossCam;

    bool cameramove = false;

    float currtTime = 0;
    float durTime = 4;

    int cam = 0;
    // Start is called before the first frame update
    void Start()
    {
        bossUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            if (cameramove)
            {
                currtTime += Time.deltaTime;
                if (currtTime < durTime)
                {
                    bossCam.Priority = 12;

                }
                else if (currtTime >= durTime)
                {
                    bossCam.Priority = 9;

                    cameramove = false;
                }

            }

        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cam++;
            bossUI.SetActive(true);
            cameramove = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossUI.SetActive(false);

        }
    }
}
