using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stungun : MonoBehaviour
{
    public GameObject trigger_L;
    public GameObject trigger_R;
    public GameObject trigger_Stungun;

    StongunButton sg;
    //public bool isMove;
    public float move = 2;
    public float moveSpeed = 2f;

    Trigger_L tl;
    Trigger_R tr;

    //플레이어 도구가 오면 반응하는 트리거
    public GameObject stunTrigger;
    

    // Start is called before the first frame update
    void Start()
    {
        sg= trigger_Stungun.GetComponent<StongunButton>();
        tl= trigger_L.GetComponent<Trigger_L>();
        tr= trigger_R.GetComponent<Trigger_R>();



    }

    // Update is called once per frame
    void Update()
    {
        //순서대로 진행
        //StungunMove();
        //OnSwitch();
        //OnSwitch2();
        
    }

    void StungunMove()
    {
        //if (sg.isMove == true)
        //{
        //    transform.Translate(Vector3.down * move *moveSpeed*Time.deltaTime);
        //}
        //sg.isMove = false;
        if (sg.isMove == true)
        {
            move -= moveSpeed * Time.deltaTime;
            if (move <= 0)
            {
                sg.isMove = false;
            }
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }
    private void OnSwitch()
    {
        if(sg.isOn == true)
        {
            stunTrigger.SetActive(true);
            Debug.Log("OnSwitch");
        }
    }
    void OnSwitch2()
    {
        if (tl.isOn && tr.isOn)
        {
            stunTrigger.SetActive(true);
            Debug.Log("OnSwitch2");
            tl.isOn = false;
            tr.isOn = false;
        }
    }
}
