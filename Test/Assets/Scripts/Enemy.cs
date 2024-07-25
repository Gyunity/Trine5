using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float enemySpeed = 5;
    GameObject player;

    float randNum = 0;
    Vector3 dir;
    void Start()
    {
        player = GameObject.Find("Player");

        randNum = Random.Range(0, 10);

         dir = player.transform.position - transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (randNum < 3)
        {
            transform.position += Vector3.down * enemySpeed * Time.deltaTime;
        }
        else
        {
            dir.Normalize();
            transform.position += dir * enemySpeed * Time.deltaTime;
        }
    }
}
