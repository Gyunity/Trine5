using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttack_GH : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("꼬리 공격 하기");
        }
    }
}
