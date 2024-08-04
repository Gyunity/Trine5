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

    // Start is called before the first frame update
    void Start()
    {
        sg= trigger_Stungun.GetComponent<StongunButton>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        StungunMove();
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
}
