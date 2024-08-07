using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_GH : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shell")
        {
            other.gameObject.SetActive(false);
            ValeribotManager_GH.instance.onShield = false;
        }
    }
}
