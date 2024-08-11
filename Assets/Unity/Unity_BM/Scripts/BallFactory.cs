using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject ballprefab;
    public float delay = 1f;
    public GameObject ballFactoryPos;

    // Start is called before the first frame update
    void Start()
    {
        EnemyMove enemyMove = GetComponent<EnemyMove>();
        enemyMove.GoBall = BallGo;
        //StartCoroutine(BallGo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(BallGoCo());
        }
    }

    void BallGo()
    {
        
        StartCoroutine(BallGoCo());
    }

    IEnumerator BallGoCo()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject ball = Instantiate(ballprefab);
            ball.transform.position = ballFactoryPos.transform.position;
            yield return new WaitForSeconds(delay);
        }

    }
}
