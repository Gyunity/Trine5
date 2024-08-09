using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_GH : MonoBehaviour
{
    ValeribotFSM_GH valeribotFSM;
    void Start()
    {
        valeribotFSM = GetComponentInParent<ValeribotFSM_GH>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shell")
        {
            other.gameObject.SetActive(false);
            valeribotFSM.onShield = false;
        }
    }
}
