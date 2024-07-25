using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cam_GH : MonoBehaviour
{
    public GameObject player;
    public GameObject camFIx;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camFocusPos = (player.transform.position + camFIx.transform.position) / 2;

        Vector3 camDir = camFocusPos - transform.position;

        transform.forward = camDir;


       

    }

}
