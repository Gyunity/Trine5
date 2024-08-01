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
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Destroy(other.gameObject);
            ValeriboyManager_GH.instance.onShield = false;
        }
    }
}
