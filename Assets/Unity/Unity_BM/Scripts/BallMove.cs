using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Rigidbody rd;
    

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rd.velocity = Vector3.right*Time.deltaTime;
    }
}
