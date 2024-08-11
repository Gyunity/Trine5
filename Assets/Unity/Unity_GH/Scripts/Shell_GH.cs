using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shell_GH : MonoBehaviour
{
    public ValeribotFSM_GH valeribotFSM;

    Image shell2Gage;

    public float shellGageDown = 0.1f;

     public Material cabbageMat;

    MeshRenderer mat;
    void Start()
    {
        shell2Gage = GetComponentInChildren<Image>();
        shell2Gage.fillAmount = 0;
        mat = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (valeribotFSM.bossPhase == 2)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (shell2Gage.fillAmount < 0.98f)
        {
            shell2Gage.fillAmount -= shellGageDown * Time.deltaTime;
        }
        else if(shell2Gage.fillAmount >= 1)
        {
            gameObject.tag = "Shell";
            mat.material = cabbageMat;
            shell2Gage.gameObject.SetActive(false);
        }
    }
}
