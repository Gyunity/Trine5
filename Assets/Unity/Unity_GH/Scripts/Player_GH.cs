using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GH : MonoBehaviour
{
    //플레이어 움직임
    float playerSpeed = 5.0f;
    CharacterController cc;

    

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(h, 0, 0);



        cc.Move(dir * playerSpeed * Time.deltaTime);




    }
}
