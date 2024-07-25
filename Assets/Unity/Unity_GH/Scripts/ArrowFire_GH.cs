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
        //ToDo 화염화살 체크
        if(other.gameObject.layer == LayerMask.NameToLayer(""))
        {
            cannon_GH.arrowFire = true;
        }
    }
}
