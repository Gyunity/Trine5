using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire_GH : MonoBehaviour
{
    public GameObject Cannon;
    Cannon_GH cannon_GH;
    void Start()
    {
        cannon_GH = Cannon.GetComponent<Cannon_GH>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ArrowMove_HMJ arrowType = other.GetComponent<ArrowMove_HMJ>();

        if (other.gameObject.layer == LayerMask.NameToLayer("Arrow") && arrowType.arrowType == ArrowType_HMJ.ArrowType.ArrowFireType)
        {
            
            if (cannon_GH.shellLoad)
            {
                cannon_GH.arrowFire = true;

            }
        }
        
    }
}
