﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    //Player
    GameObject target;
    public string targetName = "XXX";
    Vector3 dir;
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find(targetName);
        //target = GameObject.FindWithTag(targetName);
        dir =target.transform.position - transform.position;
        dir.Normalize();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            Destroy(gameObject);
        }
        
        /*//PlayerWepon에도
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.tag == "Player_equipment")
        {
            Debug.Log("불꽃 공격");
            //GameObject explosion = Instantiate(explosionFactory);
            //explosion.transform.position = transform.position;
            Destroy(gameObject);
        }*/
    }
}
