using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class StungunDown : MonoBehaviour
{
    public float move = 2;
    public float movespeed= 1f;
    public bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            move -= movespeed * Time.deltaTime;
            if (move <= 0)
            {
                canMove = false;
            }
            transform.position += Vector3.down * movespeed * Time.deltaTime;
        }

    }
}
