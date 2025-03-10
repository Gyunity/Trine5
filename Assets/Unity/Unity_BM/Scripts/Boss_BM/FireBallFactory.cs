﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallFactory : MonoBehaviour
{
    public GameObject fireBallprefab;
    public float delay = 1f;
    public GameObject fireBallFactoryPos;

    // Start is called before the first frame update
    void Start()
    {
        EnemyMove enemyMove = GetComponent<EnemyMove>();
        enemyMove.FireGo = FireGo;
        // StartCoroutine(GoFire());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(GoFire());
        }
    }
    void FireGo()
    {
        StartCoroutine(GoFire());
    }
    IEnumerator GoFire()
    {
        int prefabcount= Random.Range(2, 4); 
        
        for (int i = 0; i< prefabcount; i++)
        {
            GameObject ball =Instantiate(fireBallprefab);
            ball.transform.position = fireBallFactoryPos.transform.position;
            yield return new WaitForSeconds(delay);
        }
        
    }

}
