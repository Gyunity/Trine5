using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    //Player
    GameObject target;
    Vector3 dir;
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player_Dummy");
        dir =target.transform.position - transform.position;
        dir.Normalize();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
