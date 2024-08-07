using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StongunButton : MonoBehaviour
{
    //public GameObject stungun;

    //public bool isOn = false;

    public bool isOn = false;
    public bool isMove = false;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //전기충격
            Debug.Log("전기 충격");
            ////Stungun 의 오브젝트에서 StungunMove 컴포넌트를 가져오자.
            //StungunMove stunmove = stungun.GetComponent<StungunMove>();
            ////가져온 컴포넌트에서 caneMove 를 true 로하자.
            //stunmove.canMove = true;
            //StungunDown stg =stungun.GetComponent<StungunDown>();-
            //stg.canMove = true;-
            isOn = true;
            isMove = true;
        }
    }
}
