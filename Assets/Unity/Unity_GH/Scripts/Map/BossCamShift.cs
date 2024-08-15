using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamShift : MonoBehaviour
{
    public GameObject bossUI;

    public CinemachineVirtualCamera bossCam;
    // Start is called before the first frame update
    void Start()
    {
        bossUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossCam.Priority = 12;
            bossUI.SetActive(true);

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossCam.Priority = 9;
            bossUI.SetActive(false);

        }
    }
}
