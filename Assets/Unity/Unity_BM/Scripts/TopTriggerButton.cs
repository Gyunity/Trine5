using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTriggerButton : MonoBehaviour
{
    public GameObject stungun;

    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //전기충격
            Debug.Log("전기 충격");

            //Stungun 의 오브젝트에서 StungunMove 컴포넌트를 가져오자.
            StungunMove stunmove = stungun.GetComponent<StungunMove>();
            //가져온 컴포넌트에서 caneMove 를 true 로하자.
            stunmove.canMove = true;


        }
    }

}
