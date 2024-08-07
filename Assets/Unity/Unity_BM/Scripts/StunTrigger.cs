using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class StunTrigger : MonoBehaviour
{
    private float currTime;
    private float creatTime = 2f;

    public GameObject stt;
    //private bool isOn = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player_equipment") 
        {
            currTime += Time.deltaTime;
            if (currTime >= creatTime)
            {
                Debug.Log("공격가능");
                //스턴 애니메이션
                //hitsystem On
                
                Debug.Log("공격가능2");
                stt.SetActive(false);
            }
            
        }
        
    }
    
}
