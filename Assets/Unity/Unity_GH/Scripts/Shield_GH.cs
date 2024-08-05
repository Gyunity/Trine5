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

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "shell")
        {
            Destroy(other.gameObject);
            ValeriboyManager_GH.instance.onShield = false;
        }
    }
}
