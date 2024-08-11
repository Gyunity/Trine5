using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShellMat_GH : MonoBehaviour
{

    public Material[] shellMats;

    GameObject valeribot;
    ValeribotFSM_GH valeribotFSM;

    MeshRenderer shellcurrMat;

    float currTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        valeribot = GameObject.Find("Valeribot_GH");
        valeribotFSM = valeribot.GetComponent<ValeribotFSM_GH>();
        shellcurrMat = GetComponent<MeshRenderer>();

        if (valeribotFSM.bossPhase == 1)
        {
            shellcurrMat.material = shellMats[0];
        }
        else if (valeribotFSM.bossPhase == 2)
        {
            shellcurrMat.material = shellMats[1];

        }
        else
        {
            shellcurrMat.material = shellMats[2];

        }
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if(currTime > 2)
        {
            gameObject.tag = "Shell";
        }
    }
}
