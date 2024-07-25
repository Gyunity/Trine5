using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    public GameObject enemyFactory;

    float currtTime = 0;
    float spawnTime = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currtTime += Time.deltaTime;

        if(currtTime > spawnTime)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;

            currtTime = 0;
        }
    }
}
